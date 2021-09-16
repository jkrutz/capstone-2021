using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GameObject))]
public class Fire : MonoBehaviour
{
    private GameObject playerObject;
    private Player player;

    private float fireDamage = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        playerObject = GameObject.Find("Temp Player");
        player = playerObject.GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionStay(Collision c)
    {
        if (c.gameObject.tag == "Player")
        {
            player.setHealth(player.health - fireDamage * Time.deltaTime);
        }
    }
}
