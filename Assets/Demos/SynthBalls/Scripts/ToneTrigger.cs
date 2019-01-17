using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToneTrigger : MonoBehaviour {
    public bool isPlaying = false;
    public int note = 32;
    public pxFemme synth;

	void Start () {
        synth = GetComponent<pxFemme>();
	}
	
	void Update () {
        synth.feedback = transform.position.x * 0.3f + 0.5f;
        synth.modfeedback = transform.position.z * 0.3f + 0.5f;
        synth.modulation = transform.position.y*0.75f;

		if (!isPlaying)
        {
            if (transform.position.y > 0.4)
            {
                isPlaying = true;
                synth.KeyOn(note);
            }
        } else if (transform.position.y <= 0.4)
        {
            isPlaying = false;
            synth.KeyOff();
        }
	}
}
