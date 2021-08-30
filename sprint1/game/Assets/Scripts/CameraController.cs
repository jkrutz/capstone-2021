using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GameObject))]
[RequireComponent(typeof(PlayerController))]
public class CameraController : MonoBehaviour
{
    public GameObject player;
    private PlayerController playerController;
    private Vector3 offset;
    public float rotateSpeed = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        playerController = GetComponent<PlayerController>();

        offset = player.transform.position - transform.position;
    }

    void LateUpdate()
    {
        float h = Input.GetAxisRaw("Mouse X");
        playerController.CameraRotate(h * rotateSpeed);

        float desiredAngleY = player.transform.eulerAngles.y;
        Quaternion rotation = Quaternion.Euler(0.0f, desiredAngleY, 0.0f);

        transform.position = player.transform.position - (rotation * offset);
        transform.LookAt(player.transform);
    }
}