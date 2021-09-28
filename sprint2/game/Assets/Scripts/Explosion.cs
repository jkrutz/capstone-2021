using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public float radius = 10.0f;
    public float power = 10.0f;
    public float upForce = 20.0f;

    private GameObject playerObject;

    // Start is called before the first frame update
    void Start()
    {
        playerObject = GameObject.Find("Temp Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void cast(Vector3 point)
    {
        Instantiate(this, point, Quaternion.identity);
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
}
