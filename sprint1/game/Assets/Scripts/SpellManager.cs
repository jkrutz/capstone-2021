using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GameObject))]
public class SpellManager : MonoBehaviour
{
    public GameObject fire;
    public GameObject point;
    private GameObject playerObject;
    public CameraController cameraController;
    private float pointTimer;
    private float lastInterval = 0.0f;
    private float interval = 0.01f;
    private List<Vector2> mouseInputPoints = new List<Vector2>();
    private bool drawing = false;

    private string spell;

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
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 cameraToPlayerVector = playerObject.transform.position + (cameraController.rotation * cameraController.offset);

            Vector3 spellTarget = new Vector3(cameraToPlayerVector.x, 0.55f, cameraToPlayerVector.z);
            if (spell == "Fire")
            {
                Instantiate(fire, spellTarget, Quaternion.identity);
                spell = "None";
            }
        }

        pointTimer += Time.deltaTime;
    }

    private void LateUpdate()
    {
        Vector2 mousePosition = Input.mousePosition;

        if (Input.GetMouseButton(1))
        {
            if (Input.GetMouseButton(0))
            {
                drawing = true;
                if (shouldCollectNewPoint())
                {
                    mouseInputPoints.Add(mousePosition);
                }
                /*
                if (mousePosition.x > 0.0f && mousePosition.x < 950.0f)
                {
                    if (mousePosition.y > 450.0f && mousePosition.y < 900.0f)
                    {
                        spell = "Fire";
                    }
                }
                */
            } else
            {
                drawing = false;
            }
        }
    }

    private bool shouldCollectNewPoint()
    {
        bool a = false;

        if (pointTimer - lastInterval > interval)
        {
            a = true;
            lastInterval = pointTimer;
        }

        return a;
    }
}
