using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class Player : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    public float jumpSpeed = 5.0f;

    private PlayerController controller;

    private float health = 100.0f;
    private bool isGrounded = true;

    public GameObject mainCamera;
    public GameObject aimCamera;

    Vector3 moveVec;

    public enum PlayerState
    {
        Casting,
        Resting,
        Moving,
        Jumping
    }

    private PlayerState state;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<PlayerController>();
        state = PlayerState.Resting;
    }

    // Update is called once per frame
    void Update()
    {
        bool rightClickDown = Input.GetMouseButtonDown(1);
        bool rightClickRelease = Input.GetMouseButtonUp(1);
        if (health <= 0.0f)
        {
            controller.Die();
        }

        if (rightClickDown)
        {
            mainCamera.SetActive(false);
            aimCamera.SetActive(true);
            state = PlayerState.Casting;
        }
        if (rightClickRelease)
        {
            mainCamera.SetActive(true);
            aimCamera.SetActive(false);
            state = PlayerState.Resting;
        }

        Vector3 moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0.0f, Input.GetAxisRaw("Vertical"));
        Vector3 moveDir = transform.TransformDirection(moveInput);
        moveVec = moveDir.normalized * moveSpeed;

        if ((moveVec.x != 0 || moveVec.y != 0) && (state != PlayerState.Casting && isGrounded))
        {
            state = PlayerState.Moving;
        }
        else if ((moveVec.x == 0 || moveVec.y == 0) && (state != PlayerState.Casting && isGrounded))
        {
            state = PlayerState.Resting;
        }
        controller.Move(moveVec);

        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            controller.Jump(new Vector3(0.0f, jumpSpeed, 0.0f));
            isGrounded = false;
            if (state != PlayerState.Casting)
            {
                state = PlayerState.Jumping;
            }
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
            if (state == PlayerState.Jumping)
            {
                state = PlayerState.Resting;
            }
        }
    }

    private void OnCollisionExit(Collision c)
    {
        if (c.gameObject.tag == "Fire")
        {
            CancelInvoke("TakeDamage");
        }
    }

    public void setHealth(float _health)
    {
        health = _health;
    }

    public float getHealth()
    {
        return health;
    }

    public void SetState(PlayerState _state)
    {
        state = _state;
    }

    public PlayerState getState()
    {
        return state;
    }
}
