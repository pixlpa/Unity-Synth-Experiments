using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagSynthControl : MonoBehaviour {
    public pxFemme ctrl;
    private Rigidbody mover;
    // Use this for initialization
    void Start()
    {
        mover = GetComponent<Rigidbody>();
        ctrl = GetComponent<pxFemme>();
        ctrl.modfeedback = 0.4f + transform.position.x * 0.1f;
        ctrl.feedback = 0.6f + transform.position.z * 0.1f;
        ctrl.modulation = 0.1f+ mover.velocity.magnitude * 0.025f;


    }

    // Update is called once per frame
    void Update()
    {
        ctrl.modfeedback = 0.4f + transform.position.x * 0.3f;
        ctrl.feedback = 0.6f + transform.position.z * 0.1f;
        ctrl.modulation = mover.velocity.magnitude * 0.1f;
    }
}
