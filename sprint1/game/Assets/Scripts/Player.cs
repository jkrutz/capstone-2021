using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class Player : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    public float jumpSpeed = 5.0f;
    private PlayerController controller;
    public bool isGrounded = true;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0.0f, Input.GetAxisRaw("Vertical"));
        Vector3 moveDir = transform.TransformDirection(moveInput);
        Vector3 moveVec = moveDir.normalized * moveSpeed;
        controller.Move(moveVec);

        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            controller.Jump(new Vector3(0.0f, jumpSpeed, 0.0f));
            isGrounded = false;
        }
    }

    private void OnCollisionStay(Collision c)
    {
        if (c.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }
    }
}