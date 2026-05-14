using UnityEngine;
using UnityEngine.InputSystem;

namespace ShipSail.Combat
{
    /// <summary>
    /// Placeholder sword attack using a short raycast.
    /// </summary>
    public class SwordSwing : MonoBehaviour
    {
        [SerializeField] private Camera playerCamera;
        [SerializeField] private InputActionReference attackAction;
        [SerializeField] private float attackRange = 2f;
        [SerializeField] private LayerMask hitMask = ~0;

        private void OnEnable()
        {
            attackAction?.action.Enable();
            if (attackAction != null)
            {
                attackAction.action.performed += OnAttack;
            }
        }

        private void OnDisable()
        {
            if (attackAction != null)
            {
                attackAction.action.performed -= OnAttack;
                attackAction.action.Disable();
            }
        }

        private void OnAttack(InputAction.CallbackContext context)
        {
            if (playerCamera == null)
            {
                return;
            }

            Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
            if (Physics.Raycast(ray, out RaycastHit hit, attackRange, hitMask, QueryTriggerInteraction.Ignore))
            {
                Debug.Log($"Sword swing hit: {hit.collider.name}");
            }
            else
            {
                Debug.Log("Sword swing missed.");
            }
        }
    }
}
