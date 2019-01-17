using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pxLope {
    //----envelope
    public float attack = 0.003f;
    public float release = 0.2f;
    public float current = 0.0f;
    public bool sustain = false;
    private float delta = 0.0f;

    private float sampleRate = 44100f;

    public void KeyOn()
    {
        delta = 1.0f / (attack * sampleRate);
    }

    public void KeyOff()
    {
        delta = -1.0f / (release * sampleRate);
    }

    public void SetParams(float att, float rel, bool sus)
    {
        attack = att;
        release = rel;
        sustain = sus;
    }

    //--- AR/ASR envelope
    public float Run()
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
}
