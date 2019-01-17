using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampTrigger : MonoBehaviour {
    public bool isPlaying = false;
    public int note = 32;
    public pxSnarple synth;

	// Use this for initialization
	void Start () {
        synth = GetComponentInChildren<pxSnarple>();
	}
	
	void Update () {
        synth.start = transform.position.x * 0.1f + 0.5f;
        synth.length = transform.position.z * 0.1f + 0.5f;
        synth.speed = transform.position.y*3f+0.5f;

        if (!isPlaying)
        {
            if (transform.position.y > 0.05)
            {
                isPlaying = true;
                synth.KeyOn();
            }
        } else if (transform.position.y <= 0.05)
        {
            isPlaying = false;
            synth.KeyOff();
        }
	}

}
