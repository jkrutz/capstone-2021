using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class Player : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    public float jumpSpeed = 5.0f;

    private PlayerController controller;
    private MeshRenderer playerMesh;

    private bool isGrounded = true;
    private float health = 100.0f;
    // needs separated into own class
    private float damageDelay = 0.0f;
    private float damageTick = 1.0f;
    private float fireDamage = 10.0f;
    //
    private Color damageColor = Color.red;
    private Color restColor;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<PlayerController>();
        playerMesh = GetComponent<MeshRenderer>();
        restColor = playerMesh.material.color;
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

        if (health <= 0.0f)
        {
            controller.Die();
        }
    }

    private void OnCollisionEnter(Collision c)
    {
        if (c.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }

        // needs reworked
        if (c.gameObject.tag == "Fire")
        {
            InvokeRepeating("TakeDamage", damageDelay, damageTick);
        }
    }

    private void OnCollisionExit(Collision c)
    {
        // needs reworked
        if (c.gameObject.tag == "Fire")
        {
            CancelInvoke("TakeDamage");
        }
    }

    private void TakeDamage()
    {
        // needs reworked
        if (health > 0)
        {
            playerMesh.material.color = damageColor;
            health -= fireDamage;
            Invoke("RestoreColor", damageTick / 4);
        }
    }

    private void RestoreColor()
    {
        playerMesh.material.color = restColor;
    }

    public void setHealth(float _health)
    {
        health = _health;
    }
}
