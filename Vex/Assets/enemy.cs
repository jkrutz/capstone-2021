using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    private float health = 100f;
    private Rigidbody rb;
    private Animator anim;

    public float speed;

    float timer = 3.0f;
    bool stop = true;

    Vector3 randomDirection;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        if (timer < 0)
        {
            speed = 0.05f;
            if (stop)
            {
                speed = 0;
                anim.SetFloat("move", 0);
                stop = false;
            }
            else
            {
                anim.SetFloat("move", 1);
                transform.Rotate(new Vector3(transform.rotation.x, Random.rotation.eulerAngles.y, transform.rotation.z));
                stop = true;
            }
            timer = Random.Range(0.5f, 3.0f);
        }

        rb.MovePosition(transform.position + (transform.forward * speed));
    }
}
