using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController), typeof(PlayerInput))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float playerSpeed = 10.0f;
    [SerializeField] private float jumpHeight = 1.5f;
    [SerializeField] private float gravityValue = -16f;
    //[SerializeField] private float rotationSpeed = 50f;
    [SerializeField] private float missDistance = 25f;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform barrelTransform;
    [SerializeField] private Transform bulletParent;
    [SerializeField] private Player player;

    private CharacterController controller;
    private PlayerInput playerInput;

    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private Transform cameraTransform;

    private InputAction moveAction;
    private InputAction jumpAction;
    private InputAction shootAction;
    private InputAction aimAction;

    private bool aiming;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        playerInput = GetComponent<PlayerInput>();
        cameraTransform = Camera.main.transform;
        moveAction = playerInput.actions["Move"];
        jumpAction = playerInput.actions["Jump"];
        shootAction = playerInput.actions["Shoot"];
        aimAction = playerInput.actions["Aim"];
        aiming = false;

        Cursor.lockState = CursorLockMode.Locked;
    }

    private void OnEnable()
    {

        shootAction.performed += _ => CastSpell();
        aimAction.performed += _ => Aiming();
        aimAction.canceled += _ => CancelAiming();
    }

    private void OnDisable()
    {
        shootAction.performed -= _ => CastSpell();
        aimAction.performed -= _ => Aiming();
        aimAction.canceled -= _ => CancelAiming();
    }

    private void Aiming()
    {
        aiming = true;
    }

    private void CancelAiming()
    {
        aiming = false;
    }

    private void CastSpell()
    {
        RaycastHit hit;
        Vector3 pointHit;
        if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out hit, Mathf.Infinity))
        {
            pointHit = hit.point;
        }
        else
        {
            pointHit = cameraTransform.position + cameraTransform.forward * missDistance;
        }
        GameObject.Instantiate(bulletPrefab, pointHit, bulletPrefab.transform.rotation);
    }

    void Update()
    {
        /* States have the following order of precendence
         * Casting
         * Jumping
         * Moving/Resting
         */

        Vector2 input = moveAction.ReadValue<Vector2>();
        Vector3 move = new Vector3(input.x, 0, input.y);
        move = move.x * cameraTransform.right.normalized + move.z * cameraTransform.forward.normalized;
        move.y = 0f;
        controller.Move(move * Time.deltaTime * playerSpeed);

        // Changes the height position of the player..
        if (jumpAction.triggered && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);

        if (move.x == 0 && move.z == 0)
        {
            player.SetState(Player.PlayerState.Resting);
        }
        else
        {
            player.SetState(Player.PlayerState.Moving);
        }

        groundedPlayer = controller.isGrounded;

        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }
        else
        {
            player.SetState(Player.PlayerState.Jumping);
        }

        if (aiming)
        {
            player.SetState(Player.PlayerState.Casting);
        }

        // Rotate towards camera direction
        Quaternion targetRotation = Quaternion.Euler(0, cameraTransform.eulerAngles.y, 0);
        transform.rotation = targetRotation;//Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        //reticle from https://www.kenney.nl/assets/crosshair-pack
        //splat from https://opengameart.org/content/splattexture
    }

    public void Die()
    {
        gameObject.SetActive(false);
    }
}
