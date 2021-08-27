using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GameObject))]
public class SpellManager : MonoBehaviour
{
    public GameObject fire;
    private GameObject playerObject;
    public CameraController cameraController;

    private string spell;
    private Vector3 mousePosition;

    // Start is called before the first frame update
    void Start()
    {
        playerObject = GameObject.Find("Temp Player");
        cameraController = GetComponentInParent<CameraController>();
        spell = "None";
    }

    // Update is called once per frame
    void Update()
    {
        mousePosition = Input.mousePosition;

        if (Input.GetMouseButtonDown(0))
        {
            Vector3 cameraToPlayerVector = playerObject.transform.position + (cameraController.rotation * cameraController.offset);

            Vector3 spellTarget = new Vector3(cameraToPlayerVector.x, 0.55f, cameraToPlayerVector.z);
            if (spell == "Fire")
            {
                Instantiate(fire, spellTarget, Quaternion.identity);
            }
        }
    }

    private void LateUpdate()
    {
        mousePosition = Input.mousePosition;

        if (Input.GetMouseButton(1))
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (mousePosition.x > 0.0f && mousePosition.x < 950.0f)
                {
                    if (mousePosition.y > 450.0f && mousePosition.y < 900.0f)
                    {
                        spell = "Fire";
                    }
                }
            }
        }
    }
}
