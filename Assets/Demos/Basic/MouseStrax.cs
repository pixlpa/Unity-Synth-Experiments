using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseStrax : MonoBehaviour {
    public bool selected = false;
    private pxStrax[] synths;
    public int current = 0;
    public int note = 32;
    private float[] notes = new float[] { 0, 2f, 3f, 5f, 7f, 8f, 10f, 12f };
    // Use this for initialization
    void Start () {
        synths = GetComponentsInChildren<pxStrax>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButton(0))
        {
            transform.position =  Camera.main.ScreenToWorldPoint(Input.mousePosition)+new Vector3(0f,0f,20f);
            float note_on = notes[(int)(Mathf.Floor((transform.position.x+4f) * 2f) % 8f)] + Mathf.Floor(transform.position.y+1*2) * 12f + 20f;

            if (!selected)
            {
                current = (current + 1) % 5;
                synths[current].KeyOn(note_on);
                selected = true;
            }
        }
        else
        {
            if (selected)
            {
                selected = false;
                synths[current].KeyOff();
            }
        }
    }
}
