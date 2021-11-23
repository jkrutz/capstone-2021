using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bomb : MonoBehaviour
{
    public GameObject explosion;
    public Vector3 hitPt;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnParticleCollision(GameObject collision)
    {
        Instantiate(explosion, hitPt, Quaternion.identity);
        Destroy(this.gameObject);
    }
}
