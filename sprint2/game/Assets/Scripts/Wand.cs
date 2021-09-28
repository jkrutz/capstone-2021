using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wand : MonoBehaviour
{
    private GameObject playerObject;
    private Player player;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        playerObject = GameObject.Find("Temp Player");
        player = playerObject.GetComponent<Player>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!player.getArmed())
        {
            rb.constraints = RigidbodyConstraints.None;
        } else
        {
            setPosition();
        }
    }

    private void setPosition()
    {
        transform.rotation = player.transform.rotation;
        var offset = player.transform.rotation * new Vector3(0.75f, 0.0f, 0.6f);
        transform.position = player.transform.position + offset;
    }

    private void OnCollisionEnter(Collision c)
    {
        if (c.gameObject.tag == "Player" && !player.getArmed())
        {
            setPosition();
            rb.constraints = RigidbodyConstraints.FreezeAll;
            player.setArmed(true);
        }
    }
}
