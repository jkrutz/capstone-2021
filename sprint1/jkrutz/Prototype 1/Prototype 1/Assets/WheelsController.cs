using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelsController : MonoBehaviour { 

    private float forwardInput;
    private float turnSpeed = 500;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        forwardInput = Input.GetAxis("Vertical");
        if (forwardInput == 0) {
            forwardInput = 0;
        } else if (forwardInput > 0) {
            forwardInput = 1;
        } else {
            forwardInput = -1;
        }
            transform.Rotate(Vector3.right, Time.deltaTime * turnSpeed * forwardInput);

        }
}
