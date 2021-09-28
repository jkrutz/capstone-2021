using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GameObject))]
[RequireComponent(typeof(SpellClassifier))]
public class SpellManager : MonoBehaviour
{
    public GameObject fireObject;
    private Fire fire;
    public GameObject explosionObject;
    private Explosion explosion;
    public GameObject image;
    public Camera mainCam;
    public Transform parentObj;

    private SpellClassifier classifier;
    public RectTransform canvas;

    private List<Vector2> mouseInputPoints = new List<Vector2>();
    private bool firstTime = true;
    private bool drawing = false;

    private string spell;

    // Start is called before the first frame update
    void Start()
    {
        classifier = GetComponent<SpellClassifier>();

        fire = fireObject.GetComponent<Fire>();
        explosion = explosionObject.GetComponent<Explosion>();
        canvas = GameObject.Find("Canvas").GetComponent<RectTransform>();
        spell = "none";
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Vector3 hitPoint;
            float missDistance = 50;
            if (Physics.Raycast(mainCam.transform.position, mainCam.transform.forward, out hit, Mathf.Infinity))
            {
                hitPoint = hit.point;
            }
            else
            {
                hitPoint = mainCam.transform.position + mainCam.transform.forward * missDistance;
            }

            if (spell == "circle")
            {
                fire.cast(hitPoint);
                
            } else if (spell == "star")
            {
<<<<<<< HEAD
                explosion.cast();
=======
                explosion.cast(hitPoint);
>>>>>>> parent of 35906dd (Disarm)
            } else if (spell == "check")
            {
            }
            spell = "none";
        }
    }

    private void LateUpdate()
    {
        Vector2 mousePosition = Input.mousePosition;
        Cursor.visible = false;
        if (Input.GetMouseButton(1))
        {
            Cursor.visible = true;
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

                    firstTime = true;
                    drawing = false;
                }
            }
        }
    }

    private void collectPoints()
    {
        Vector2 mousePosition = Input.mousePosition;

        mousePosition = Vector3.Scale((mainCam.ScreenToViewportPoint(mousePosition) - new Vector3(0.5f, 0.5f, 0.0f)), new Vector3(canvas.rect.width, canvas.rect.height, 1.0f));
        
        var newElement = Instantiate(image.transform, parentObj) as RectTransform;
        newElement.anchoredPosition = (new Vector3(mousePosition.x, mousePosition.y, 0.0f));
        newElement.SetParent(parentObj);
        mouseInputPoints.Add(mousePosition);
    }
}
