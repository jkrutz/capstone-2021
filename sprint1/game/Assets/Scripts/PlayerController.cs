using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    private Vector3 velocity;
    private Rigidbody playerRigidBody;

    // Start is called before the first frame update
    void Start()
    {
        playerRigidBody = GetComponent<Rigidbody>();
    }

    void LateUpdate()
    {
        playerRigidBody.velocity = velocity;
    }

    public void Move(Vector3 _velocity)
    {
        velocity = _velocity;
    }
}
