using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{

    public GameObject playerObject;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(playerObject == null)
        {
            SpawnPlayer();
        }
    }

    void SpawnPlayer()
    {
        Instantiate(playerObject, transform.position, playerObject.transform.rotation);
    }
}
