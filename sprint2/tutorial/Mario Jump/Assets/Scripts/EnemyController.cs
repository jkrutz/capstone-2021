using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    public float speed = 3;
    private Rigidbody enemyRb;
    private GameObject player;
    private float startDelay = 0.1f;

    public bool gameOver;

    // Start is called before the first frame update
    void Start()
    { 
        enemyRb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
        Invoke("GenerateForce", startDelay);
        gameOver = false;
    }
     
    // Update is called once per frame
    void Update()
    {
        if(!gameOver)
        {
            startDelay = Random.Range(1, 2.5f);
            Invoke("GenerateForce", startDelay);
        }
        
    }

    private void GenerateForce()
    {
        float forceX = Random.Range(-speed, speed);
        float forceZ = Random.Range(-speed, speed);
        enemyRb.AddForce((new Vector3(forceX, 0, forceZ)).normalized * speed, ForceMode.Impulse);
    }
}
