using ShipSail.Interaction;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ShipSail.Ship
{
    /// <summary>
    /// Interactable helm that lets player take/release ship control.
    /// </summary>
    public class ShipHelm : MonoBehaviour, IInteractable
    {
        [SerializeField] private ShipController shipController;
        [SerializeField] private InputActionReference interactAction;
        [SerializeField] private InputActionReference cancelHelmAction;

        private Interactor currentInteractor;

        private void OnEnable()
        {
            interactAction?.action.Enable();
            cancelHelmAction?.action.Enable();

            if (interactAction != null)
            {
                interactAction.action.performed += OnExitRequested;
            }

            if (cancelHelmAction != null)
            {
                cancelHelmAction.action.performed += OnExitRequested;
            }
        }

        private void OnDisable()
        {
            if (interactAction != null)
            {
                interactAction.action.performed -= OnExitRequested;
                interactAction.action.Disable();
            }

            if (cancelHelmAction != null)
            {
                cancelHelmAction.action.performed -= OnExitRequested;
                cancelHelmAction.action.Disable();
            }
        }

        public void Interact(GameObject interactorObject)
        {
            Interactor interactor = interactorObject.GetComponent<Interactor>();
            if (interactor == null || shipController == null)
            {
                return;
            }

            if (currentInteractor == null)
            {
                currentInteractor = interactor;
                currentInteractor.SetPlayerControlEnabled(false);
                shipController.SetControlled(true);
                Debug.Log("Entered ship helm control.");
            }
            else if (currentInteractor == interactor)
            {
                ExitHelmControl();
            }
        }

        public string GetInteractionPrompt()
        {
            return currentInteractor == null ? "Take Helm" : "Leave Helm";
        }

        private void OnExitRequested(InputAction.CallbackContext context)
        {
            if (currentInteractor != null)
            {
                ExitHelmControl();
            }
        }

        private void ExitHelmControl()
        {
            shipController.SetControlled(false);
            currentInteractor.SetPlayerControlEnabled(true);
            currentInteractor = null;
            Debug.Log("Exited ship helm control.");
        }
    }
}
