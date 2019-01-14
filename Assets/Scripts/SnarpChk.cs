using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnarpChk : MonoBehaviour {
    public float speed = 10f;
    public pxSnarple ctrl;
    public float lifespan = 0.5f;
    public float life = 0f;
	// Use this for initialization
	void Start () {
        ctrl = GetComponentInChildren<pxSnarple>();
        ctrl.length = 0.02f + transform.position.x * 0.01f;
        ctrl.start = 0.5f + transform.position.z * 0.1f;
        ctrl.speed = 1f+ transform.position.y*0.5f;
        ctrl.KeyOn();

    }
	
	// Update is called once per frame
	void Update () {
        transform.Translate(Vector3.up * speed * Time.deltaTime);
        life += Time.deltaTime;
        if (life > lifespan) Destroy(gameObject);
	}
}
