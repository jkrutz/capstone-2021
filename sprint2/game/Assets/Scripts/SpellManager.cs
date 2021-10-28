using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine.UI;
using UnityEngine;

[RequireComponent(typeof(GameObject))]
[RequireComponent(typeof(SpellClassifier))]
public class SpellManager : MonoBehaviour
{
    public GameObject fireObject;
    public GameObject disarmObject;
    public GameObject wallObject;
    public GameObject playerObject;
    public Player player;
    private Fire fire;
    private Wall wall;
    private Disarm disarm;
    public GameObject explosionObject;
    private Explosion explosion;
    public Image image;
    public Camera mainCam;
    public Canvas canvas;
    private Transform canvasTransform;

    private SpellClassifier classifier;

    public Dropdown dropdown;

    private List<Vector2> mouseInputPoints = new List<Vector2>();
    private bool firstTime = true;
    private bool drawing = false;

    private string spell;

    // Start is called before the first frame update
    void Start()
    {
        classifier = GetComponent<SpellClassifier>();
        disarm = disarmObject.GetComponent<Disarm>();
        fire = fireObject.GetComponent<Fire>();
        wall = wallObject.GetComponent<Wall>();
        explosion = explosionObject.GetComponent<Explosion>();
        canvasTransform = canvas.transform;
        
        spell = "none";
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !player.GetPaused())
        {
            RaycastHit hit;
            Vector3 hitPoint;
            float missDistance = 50;
            bool playerWasHit = false;
            GameObject hitEntity = null;

            if (Physics.Raycast(mainCam.transform.position, mainCam.transform.forward, out hit, Mathf.Infinity))
            {
                hitPoint = hit.point;
                hitEntity = hit.collider.gameObject;
                playerWasHit = hitEntity.CompareTag("Player");
            }
            else
            {
                hitPoint = mainCam.transform.position + mainCam.transform.forward * missDistance;
            }

            if (player.getArmed())
            {
                if (spell == "circle")
                {
                    fire.cast(hitPoint);

                }
                else if (spell == "star")
                {
                    explosion.cast(hitPoint);
                }
                else if (spell == "check")
                {
                    if (playerWasHit)
                    {
                        disarm.cast(hitEntity);
                    }

                }
                else if (spell == "spiral")
                {
                    wall.cast(new Vector3(hitPoint.x, 0, hitPoint.z), playerObject.transform.rotation);
                }
            }
            spell = "none";
            player.SetActiveSpell(spell);
        }
    }

    private void LateUpdate()
    {
        if (!player.GetPaused())
        {
            //Cursor.visible = false;
        }

        //if (Input.GetMouseButton(1) && !player.GetPaused())
        if (!player.GetPaused())
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
                    foreach (Transform child in canvasTransform)
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

                    /*bool isNumeric = true;
                    foreach(Vector2 pt in mouseInputPoints)
                    {
                        float ptx = pt.x;
                        float pty = pt.y;
                        if(ptx < 0)
                        {
                            ptx *= -1;
                        }
                        if(pty < 0)
                        {
                            pty *= -1;
                        }
                        isNumeric = (!float.IsPositiveInfinity(ptx)) && (!float.IsNaN(ptx)) &&
                            (!float.IsPositiveInfinity(pty)) && (!float.IsNaN(pty));
                        if (!isNumeric)
                        {
                            break;
                        }
                        Debug.Log(ptx + " " + pty);
                    }

                    if (isNumeric)
                    {
                        int num = 1;
                        string template_type = dropdown.options[dropdown.value].text;
                        var path = "Assets/Spell_Templates/" + template_type + "/" + template_type + num + ".txt";
                        while (File.Exists(path))
                        {
                            num++;
                            path = "Assets/Spell_Templates/" + template_type + "/" + template_type + num + ".txt";
                        }
                        classifier.CreateTemplates(mouseInputPoints, path);
                    }
                    else
                    {
                        print("Point could not be saved");
                    }*/
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
  
        Image newElement = Instantiate(image, canvasTransform) as Image;
        newElement.transform.position = (new Vector3(mousePosition.x, mousePosition.y, 0.0f));
        mousePosition.x -= Screen.width / 2;
        mousePosition.y -= Screen.height / 2;
        mouseInputPoints.Add(mousePosition);
    }
}
