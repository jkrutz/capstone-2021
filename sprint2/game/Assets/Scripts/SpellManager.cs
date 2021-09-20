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

    public GameObject image;
    public Camera mainCam;
    public Transform parentObj;

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
        spell = "None";
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
        }
    }

    private void LateUpdate()
    {
        Vector2 mousePosition = Input.mousePosition;

        if (Input.GetMouseButton(1))
        {
            if (Input.GetMouseButton(0))
            {
                if (firstTime)
                {
                    InvokeRepeating("collectPoints", 0.0f, 0.005f);
                    firstTime = false;
                    drawing = true;
                }
            } else
            {
                if (drawing)
                {
                    CancelInvoke("collectPoints");
                    foreach (Transform child in parentObj.transform)
                    {
                        Destroy(child.gameObject);
                    }

                    /*
                     * To run the game:
                     * 1) run the classify function with create templates commented out
                     * 2) ensure you've commented out and uncommented the correct templates in SpellClassifier
                     * 
                     * To create spells:
                     * 1) uncomment and run the classifier.CreateTemplates() function below
                     * 2) comment out the spell = classifier.Classify() function below
                     * 2) add in a read teamplate call in SpellClassifier for the new template
                     * 3) go into the template file and delete the empty line. Should be 64 lines in file total.
                     */
                    spell = classifier.Classify(mouseInputPoints);

                    //classifier.CreateTemplates(mouseInputPoints, "Assets/Spell_Templates/star.txt");
                    
                    mouseInputPoints.Clear();

                    Debug.Log(spell);

                    firstTime = true;
                    drawing = false;
                }
            }
        }
    }

    private void collectPoints()
    {
        Vector2 mousePosition = Input.mousePosition;

        mousePosition = Vector3.Scale((mainCam.ScreenToViewportPoint(mousePosition) - new Vector3(0.5f, 0.5f, 0.0f)), new Vector3(1038.5f, 636.0f, 1.0f));
        var newElement = Instantiate(image.transform, parentObj) as RectTransform;
        newElement.anchoredPosition = (new Vector3(mousePosition.x, mousePosition.y, 0.0f));
        newElement.SetParent(parentObj);
        mouseInputPoints.Add(mousePosition);
    }
}
