using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraController : MonoBehaviour
{
    public Vector3 nextPosition;
    public Quaternion nextRotation;

    public float rotationPower = 3f;
    public float rotationLerp = 0.5f;

    public float speed = 1f;

    public GameObject followTransform;
    public Player player;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (!player.GetPaused())
        {
            //Rotate the Follow Target transform based on the input
            float xLook = Input.GetAxis("Mouse X");
            float yLook = Input.GetAxis("Mouse Y");
            followTransform.transform.rotation *= Quaternion.AngleAxis(xLook * rotationPower, Vector3.up);

            followTransform.transform.rotation *= Quaternion.Inverse(Quaternion.AngleAxis(yLook * rotationPower, Vector3.right));

            var angles = followTransform.transform.localEulerAngles;
            angles.z = 0;

            float Xangle = followTransform.transform.localEulerAngles.x;
            float Yangle = followTransform.transform.localEulerAngles.y;

            //Clamp the Up/Down rotation
            float fov = 70;

            if (Xangle > 180 && Xangle < 360 - fov)
            {
                angles.x = 360 - fov;
            }
            else if (Xangle < 180 && Xangle > fov)
            {
                angles.x = fov;
            }
            if (Yangle > 180 && Yangle < 360 - fov)
            {
                angles.y = 360 - fov;
            }
            else if (Yangle < 180 && Yangle > fov)
            {
                angles.y = fov;
            }

            /*
             * if (angle > 180 && angle < 340)
            {
                angles.x = 340;
            }
            else if (angle < 180 && angle > 40)
            {
                angles.x = 40;
            }
             */

            followTransform.transform.localEulerAngles = angles;

            nextRotation = Quaternion.Lerp(followTransform.transform.rotation, nextRotation, Time.deltaTime * rotationLerp);

            // If player is not moving, allow free look around player
            /*
            if (moveVec.x == 0 && moveVec.y == 0)
            {
                nextPosition = transform.position;

                if (Input.GetMouseButtonDown(1))
                {
                    //Set the player rotation based on the look transform
                    transform.rotation = Quaternion.Euler(0, followTransform.transform.rotation.eulerAngles.y, 0);
                    //reset the y rotation of the look transform
                    followTransform.transform.localEulerAngles = new Vector3(angles.x, 0, 0);
                }

                return;
            }    
            */

            //Set the player rotation based on the look transform
            transform.rotation = Quaternion.Euler(0, followTransform.transform.rotation.eulerAngles.y, 0);
            //reset the y rotation of the look transform
            followTransform.transform.localEulerAngles = new Vector3(angles.x, 0, 0);
        }
    }
}
