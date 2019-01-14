using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pxSnarple : MonoBehaviour { 
    public float sampleRate = 44100f;
    public AudioClip sample;
    
    private float level = 0.0f;
    public float volume = 0.5f;
    //----player parameters
    public float start = 0.0f;
    public float length = 0.0f;
    public float speed = 1f;

    //----envelope
    public float attack = 0.003f;
    public float release = 0.2f;
    private float current = 0.0f;
    public bool sustain = false;
    public float amplifier = 1.0f;

    public float delta = 0.0f;


    //----player internals
    private float posx= 0.0f;
    private float lenx = 0.0f;
    private float startx = 0f;
    private float[] sampdata;
    private int samplength;
    private float sampcount;
    private int sampchans;

    // Use this for initialization
    void Start () { 
        samplength = sample.samples * sample.channels;
        sampcount = (float)sample.samples;
        sampdata = new float[samplength];
        sampchans = sample.channels;
        sample.GetData(sampdata, 0);
        startx = start;
        lenx = length * (sampcount - start * sampcount);
    }

    void Awake()
    {
        samplength = sample.samples * sample.channels;
        sampcount = (float)sample.samples;
        sampdata = new float[samplength];
        sampchans = sample.channels;
        sample.GetData(sampdata, 0);
    }

    public float Run()
    {
        level = SnarpleRun() * LopeRun();
        return level;
    }

    public void SetParam(float sm_length, float sm_start, float sm_speed)
    {
        length = Mathf.Max(sm_length,0.00001f);
        start = Mathf.Max(Mathf.Min(sm_start,1f),0f);
        speed = sm_speed;
    }

    public float SnarpleRun()
    {
        float duck = 1f;
        float lastpos = posx;
        lenx = length*(sampcount - startx*sampcount);
        posx += speed;
        posx = posx%lenx;
        if (posx < 0f)
        {
            posx = lenx + posx;
        }
        if (posx < 100f) duck = Mathf.Lerp(0f, 1f, posx / 100f);
        else if (posx > lenx - 100) duck = Mathf.Lerp(0f, 1f, (lenx - posx) / 100f);
        if (Mathf.Abs(lastpos - posx) > speed) startx = start;//check if a new loop by testing the phase difference against speed
        float currentx = posx + startx*sampcount;
        float snarpval = Mathf.Lerp(sampdata[(int)currentx*sampchans],sampdata[((int)currentx+1)*sampchans],currentx%1);
        return snarpval*duck;
    }

    public void KeyOn()
    {   
        posx = 0f;
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
