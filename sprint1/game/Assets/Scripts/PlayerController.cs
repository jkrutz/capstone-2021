using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(GameObject))]
public class PlayerController : MonoBehaviour
{
    private Vector3 velocity;
    private Rigidbody playerRigidBody;
    private GameObject playerObject;

    // Start is called before the first frame update
    void Start()
    {
        playerRigidBody = GetComponent<Rigidbody>();
        playerObject = GameObject.Find("Temp Player");
    }

    void FixedUpdate()
    {
        playerRigidBody.MovePosition(transform.position + velocity * Time.deltaTime);
    }

    public void Move(Vector3 _velocity)
    {
        velocity = _velocity;
    }

    public void CameraRotate(float h)
    {
        playerObject.transform.Rotate(0.0f, h, 0.0f);
    }

    public void Jump(Vector3 jumpVec)
    {
        playerRigidBody.AddForce(jumpVec, ForceMode.Impulse);
    }
}
