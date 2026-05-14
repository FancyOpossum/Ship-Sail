using UnityEngine;
using UnityEngine.InputSystem;

namespace ShipSail.Player
{
    /// <summary>
    /// Handles first-person camera and body rotation from mouse/controller look input.
    /// </summary>
    public class PlayerLook : MonoBehaviour
    {
        [SerializeField] private Transform playerBody;
        [SerializeField] private InputActionReference lookAction;
        [SerializeField] private float lookSensitivity = 120f;
        [SerializeField] private float minPitch = -80f;
        [SerializeField] private float maxPitch = 80f;

        private float pitch;
        private bool lookEnabled = true;

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        private void OnEnable()
        {
            lookAction?.action.Enable();
        }

        private void OnDisable()
        {
            lookAction?.action.Disable();
        }

        private void Update()
        {
            if (!lookEnabled || lookAction == null || playerBody == null)
            {
                return;
            }

            Vector2 lookInput = lookAction.action.ReadValue<Vector2>();
            float mouseX = lookInput.x * lookSensitivity * Time.deltaTime;
            float mouseY = lookInput.y * lookSensitivity * Time.deltaTime;

            pitch -= mouseY;
            pitch = Mathf.Clamp(pitch, minPitch, maxPitch);

            transform.localRotation = Quaternion.Euler(pitch, 0f, 0f);
            playerBody.Rotate(Vector3.up * mouseX);
        }

        public void SetLookEnabled(bool enabled)
        {
            lookEnabled = enabled;
        }
    }
}
