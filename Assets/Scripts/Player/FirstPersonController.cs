using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class FirstPersonController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float walkSpeed = 4f;
    [SerializeField] private float sprintSpeed = 6f;

    [Header("Jumping & Gravity")]
    [SerializeField] private float jumpHeight = 1.2f;
    [SerializeField] private float gravity = -15f;
    [SerializeField] private float groundedVerticalReset = -2f;

    [Header("Input")]
    [SerializeField] private InputActionReference moveAction;
    [SerializeField] private InputActionReference jumpAction;
    [SerializeField] private InputActionReference sprintAction;

    private CharacterController characterController;
    private float verticalVelocity;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void OnEnable()
    {
        moveAction?.action.Enable();
        jumpAction?.action.Enable();
        sprintAction?.action.Enable();
    }

    private void OnDisable()
    {
        moveAction?.action.Disable();
        jumpAction?.action.Disable();
        sprintAction?.action.Disable();
    }

    private void Update()
    {
        bool grounded = characterController.isGrounded;

        if (grounded && verticalVelocity < 0f)
        {
            verticalVelocity = groundedVerticalReset;
        }

        if (grounded && jumpAction != null && jumpAction.action.WasPressedThisFrame())
        {
            verticalVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        Vector2 moveInput = moveAction != null ? moveAction.action.ReadValue<Vector2>() : Vector2.zero;
        bool sprinting = sprintAction != null && sprintAction.action.IsPressed();
        float currentSpeed = sprinting ? sprintSpeed : walkSpeed;

        Vector3 horizontal = (transform.right * moveInput.x + transform.forward * moveInput.y) * currentSpeed;

        verticalVelocity += gravity * Time.deltaTime;

        Vector3 velocity = horizontal + Vector3.up * verticalVelocity;
        characterController.Move(velocity * Time.deltaTime);
    }
}
