using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pxFemme : MonoBehaviour {
    public float sampleRate = 44100f;
    public float volume = 0.5f;
    private float level = 0.0f;
    //----oscillator parameters
    public float multiplier = 1.0f;
    public float modulation = 0.0f;
    public float feedback = 0.01f;
    public float modfeedback = 0.01f;

    //----envelope
    public float attack = 0.003f;
    public float release = 0.2f;
    public float current = 0.0f;
    public bool sustain = false;
    public float amplifier = 1.0f;

    public float delta = 0.0f;


    //----oscillator internals
    private float mx = 0.0f;
    private float cx = 0.0f;
    private float step = 0.0f;
    private float history1 = 0.0f;
    //----oscillator magic numbers
    static private float kPi = 3.14159265359f;
    static private float kPi2 = 6.28318530718f;
    static private float k4dPi = 1.27323954474f;
    static private float k4dPiPi = 0.40528473456f;


    // Use this for initialization
    void Start () { 
    }

    public float Run()
    {
        level = FBFMRun() * LopeRun();
        return level;
    }

    public void SetParam(float fm_mul, float fm_mod, float fm_fb, float fm_modfb)
    {
        multiplier = fm_mul;
        modulation = fm_mod;
        feedback = fm_fb;
        modfeedback = fm_modfb;
    }

    //---- dual-FB FM Oscillator
    // http://devmaster.net/forums/topic/4648-fast-and-accurate-sinecosine/
    static private float fast_sin(float x)
    {
        x -= kPi;
        x = k4dPi * x - k4dPiPi * x * Mathf.Abs(x);
        return -0.225f * (x * Mathf.Abs(x) - x) - x;
    }

    public void SetNote(float note)
    {
        float freq = 440.0f * Mathf.Pow(2.0f, 1.0f * (note - 69f) / 12.0f);
        step = freq / sampleRate;
    }

    public float FBFMRun()
    {
        float xstep = step + history1 * feedback * step;
        mx += step * multiplier + history1 * modfeedback * step * multiplier;
        cx += xstep;
        mx -= Mathf.Floor(mx);
        cx -= Mathf.Floor(cx);
        var x = cx + modulation * fast_sin(kPi2 * mx);
        x -= Mathf.Floor(x);
        history1 = fast_sin(kPi2 * x);
        return history1;
    }

    public void KeyOn(int midinote)
    {
        SetNote(midinote);
        delta = 1.0f / (attack * sampleRate);
    }

    public void KeyOff()
    {
        delta = -1.0f / (release * sampleRate);
    }

    //--- AR/ASR envelope
    public float LopeRun()
    {
        float lopestate = current;
        if (delta > 0.0f)
        {
            current += delta;
            if (current >= 1.0f)
            {
                current = 1.0f;
                if (!sustain) KeyOff();
            }
        }
        else
        {
            current = Mathf.Max(current + delta, 0.0f);
        }
        return lopestate;
    }

    void OnAudioFilterRead(float[] data, int channels)
    {
        for (var i = 0; i < data.Length; i += 2)
        {
            float s1 = Run() * volume;
            data[i] = s1;
            data[i + 1] = s1;
        }
    }
}
