using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GameObject))]
[RequireComponent(typeof(PlayerController))]
public class CameraController : MonoBehaviour
{
    private GameObject playerObject;
    private PlayerController playerController;
    public Vector3 offset;
    private Vector3 offsetZoomed;
    private Vector3 offsetRegular;
    public float rotateSpeed = 2.0f;
    public Quaternion rotation;
    public float desiredAngleX = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        playerController = GetComponent<PlayerController>();

        offset = playerObject.transform.position - transform.position;
        offsetRegular = offset;
        offsetZoomed = (offset / 2);
    }

    void LateUpdate()
    {
        float h = Input.GetAxisRaw("Mouse X");
        float v = Input.GetAxisRaw("Mouse Y");

        desiredAngleX = desiredAngleX + v;
        if (desiredAngleX < -10.0f)
        {
            desiredAngleX = -10.0f;
        } else if (desiredAngleX > 5.0f)
        {
            desiredAngleX = 5.0f;
        }

        if (Input.GetMouseButton(1))
        {
            offset = offsetZoomed;
            desiredAngleX = 0.0f;
        }
        else
        {
            offset = offsetRegular;
            playerController.CameraRotate(h * rotateSpeed);
        }

        float desiredAngleY = playerObject.transform.eulerAngles.y;
        rotation = Quaternion.Euler(-desiredAngleX * rotateSpeed, desiredAngleY, 0.0f);
        
        transform.position = playerObject.transform.position - (rotation * offset);
        transform.LookAt(playerObject.transform);
    }
}