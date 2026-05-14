using UnityEngine;
using UnityEngine.InputSystem;

namespace ShipSail.Player
{
    /// <summary>
    /// Basic first-person movement using CharacterController.
    /// </summary>
    [RequireComponent(typeof(CharacterController))]
    public class FirstPersonController : MonoBehaviour
    {
        [Header("Movement")]
        [SerializeField] private float walkSpeed = 4f;
        [SerializeField] private float sprintSpeed = 7f;
        [SerializeField] private float jumpHeight = 1.2f;
        [SerializeField] private float gravity = -20f;

        [Header("Input Actions")]
        [SerializeField] private InputActionReference moveAction;
        [SerializeField] private InputActionReference jumpAction;
        [SerializeField] private InputActionReference sprintAction;

        private CharacterController characterController;
        private Vector3 verticalVelocity;
        private bool movementEnabled = true;

        public bool MovementEnabled => movementEnabled;

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
            ApplyMovement();
        }

        public void SetMovementEnabled(bool enabled)
        {
            movementEnabled = enabled;
        }

        private void ApplyMovement()
        {
            if (characterController.isGrounded && verticalVelocity.y < 0f)
            {
                verticalVelocity.y = -2f;
            }

            Vector2 moveInput = movementEnabled && moveAction != null
                ? moveAction.action.ReadValue<Vector2>()
                : Vector2.zero;

            bool isSprinting = movementEnabled && sprintAction != null && sprintAction.action.IsPressed();
            float targetSpeed = isSprinting ? sprintSpeed : walkSpeed;

            Vector3 move = transform.right * moveInput.x + transform.forward * moveInput.y;
            characterController.Move(move * targetSpeed * Time.deltaTime);

            if (movementEnabled && characterController.isGrounded && jumpAction != null && jumpAction.action.WasPressedThisFrame())
            {
                verticalVelocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }

            verticalVelocity.y += gravity * Time.deltaTime;
            characterController.Move(verticalVelocity * Time.deltaTime);
        }
    }
}
