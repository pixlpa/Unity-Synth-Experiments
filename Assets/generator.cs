using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class generator : MonoBehaviour {
    public GameObject snarp;
    public float interval = 0.5f;
    public float force = 10f;
    private float acc_time = 0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        acc_time+= Time.deltaTime;
        if (acc_time > interval)
        {
            acc_time = 0;
            GameObject pop = Instantiate(snarp, transform.position + transform.forward * 0.25f, transform.rotation);
            pop.GetComponent<Rigidbody>().velocity = pop.transform.forward * force;
        }

    }
}
