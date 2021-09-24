using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GameObject))]
public class Fire : MonoBehaviour
{
    private GameObject playerObject;
    private Player player;

    private float fireDamage = 10.0f;
    private float duration = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        playerObject = GameObject.Find("Temp Player");
        player = playerObject.GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        duration -= Time.deltaTime;
        if (duration < 0)
        {
            Destroy(this.gameObject);
            duration = 5.0f;
        }
    }

    private void OnTriggerStay(Collider c)
    {
        if (c.gameObject.tag == "Player")
        {
            player.setHealth(player.getHealth() - fireDamage * Time.deltaTime);
        }
    }

    public void cast(Vector3 point)
    {
        Instantiate(this, point, Quaternion.identity);
    }
}
