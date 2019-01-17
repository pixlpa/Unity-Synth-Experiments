using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadSynthControl : MonoBehaviour {
    public pxSnarple synth;
    private Rigidbody motion;
    public bool playing = false;
    public float mags = 0f;

     // Use this for initialization
    void Start () {
        synth = GetComponent<pxSnarple>();
        motion = GetComponent<Rigidbody>();
	}
	
	void FixedUpdate () {
        mags = motion.velocity.magnitude;
		if(motion.velocity.magnitude > 2f)
        {
            if (!playing)
            {
                playing = true;
                synth.KeyOn();
            }
        }
        else if (playing)
        {
            playing = false;
            synth.KeyOff();
        }
        synth.start = Mathf.Clamp(Mathf.Abs(motion.velocity.y * 0.25f),0f,0.9f);
        synth.length = Mathf.Clamp(Mathf.Abs(transform.position.x * 0.001f)+0.001f,0.001f,1f);
    }
}
