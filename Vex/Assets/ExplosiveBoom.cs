using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveBoom : MonoBehaviour
{
    public float power = 10f;
    public float radius = 5f;

    void OnCollisionEnter(Collision collision)
    {

        Rigidbody hitPoint = collision.gameObject.GetComponent<Rigidbody>();
        if (hitPoint != null) {
            hitPoint.AddExplosionForce(power, transform.position, radius, 3.0f);
        }
    }
}
