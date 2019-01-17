using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StraxChk : MonoBehaviour {
    public float speed = 10f;
    public pxStrax ctrl;
    public float lifespan = 0.5f;
    public float life = 0f;
    private float[] notes = new float[] {0,2f,3f,5f,7f,8f,10f,12f};
	// Use this for initialization
	void Start () {
        ctrl = GetComponent<pxStrax>();
        ctrl.cutoff = 0.1f + transform.position.x * 0.05f;
        ctrl.resonance = 0.6f + transform.position.z * 0.1f;
        ctrl.osc2Mix = 0.5f+ transform.position.y*0.25f;
        float note = notes[(int)(Mathf.Floor(Mathf.Abs(transform.position.x) * 24f) % 8f)] + Mathf.Floor(Random.value * 3f) * 12f + 43f;
        ctrl.KeyOn(note);

    }
	
	// Update is called once per frame
	void Update () {
        transform.Translate(Vector3.up * speed * Time.deltaTime);
        life += Time.deltaTime;
        if (life > lifespan) Destroy(gameObject);
	}
}
