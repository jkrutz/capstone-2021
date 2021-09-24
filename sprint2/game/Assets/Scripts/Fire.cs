using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GameObject))]
public class Fire : MonoBehaviour
{
    public GameObject playerObject;
    private Player player;

    private float fireDamage = 10.0f;
    private float duration = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
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

    private void OnCollisionStay(Collision c)
    {
        if (c.gameObject.tag == "Player")
        {
            player.setHealth(player.getHealth() - fireDamage * Time.deltaTime);
        }
    }

    public void cast()
    {
        Instantiate(this, new Vector3(0.0f, 3.0f, 0.0f), Quaternion.identity);
    }
}