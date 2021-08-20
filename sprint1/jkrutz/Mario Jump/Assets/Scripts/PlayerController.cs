using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float speed = 5.0f;
    private float horizontalInput;
    private float forwardInput;
    private float range = 5.5f;
    private Rigidbody playerRb;
    private AudioSource playerAudio;
    public float jumpForce = 10f;
    private bool isOnGround = true;

    public AudioClip jumpSound;
    public AudioClip crashSound;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        EnemyController[] enemies = GameObject.FindObjectsOfType<EnemyController>();

        // Get player input
        horizontalInput = Input.GetAxis("Horizontal");
        forwardInput = Input.GetAxis("Vertical");

        if (transform.position.x < -range)
        {
            transform.position = new Vector3(-range, transform.position.y, transform.position.z);
        }
        if (transform.position.x > range)
        {
            transform.position = new Vector3(range, transform.position.y, transform.position.z);
        }
        if (transform.position.z < -range)
        {
            transform.position = new Vector3(transform.position.z, transform.position.y, -range);
        }
        if (transform.position.z > range)
        {
            transform.position = new Vector3(transform.position.z, transform.position.y, range);
        }

        if(transform.position.y < 0)
        {
            for(int i = 0; i < enemies.Length; i++)
            {
                enemies[i].gameOver = true;
            }
            Destroy(gameObject);
        }

        // Move vehicle on vertical input (W, S) then rotate on horizontal (A, D)
        transform.Translate(transform.forward * Time.deltaTime * speed * forwardInput);
        transform.Translate(transform.right * Time.deltaTime * speed * horizontalInput);

        if (Input.GetKeyDown(KeyCode.Space) && isOnGround)
        {
            playerRb.AddForce(Vector3.up * jumpForce/2, ForceMode.Impulse);
            playerAudio.PlayOneShot(jumpSound, 1.0f);
            isOnGround = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
        }
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            playerAudio.PlayOneShot(crashSound, 1.0f);
            Debug.Log("Hit!");
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            Destroy(collision.gameObject.transform.parent.gameObject);
        }

    }
}
