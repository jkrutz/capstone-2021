using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.XR;
using TMPro;

public class DrawSpell : MonoBehaviour
{
    public XRNode drawHand;
    public int penSize = 5;
    public float maxRay = 10;
    public LayerMask layerMask;
    public Material material;
    public Material magicMaterial, fireMaterial, waterMaterial;
    public ParticleSystem magicalParticles;
    public ParticleSystem fireParticles;
    public ParticleSystem waterParticles;
    public GameObject bullet;
    public string spell;
    public GameObject cursor;

    private List<Vector2> inputPoints = new List<Vector2>();
    private Color[] _colors;
    private RaycastHit _touch;
    private CanvasDrawer canvasDrawer;
    private Vector2 _touchPos;
    private Vector2 _lastTouchPos;
    private bool _touchedLastFrame;
    private bool stoppedDrawing = true;
    private Classifier classifier;
    private bool awaitingCast;
    // Start is called before the first frame update
    void Start()
    {
        var color = new Color(material.color.r, material.color.g, material.color.b, 0);
        _colors = Enumerable.Repeat(color, penSize * penSize).ToArray();
        classifier = GetComponent<Classifier>();
        magicalParticles.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        InputDevice device = InputDevices.GetDeviceAtXRNode(drawHand);
        device.TryGetFeatureValue(CommonUsages.triggerButton, out bool drawing);
        if (drawing)
        {
            if (stoppedDrawing)
            {
                stoppedDrawing = false;
                awaitingCast = false;
                magicalParticles.Stop();
                fireParticles.Stop();
                waterParticles.Stop();
            }
            Draw();
        }
        else if (awaitingCast)
        {
            device.TryGetFeatureValue(CommonUsages.secondaryButton, out bool shoot);
            if (shoot)
            {
                Shoot(spell);
                magicalParticles.Stop();
                fireParticles.Stop();
                waterParticles.Stop();
                awaitingCast = false;
            }
        }
        else
        {
            if (!stoppedDrawing)
            {
                magicalParticles.Stop();
                fireParticles.Stop();
                waterParticles.Stop();
                canvasDrawer.ClearTexture();
                _touchedLastFrame = false;
                stoppedDrawing = true;
                if (inputPoints.Count > 10)
                {
                    spell = classifier.Classify(inputPoints);
                    var output = classifier.output;
                    

                    switch (spell)
                    {
                        case ("circle"):
                            magicalParticles.Play();
                            awaitingCast = true;
                            break;
                        case ("star"):
                            fireParticles.Play();
                            awaitingCast = true;
                            break;
                        case ("square"):
                            waterParticles.Play();
                            awaitingCast = true;
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    print(inputPoints.Count);
                }
                inputPoints.Clear();
            }
        }
    }

    private void Draw()
    {
        if (Physics.Raycast(transform.position, transform.forward, out _touch, maxRay, ~layerMask))
        {
            if (_touch.transform.CompareTag("Drawing Canvas"))
            {
                if (canvasDrawer == null)
                {
                    canvasDrawer = _touch.transform.GetComponent<CanvasDrawer>();
                }

                _touchPos = new Vector2(_touch.textureCoord.x, _touch.textureCoord.y);

                var x = (int)(_touchPos.x * canvasDrawer.textureSize.x - (penSize / 2));
                var y = (int)(_touchPos.y * canvasDrawer.textureSize.y - (penSize / 2));

                if (y < 0 || y > canvasDrawer.textureSize.y || x < 0 || x > canvasDrawer.textureSize.x)
                {
                    return;
                }
                if (_touchedLastFrame)
                {
                    canvasDrawer.texture.SetPixels(x, y, penSize, penSize, _colors);

                    for (float f = 0.01f; f < 1.00f; f += 0.01f)
                    {
                        var lerpX = (int)Mathf.Lerp(_lastTouchPos.x, x, f);
                        var lerpY = (int)Mathf.Lerp(_lastTouchPos.y, y, f);
                        canvasDrawer.texture.SetPixels(lerpX, lerpY, penSize, penSize, _colors);
                        cursor.transform.position = new Vector3(lerpX, lerpY, 0);//_touch.textureCoord.x, _touch.textureCoord.y, _touch.textureCoord.z);
                        print(_colors[0].a);
                    }

                    canvasDrawer.texture.Apply();
                }
                inputPoints.Add(new Vector2(x, y));
                _lastTouchPos = new Vector2(x, y);
                _touchedLastFrame = true;
                return;
            }
        }

        canvasDrawer = null;
        _touchedLastFrame = false;
    }

    private void Shoot(string spell)
    {
        print("shot");
        GameObject fired = Instantiate(bullet, transform.position, transform.rotation);
        switch (spell)
        {
            case ("circle"):
                fired.GetComponent<MeshRenderer>().material = magicMaterial;
                break;
            case ("star"):
                fired.GetComponent<MeshRenderer>().material = fireMaterial;
                break;
            case ("square"):
                fired.GetComponent<MeshRenderer>().material = waterMaterial;
                break;
            default:
                break;
        }
        fired.GetComponent<Rigidbody>().AddForce(transform.forward* 1000);
        Destroy(fired, 3);

        awaitingCast = true;
    }
}