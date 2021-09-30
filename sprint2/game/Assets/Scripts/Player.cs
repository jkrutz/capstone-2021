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
    private bool isArmed;
    private string activeSpell;
    private bool isPaused;
    //public bool isComputer;

    public GameObject mainCamera;
    public GameObject aimCamera;

    Vector3 moveVec;

    // Start is called before the first frame update
    void Start()
    {
        activeSpell = "none";
        isArmed = true;
        controller = GetComponent<PlayerController>();
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

        if (rightClickDown && !isPaused)
        {
            mainCamera.SetActive(false);
            aimCamera.SetActive(true);
        }
        if (rightClickRelease && !isPaused)
        {
            mainCamera.SetActive(true);
            aimCamera.SetActive(false);
        }

        Vector3 moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0.0f, Input.GetAxisRaw("Vertical"));
        Vector3 moveDir = transform.TransformDirection(moveInput);
        moveVec = moveDir.normalized * moveSpeed;

        if (!isPaused)
        {
            controller.Move(moveVec);
        }

        if (isGrounded && Input.GetKeyDown(KeyCode.Space) && !isPaused)
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
    }

    public void setHealth(float _health)
    {
        health = _health;
    }

    public float getHealth()
    {
        return health;
    }

    public bool getArmed()
    {
        return isArmed;
    }

    
   public void setArmed(bool _isArmed)
   {
        isArmed = _isArmed;
   }

    public void SetActiveSpell(string _activeSpell)
    {
        activeSpell = _activeSpell;
    }

    public string GetActiveSpell()
    {
        return activeSpell;
    }
    
    public void TogglePaused()
    {
        isPaused = !isPaused;
    }

    public bool GetPaused()
    {
        return isPaused;
    }
}