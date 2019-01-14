using UnityEngine;
using System.Collections;

//[RequireComponent (typeof (AudioSource))]

public class pxSnarpleControl : MonoBehaviour {

public pxSnarple synthMod0;

[Range(0.0f, 1.0f)] public float volume = 0.5f;
[Range(0.0f, 1f)] public float sm_start = 0f;
[Range(0.0f, 1.0f)] public float sm_length = 1f;
[Range(0.0f, 10f)] public float sm_speed = 1f;

void Awake()
{
    synthMod0 = GetComponent<pxSnarple>();
}

void Start() {
    synthMod0 = GetComponent<pxSnarple>();
 }

void Update() {
	synthMod0.SetParam(sm_length,sm_start,sm_speed);
}

public void KeyOn() {
    synthMod0.KeyOn();
}

public void KeyOff()
    {
        synthMod0.KeyOff();
    }
}
