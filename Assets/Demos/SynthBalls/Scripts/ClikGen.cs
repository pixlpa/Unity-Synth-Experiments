using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClikGen : MonoBehaviour {
    public GameObject effect;
    public float lifespan = 2f;
    private float life = 0f;

    private void OnCollisionEnter(Collision collision)
    {
        foreach (ContactPoint contact in collision.contacts)
        {
            Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
            Vector3 pos = contact.point;
            Instantiate(effect, pos, rot);
        }
    }
    private void Update()
    {
        life += Time.deltaTime;
        if (life > lifespan) Destroy(gameObject);
    }
}
