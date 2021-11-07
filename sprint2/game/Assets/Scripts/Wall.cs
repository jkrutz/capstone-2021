using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        this.transform.Rotate(new Vector3(-90, 0, 0));
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void cast(Vector3 point, Quaternion direction)
    {
        Instantiate(this, point + new Vector3(0, 2.5f, 0), direction);
    }
}
