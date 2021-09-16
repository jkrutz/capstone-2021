using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GameObject))]
[RequireComponent(typeof(SpellClassifier))]
public class SpellManager : MonoBehaviour
{
    public GameObject fire;
    public GameObject point;
    private GameObject playerObject;
    private SpellClassifier classifier;
    private List<Vector2> mouseInputPoints = new List<Vector2>();
    private bool firstTime = true;
    private bool drawing = false;

    private string spell;

    // Start is called before the first frame update
    void Start()
    {
        classifier = GetComponent<SpellClassifier>();
        playerObject = GameObject.Find("Temp Player");
        //cameraController = GetComponentInParent<CameraController>();
        spell = "None";
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 cameraToPlayerVector = playerObject.transform.position;// + (cameraController.rotation * cameraController.offset);

            Vector3 spellTarget = new Vector3(cameraToPlayerVector.x, 0.55f, cameraToPlayerVector.z);
            if (spell == "Fire")
            {
                Instantiate(fire, spellTarget, Quaternion.identity);
                spell = "None";
            }
        }
    }

    private void LateUpdate()
    {
        Vector2 mousePosition = Input.mousePosition;

        if (Input.GetMouseButton(1))
        {
            if (Input.GetMouseButtonDown(0))
            {
                /*
                if (firstTime)
                {
                    InvokeRepeating("collectPoints", 0.0f, 0.01f);
                    firstTime = false;
                    drawing = true;
                }
                */
                float vertBoundary = Screen.height / 2.0f;
                float horizBoundary = Screen.width / 2.0f;
                if (mousePosition.x > 0.0f && mousePosition.x < horizBoundary)
                {
                    if (mousePosition.y > vertBoundary && mousePosition.y < Screen.height)
                    {
                        spell = "Fire";
                    }
                }
                
            } /*else
            {
                if (drawing)
                {
                    CancelInvoke("collectPoints");
                    classifier.Resample(mouseInputPoints, 64);
                    firstTime = true;
                    drawing = false;
                }
            }*/
        }
    }

    private void collectPoints()
    {
        Vector2 mousePosition = Input.mousePosition;

        mouseInputPoints.Add(mousePosition);
    }
}
