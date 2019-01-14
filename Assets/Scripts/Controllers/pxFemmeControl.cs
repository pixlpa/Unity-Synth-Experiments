using UnityEngine;
using System.Collections;

//[RequireComponent (typeof (AudioSource))]

public class pxFemmeControl : MonoBehaviour {

public pxFemme synthMod0;

[Range(0.0f, 1.0f)] public float volume = 0.5f;
[Range(-1.0f, 1.0f)] public float stereo = 0.5f;
[Range(0.05f, 10f)] public float fm_mul = 8f;
[Range(0.0f, 1.0f)] public float fm_mod = 0.08f;
[Range(0.0f, 5f)] public float fm_fb = 0.2f;
[Range(0.0f, 5f)] public float fm_modfb = 0.2f;

void Awake()
{
	synthMod0 = GetComponent<pxFemme>();
}

void Start()
{

}

void Update() {
	synthMod0.SetParam(fm_mul, fm_mod, fm_fb,fm_modfb);
}

public void KeyOn(int note) {
    synthMod0.KeyOn(note);
}

public void KeyOff()
    {
        synthMod0.KeyOff();
    }
}
