using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using TMPro;


public class ContinuousMovement : MonoBehaviour
{
    public XRNode moveSource;
    public XRNode rotSource;
    public float speed;
    public float gravity;
    public int rotAmount = 45;
    public float turnDelay = 0.5f;
    public LayerMask groundLayer;
    public float additionalHeight = 0.20f;
    public TMP_Text snapText;

    private float fallingSpeed;
    private Vector2 moveAxis;
    private Vector2 rotAxis;
    private CharacterController character;
    public Camera rig;
    public bool isGrounded;

    private bool turnLeft = false;
    private bool turnRight = false;

    // Start is called before the first frame update
    void Start()
    {
        character = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        InputDevice moveDevice = InputDevices.GetDeviceAtXRNode(moveSource);
        moveDevice.TryGetFeatureValue(CommonUsages.primary2DAxis, out moveAxis);
        InputDevice rotDevice = InputDevices.GetDeviceAtXRNode(rotSource);
        rotDevice.TryGetFeatureValue(CommonUsages.primary2DAxis, out rotAxis);
    }

    private void LateUpdate()
    {
        if (rotAxis.x < -.2 && !turnLeft)
        {
            turnLeft = true;
            if (IsInvoking("SnapTurnRight"))
            {
                CancelInvoke("SnapTurnRight");
            }
            InvokeRepeating("SnapTurnLeft", 0, turnDelay);
            turnRight = false;
        }
        else if (rotAxis.x > .2 && !turnRight)
        {
            if (IsInvoking("SnapTurnLeft"))
            {
                CancelInvoke("SnapTurnLeft");
            }
            InvokeRepeating("SnapTurnRight", 0, turnDelay);
            turnRight = true;
            turnLeft = false;
        }
        else if (rotAxis.x > -.2 && rotAxis.x < .2)
        {
            CancelInvoke("SnapTurnLeft");
            CancelInvoke("SnapTurnRight");
            turnRight = false;
            turnLeft = false;

        }
        CapsuleFollowHeadset();
        Quaternion headYaw = Quaternion.Euler(0, rig.transform.eulerAngles.y, 0);
        Vector3 direction = headYaw * new Vector3(moveAxis.x, 0, moveAxis.y);

        //gravity
        isGrounded = CheckIfGrounded();
        if (isGrounded)
        {
            fallingSpeed = 0;
        }
        else
        {
            fallingSpeed += gravity * Time.deltaTime;
        }
        //print(isGrounded + " " + Vector3.up * fallingSpeed);
        character.Move(Vector3.up * fallingSpeed + direction * Time.deltaTime * speed);
    }

    void SnapTurnLeft()
    {
        transform.Rotate(new Vector3(0, -rotAmount, 0));
    }

    void SnapTurnRight()
    {
        transform.Rotate(new Vector3(0, rotAmount, 0));
    }

    void CapsuleFollowHeadset()
    {
        character.height = rig.transform.localPosition.y + additionalHeight;
        Vector3 capsuleCenter = transform.InverseTransformPoint(rig.transform.position);
        character.center = new Vector3(capsuleCenter.x, character.height / 2 + character.skinWidth, capsuleCenter.z);
    }

    bool CheckIfGrounded()
    {
        //tells us if on ground
        Vector3 rayStart = transform.TransformPoint(character.center);

        bool hasHit = Physics.Raycast(new Ray(rayStart, Vector3.down), character.height * 2);

        return hasHit;
    }


    /*
    public void ChangeTurnAmount(DeviceBasedSnapTurnProvider snapCode)
    {
        float[] snapAmounts = { 30, 45, 60, 90 };
        float curSnap = snapCode.turnAmount;
        int index = 0;
        for (int i = 0; i < snapAmounts.Length; i++)
        {
            if (snapAmounts[i] == curSnap)
            {
                index = (i + 1) % snapAmounts.Length;
            }
        }

        snapCode.turnAmount = snapAmounts[index];
        snapText.text = snapAmounts[index] + "°\nSnap Turn";
    }*/
}
