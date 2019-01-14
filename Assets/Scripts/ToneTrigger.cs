using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToneTrigger : MonoBehaviour {
    public bool isPlaying = false;
    public int note = 32;
    public pxFemme synth;

	void Start () {
        synth = GetComponentInChildren<pxFemme>();
	}
	
	void Update () {
        synth.feedback = transform.position.x * 0.1f + 0.2f;
        synth.modfeedback = transform.position.z * 0.1f + 0.2f;
        synth.modulation = transform.position.y;

		if (!isPlaying)
        {
            if (transform.position.y > 0.05)
            {
                isPlaying = true;
                synth.KeyOn(note);
            }
        } else if (transform.position.y <= 0.05)
        {
            isPlaying = false;
            synth.KeyOff();
        }
	}
}
