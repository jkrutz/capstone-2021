using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disarm : MonoBehaviour
{

    public void cast(GameObject player)
    {
        Player p = player.GetComponent<Player>();
        Rigidbody wand = player.transform.Find("Wand").gameObject.GetComponent<Rigidbody>();
        wand.AddForce(new Vector3(Random.Range(0, 50), Random.Range(0, 50), Random.Range(0, 50)), ForceMode.Impulse);
        p.setArmed(false);
        p.setHealth(p.getHealth() - 5f);
    }
}