using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pxLFO {
    public float frequency = 1f;
    public float amp = 1f;
    private float sampleRate = 44100f;

    private float step = 0f;
    private float phase = 0f;
    public float value = 0f;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        step = frequency / sampleRate;
	}

    public float Run()
    {
        phase += step;
        phase -= Mathf.Floor(phase);
        return (Mathf.Abs(phase - 0.5f) * 2f - 1f) * amp;
    }

}
