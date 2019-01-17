using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMover : MonoBehaviour {
    public bool selected = false;
    public pxFemme synth;
    public int note = 32;
    private float[] notes = new float[] { 0, 2f, 3f, 5f, 7f, 8f, 10f, 12f };
    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButton(0))
        {
            transform.position =  Camera.main.ScreenToWorldPoint(Input.mousePosition)+new Vector3(0f,0f,20f);
            float note_on = notes[(int)(Mathf.Floor(Mathf.Abs(transform.position.x+1f) * 5f) % 8f)] + Mathf.Floor(transform.position.y+1*2) * 12f + 20f;

            if (!selected) synth.KeyOn(note_on);
            selected = true;
        }
        else
        {
            if (selected)
            {
                selected = false;
                synth.KeyOff();
            }
        }
    }
}
