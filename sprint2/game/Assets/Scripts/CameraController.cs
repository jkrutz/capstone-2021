using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GameObject))]
public class CameraController : MonoBehaviour
{
    private Transform target;
    private float targetDistance;
    public Vector3 offset;

    public float damping = 1;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("Temp Player").transform;
        offset = target.position - transform.position;
    }

    void LateUpdate()
    {
        float desiredAngle = target.eulerAngles.y;
        Quaternion rotation = Quaternion.Euler(0, desiredAngle, 0);

        transform.position = target.position - (rotation * offset);
        transform.LookAt(target);

/*
        float y = Input.GetAxisRaw("Mouse X") * rotateSpeed;
        rotX += Input.GetAxisRaw("Mouse Y") * rotateSpeed;

        rotX = Mathf.Clamp(rotX, minTurnAngle, maxTurnAngle);

        
        if (Input.GetMouseButton(1))
        {
            offset = offsetZoomed;
            v = 0.0f;
        }
        else
        {
            offset = offsetRegular;
            playerController.CameraRotate(h * rotateSpeed);
        }

        rotation = Quaternion.Euler(-v * rotateSpeed, h, 0.0f);
        
        transform.position = playerObject.transform.position - (rotation * offset);
        transform.LookAt(playerObject.transform);

        //transform.eulerAngles = new Vector3(-rotX, transform.eulerAngles.y + y, 0);
        transform.position = target.transform.position - (transform.forward * targetDistance);
        transform.LookAt(target.transform);*/
    }
}