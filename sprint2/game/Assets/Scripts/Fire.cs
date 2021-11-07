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
    private int numFires;

    private Vector3 pos;

    // Start is called before the first frame update
    void Start()
    {
        playerObject = GameObject.Find("Temp Player");
        player = playerObject.GetComponent<Player>();
        numFires = 0;
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

        if (numFires > 5)
        {
            CancelInvoke();
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
        pos = point;
        InvokeRepeating(nameof(spawnfire), 0f, 1f);
    }

    private void spawnfire()
    {
        Instantiate(this, pos + new Vector3(Random.Range(-5f, 5f), 3, Random.Range(-5f, 5f)), Quaternion.identity);
        numFires++;
    }
}
