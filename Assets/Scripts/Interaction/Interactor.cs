using ShipSail.Player;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ShipSail.Interaction
{
    /// <summary>
    /// Casts a ray from the camera center and interacts with IInteractable objects.
    /// </summary>
    public class Interactor : MonoBehaviour
    {
        [SerializeField] private Camera playerCamera;
        [SerializeField] private InputActionReference interactAction;
        [SerializeField] private float interactDistance = 4f;
        [SerializeField] private LayerMask interactMask = ~0;
        [SerializeField] private FirstPersonController firstPersonController;
        [SerializeField] private PlayerLook playerLook;

        private void OnEnable()
        {
            interactAction?.action.Enable();
            if (interactAction != null)
            {
                interactAction.action.performed += OnInteract;
            }
        }

        private void OnDisable()
        {
            if (interactAction != null)
            {
                interactAction.action.performed -= OnInteract;
                interactAction.action.Disable();
            }
        }

        private void OnInteract(InputAction.CallbackContext context)
        {
            if (playerCamera == null)
            {
                return;
            }

            Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
            if (Physics.Raycast(ray, out RaycastHit hit, interactDistance, interactMask, QueryTriggerInteraction.Ignore))
            {
                IInteractable interactable = hit.collider.GetComponentInParent<IInteractable>();
                if (interactable != null)
                {
                    interactable.Interact(gameObject);
                }
            }
        }

        public void SetPlayerControlEnabled(bool enabled)
        {
            firstPersonController?.SetMovementEnabled(enabled);
            playerLook?.SetLookEnabled(enabled);
        }
    }
}
