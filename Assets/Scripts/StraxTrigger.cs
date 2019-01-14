using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StraxTrigger : MonoBehaviour {
    public bool isPlaying = false;
    public float note = 32f;
    public pxStrax synth;

	void Start () {
        synth = GetComponent<pxStrax>();
	}
	
	void Update () {
        synth.cutoff = transform.position.y * 0.1f;
        synth.resonance = transform.position.x * 0.5f + 0.5f;
        synth.osc2Mix = transform.position.z + 0.5f;
        if (!isPlaying)
        {
            if (transform.position.y > 0.0)
            {
                isPlaying = true;
                synth.KeyOn(note);
            }
        } else if (transform.position.y <= 0.0)
        {
            isPlaying = false;
            synth.KeyOff();
        }
	}
}
