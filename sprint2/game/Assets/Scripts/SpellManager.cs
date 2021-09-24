using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(GameObject))]
[RequireComponent(typeof(SpellClassifier))]
public class SpellManager : MonoBehaviour
{
    [SerializeField] private PlayerInput spellInput;
    [SerializeField] private PlayerInput debugInput;

    private InputAction aimAction;
    private InputAction mouseAction;
    private InputAction castAction;

    public GameObject fire;
    [SerializeField] private GameObject playerObject;

    public GameObject image;
    private Camera mainCam;
    public Transform parentObj;

    private SpellClassifier classifier;

    private List<Vector2> points;

    private bool firstTime = true;
    private bool drawing = false;

    private bool rightClick = false;
    private bool leftClick = false;

    private string spell;

    void Awake()
    {
        points = new List<Vector2>();
        mainCam = GetComponentInParent<Camera>();
        mouseAction = debugInput.actions["Mouse Position"];
        castAction = spellInput.actions["Cast"];
        aimAction = spellInput.actions["Aim"];
        spell = "None";
    }

    private void OnEnable()
    {
        aimAction.performed += _ => ToggleAim();
        castAction.performed += _ => ToggleCast();
        aimAction.canceled += _ => ToggleAim();

       
        castAction.canceled += _ => ToggleCast();
    }

    private void OnDisable()
    {/*
        aimAction.performed -= _ => ToggleAim();
        

        castAction.performed -= _ => ToggleCast();
        aimAction.canceled -= _ => ToggleAim();
        castAction.canceled -= _ => ToggleCast();
    */}


    void ToggleAim()
    {
        rightClick = !rightClick;
    }

    void ToggleCast()
    {
        leftClick = !leftClick;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Cast: " + leftClick + "--Aim: " + rightClick);
        if (!leftClick && rightClick)
        {
            if (spell == "circle")
            {
                Debug.Log(spell.ToString());
                fire.GetComponent<Fire>().cast();
                spell = "none";
            }
            else if (spell == "star")
            {
                Debug.Log(spell.ToString());
                spell = "none";
            }
            else if (spell == "check")
            {
                Debug.Log(spell.ToString());
                spell = "none";
            }
        }
    }

    private void LateUpdate()
    {

        if (rightClick && leftClick)
        {
            if (firstTime)
            {
                InvokeRepeating("collectPoints", 0.0f, 0.005f);
                firstTime = false;
                drawing = true;
            }
            else
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
                    spell = classifier.Classify(points);

                    //classifier.CreateTemplates(mouseInputPoints, "Assets/Spell_Templates/star.txt");

                    points.Clear();

                    firstTime = true;
                    drawing = false;
                }
            }
        }
    }

    private void collectPoints()
    {
        Vector2 mousePosition = mouseAction.ReadValue<Vector2>();
        Debug.Log(mousePosition.x);
        mousePosition = Vector3.Scale((mainCam.ScreenToViewportPoint(mousePosition) - new Vector3(0.5f, 0.5f, 0.0f)), new Vector3(1038.5f, 636.0f, 1.0f));
        var newElement = Instantiate(image.transform, parentObj) as RectTransform;
        newElement.anchoredPosition = (new Vector3(mousePosition.x, mousePosition.y, 0.0f));
        newElement.SetParent(parentObj);
        points.Add(mousePosition);
    }
}