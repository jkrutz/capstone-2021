using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;


public class DebugController : MonoBehaviour
{

    public TextMeshProUGUI infoBox;
    private GameObject playerObject;
    private Player player;
    private RectTransform canvas;

    private PlayerInput debugInput;

    private InputAction debugAction;
    private InputAction lookAction;

    private void Awake()
    {
        debugInput = GetComponent<PlayerInput>();
        lookAction = debugInput.actions["Mouse Position"];
        debugAction = debugInput.actions["Debug"];

        infoBox.enabled = false;
        playerObject = GameObject.Find("Temp Player");
        player = playerObject.GetComponent<Player>();
        canvas = gameObject.GetComponent<RectTransform>();
    }

    private void OnEnable()
    {
        debugAction.performed += _ => ToggleDebugMenu();
    }

    private void OnDisable()
    {
        debugAction.performed -= _ => ToggleDebugMenu();
    }

    private void ToggleDebugMenu()
    {
        infoBox.enabled = !infoBox.isActiveAndEnabled;
    }

        // Update is called once per frame
    void Update()
    {
        Vector3 playerPos = playerObject.transform.position;
        int posX = (int)playerPos.x;
        int posY = (int)playerPos.y;
        int posZ = (int)playerPos.z;

        float playerRot = playerObject.transform.rotation.eulerAngles.y % 360;

        if (playerRot < 0)
        {
            playerRot = 360 + playerRot;
        }

        string playerDir = "";

        if (playerRot < 22.5 && playerRot > -22.5 || playerRot >= 337.5)
        {
            playerDir = "N";
        }
        else if (playerRot >= 22.5 && playerRot < 67.5)
        {
            playerDir = "NE";
        }
        else if (playerRot >= 67.5 && playerRot < 112.5)
        {
            playerDir = "E";
        }
        else if (playerRot >= 112.5 && playerRot < 157.5)
        {
            playerDir = "SE";
        }
        else if (playerRot >= 157.5 && playerRot < 202.5)
        {
            playerDir = "S";
        }
        else if (playerRot >= 202.5 && playerRot < 247.5)
        {
            playerDir = "SW";
        }
        else if (playerRot >= 247.5 && playerRot < 292.5)
        {
            playerDir = "W";
        }
        else if (playerRot >= 292.5 && playerRot < 337.5)
        {
            playerDir = "NW";
        }

        int health = (int)player.getHealth();
        Player.PlayerState state = player.getState();


        Vector2 mousePos = lookAction.ReadValue<Vector2>();
        mousePos.x = Camera.main.nearClipPlane;
        var worldPosition = transform.InverseTransformPoint(Camera.main.ScreenToWorldPoint(mousePos));

        Vector2 localpoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas, mousePos, GetComponentInParent<Canvas>().worldCamera, out localpoint);

        Vector2 normalizedPoint = Rect.PointToNormalized(canvas.rect, localpoint);
        infoBox.SetText(
            "XYZ: " + posX + " " + posY + " " + posZ + "\n" +
            "Direction: " + playerDir + "\n" +
            "Health: " + health + "\n" +
            "Spell: < None >\n" +
            "State: " + state + "\n" +
            "Mouse: (" + normalizedPoint.x * canvas.rect.width + ", " + normalizedPoint.y * canvas.rect.height + ")"
        );
    }
}