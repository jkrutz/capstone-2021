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
    private GameObject playerObject;
    public Canvas pauseMenu;

    // Start is called before the first frame update
    void Start()
    {
        playerRigidBody = GetComponent<Rigidbody>();
        playerObject = GameObject.Find("Temp Player");
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            pauseMenu.enabled = !pauseMenu.enabled;
        }
    }
    private void FixedUpdate()
    {
        playerRigidBody.MovePosition(transform.position + velocity * Time.deltaTime);
    }

    public void Move(Vector3 _velocity)
    {
        velocity = _velocity;
    }

    public void Jump(Vector3 jumpVec)
    {
        playerRigidBody.AddForce(jumpVec, ForceMode.Impulse);
    }

    public void Die()
    {
        playerObject.SetActive(false);
    }
}
