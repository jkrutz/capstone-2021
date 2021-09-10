using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GameObject))]
public class CameraController : MonoBehaviour
{
    private GameObject playerObject;
    private PlayerController playerController;
    public Vector3 offset;
    private Vector3 initOffset = new Vector3(8.0f, 6.0f, -3.0f);
    private Vector3 offsetZoomed;
    private Vector3 offsetRegular;
    private float rotateSpeed = 2.0f;
    public Quaternion rotation;

    private GameObject target;
    private float targetDistance;

    private float rotX;
    private float minTurnAngle = -90.0f;
    private float maxTurnAngle = 0f;

    // Start is called before the first frame update
    void Start()
    {
        /*
        playerObject = GameObject.Find("Temp Player");
        playerController = playerObject.GetComponent<PlayerController>();

        offset = playerObject.transform.position - initOffset;
        offsetRegular = offset;
        offsetZoomed = (offset / 2);
        */
        
        target = GameObject.Find("Temp Player");
        targetDistance = Vector3.Distance(transform.position, target.transform.position);
        target.transform.rotation.SetLookRotation(gameObject.transform.rotation.eulerAngles);
    }

    void LateUpdate()
    {
        float y = Input.GetAxisRaw("Mouse X") * rotateSpeed;
        rotX += Input.GetAxisRaw("Mouse Y") * rotateSpeed;

        rotX = Mathf.Clamp(rotX, minTurnAngle, maxTurnAngle);

        /*
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
        transform.LookAt(playerObject.transform);*/

        transform.eulerAngles = new Vector3(-rotX, transform.eulerAngles.y + y, 0);
        transform.position = target.transform.position - (transform.forward * targetDistance);
    }
}