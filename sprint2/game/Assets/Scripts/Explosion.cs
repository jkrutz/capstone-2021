using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public float radius = 10.0f;
    public float power = 10.0f;
    public float upForce = 20.0f;

    public GameObject projectile;
    private GameObject bomb;


    // Start is called before the first frame update
    void Start()
    {
        Collider[] colliders = Physics.OverlapSphere(new Vector3(10.0f, 1.0f, 10.0f), radius);
        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();

            if (rb != null)
                rb.AddExplosionForce(power, new Vector3(10.0f, 1.0f, 10.0f), radius, upForce, ForceMode.Impulse);

            Player player = hit.GetComponent<Player>();

            if (player != null)
                player.setHealth(player.getHealth() - 50.0f);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void cast(Vector3 point, GameObject player)
    {
        Vector3 dir = player.transform.position - point;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Quaternion q = Quaternion.Euler(0f, 0f, angle);
        var proj = Instantiate(projectile, player.transform.position + new Vector3(0, 3, 0), Camera.main.transform.rotation);
        proj.GetComponent<bomb>().hitPt = point;
        proj.transform.LookAt(point);
        //bomb.GetComponent<Rigidbody>().velocity = point - playerPos + new Vector3(0, -1f, 0);

//bomb = Instantiate(projectile, playerPos + new Vector3(0, 3, 0), Quaternion.identity);
       // bomb.GetComponent<Rigidbody>().velocity = point - playerPos + new Vector3(0, -1f, 0);
    }
}
