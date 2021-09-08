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
    private Vector3 initOffset = new Vector3(8.0f, 6.0f, -3.0f);
    private Vector3 offsetZoomed;
    private Vector3 offsetRegular;

    public float rotateSpeed = 2.0f;
    public Quaternion rotation;
    public float desiredAngleX = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        playerObject = GameObject.Find("Temp Player");
        playerController = GetComponent<PlayerController>();

        // set zoomed in distance
        offset = playerObject.transform.position - initOffset;
        offsetRegular = offset;
        offsetZoomed = (offset / 2);
    }

    void LateUpdate()
    {
        float h = Input.GetAxisRaw("Mouse X");
        float v = Input.GetAxisRaw("Mouse Y");

        // constraints on looking too far up or down
        desiredAngleX = desiredAngleX + v;
        if (desiredAngleX < -10.0f)
        {
            desiredAngleX = -10.0f;
        } else if (desiredAngleX > 5.0f)
        {
            desiredAngleX = 5.0f;
        }

        // zoom in
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

        // base our camera rotation of player rotation
        float desiredAngleY = playerObject.transform.eulerAngles.y;
        rotation = Quaternion.Euler(-desiredAngleX * rotateSpeed, desiredAngleY, 0.0f);
        
        transform.position = playerObject.transform.position - (rotation * offset);
        transform.LookAt(playerObject.transform);
    }
}