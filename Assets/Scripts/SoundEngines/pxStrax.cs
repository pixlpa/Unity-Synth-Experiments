using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pxStrax : MonoBehaviour {
    public float sampleRate = 44100f; 
    private float level = 0.0f;
    public float volume = 0.5f;

    //----synth parameters
    public float cutoff = 0.1f;
    public float resonance = 0.8f;
    public float envelope = 0.01f;
    public float harmonic = 0.5f;
    public float osc2Mix = 0.1f;
    public float distortion = 0.5f;
    public float lfoRate = 1.5f;
    public float lfoAmp = 0.1f;

    //----envelope
    public float attack = 0.003f;
    public float release = 0.2f;
    private float current = 0.0f;
    public bool sustain = false;
    private float delta = 0.0f;


    //----osc internals
    private float px1 = 0.0f;
    private float px2 = 0.0f;
    private float step = 0f;

    //----filter internals
    private float out1 = 0f;
    private float out2 = 0f;
    private float out3 = 0f;
    private float out4 = 0f;
    private float in1 = 0f;
    private float in2 = 0f;
    private float in3 = 0f;
    private float in4 = 0f;

    private pxLFO lfo = new pxLFO();

    // Use this for initialization
    void Start () 
    {
        lfo.frequency = lfoRate;
        lfo.amp = lfoAmp;
    }

    void Awake()
    {

    }

    public float Run()
    {
        lfo.frequency = lfoRate;
        lfo.amp = lfoAmp;
        level = FilterRun(StraxRun()) * LopeRun();
        return level;
    }

    public void SetParam(float sm_length, float sm_start, float sm_speed)
    {
    }

    //really basic 2 part square wave and triangle generator
    public float StraxRun()
    {
        float step2 = harmonic * step;
        px1 += step;
        px1 = px1 - Mathf.Floor(px1);
        px2 += step2;
        px2 = px2 - Mathf.Floor(px2);
        return 1.0f - ((Mathf.Floor(px1+0.5f)*2f-1f) * (1f - osc2Mix) + (Mathf.Abs(1f-(px2*2f))*2f-1f) * osc2Mix);
    }

    //Moog VCF from http://www.musicdsp.org/showArchiveComment.php?ArchiveID=26

    public float FilterRun(float filterIn)
    {
        float fc = Mathf.Clamp (cutoff+current*envelope+lfo.Run()*cutoff, 0.0005f, 1.0f);
        float res = Mathf.Clamp (resonance, 0f,1f);
        float input = filterIn;
        float f = fc * 1.16f;
        float fb = res * (1.0f - 0.15f * f * f);
        input -= WaveShape(out4) * fb;
        input *= 0.35013f * (f * f) * (f * f);
        out1 = input + 0.3f * in1 + (1 - f) * out1; // Pole 1
        in1 = input;
        out2 = out1 + 0.3f * in2 + (1 - f) * out2;  // Pole 2
        in2 = out1;
        out3 = out2 + 0.3f * in3 + (1 - f) * out3;  // Pole 3
        in3 = out2;
        out4 = out3 + 0.3f * in4 + (1 - f) * out4;  // Pole 4
        in4 = WaveShape(out3);
        return out4;
    }

    //Waveshaper equation from http://www.musicdsp.org/showArchiveComment.php?ArchiveID=46
    public float WaveShape(float waveIn)
    {
        float x = waveIn;
        float distx = Mathf.Clamp(distortion, 0.000f, 1.0f);
        float k = 2f * distx / (1f - distx);

        return (1f + k) * x / (1f + k * Mathf.Abs(x));
    }

    public void SetNote(float note)
    {
        float freq = 440.0f * Mathf.Pow(2.0f, 1.0f * (note - 69f) / 12.0f);
        step = freq / sampleRate;
    }

    public void KeyOn(float midinote)
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
