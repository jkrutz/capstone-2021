using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private GameObject playerObject;
    private Player player;

    // Start is called before the first frame update
    void Start()
    {
        playerObject = GameObject.Find("Temp Player");
        player = playerObject.GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!playerObject.activeInHierarchy)
        {
            player.CancelInvoke("TakeDamage");
            playerObject.transform.position = new Vector3(7.0f, 2.0f, 6.0f);
            playerObject.SetActive(true);
            player.setHealth(100.0f);
        }
    }
}
