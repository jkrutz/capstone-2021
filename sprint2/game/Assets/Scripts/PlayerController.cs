using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(GameObject))]
public class PlayerController : MonoBehaviour
{
    private Vector3 velocity;
    private Rigidbody playerRigidBody;
    public Canvas pauseMenu;
    public float fallMultiplier = 2.5f;

    private Player player;

    // Start is called before the first frame update
    void Start()
    {
        playerRigidBody = GetComponent<Rigidbody>();
        player = GetComponent<Player>();
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            TogglePause();
        }
    }
    private void FixedUpdate()
    {
        if (!player.GetPaused())
        {
            playerRigidBody.MovePosition(transform.position + velocity * Time.deltaTime);
        }
        
    }

    public void TogglePause()
    {
        player.TogglePaused();
        pauseMenu.enabled = player.GetPaused();
    }

    public void Move(Vector3 _velocity)
    {
        velocity = _velocity;
    }

    public void Jump(Vector3 jumpVec)
    {
        playerRigidBody.velocity += Vector3.Scale(Vector3.up, jumpVec);
    }

    public void Die()
    {
        gameObject.SetActive(false);
    }
}
