using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyPositioner : MonoBehaviour
{
    public GameObject head;
    public Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(head.transform.position.x + offset.x, head.transform.position.y + offset.y, head.transform.position.z + offset.z);
    }
}
