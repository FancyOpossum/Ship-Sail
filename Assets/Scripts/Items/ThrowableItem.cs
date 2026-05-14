using UnityEngine;
using UnityEngine.InputSystem;

namespace ShipSail.Items
{
    /// <summary>
    /// Spawns and throws a Rigidbody prefab (ex: fruit) from the camera forward direction.
    /// </summary>
    public class ThrowableItem : MonoBehaviour
    {
        [SerializeField] private Camera playerCamera;
        [SerializeField] private InputActionReference previousAction;
        [SerializeField] private InputActionReference nextAction;
        [SerializeField] private Rigidbody throwablePrefab;
        [SerializeField] private float spawnForwardOffset = 0.6f;
        [SerializeField] private float throwForce = 16f;

        private void OnEnable()
        {
            previousAction?.action.Enable();
            nextAction?.action.Enable();

            if (previousAction != null)
            {
                previousAction.action.performed += OnThrowRequested;
            }

            if (nextAction != null)
            {
                nextAction.action.performed += OnThrowRequested;
            }
        }

        private void OnDisable()
        {
            if (previousAction != null)
            {
                previousAction.action.performed -= OnThrowRequested;
                previousAction.action.Disable();
            }

            if (nextAction != null)
            {
                nextAction.action.performed -= OnThrowRequested;
                nextAction.action.Disable();
            }
        }

        private void OnThrowRequested(InputAction.CallbackContext context)
        {
            if (playerCamera == null || throwablePrefab == null)
            {
                Debug.LogWarning("ThrowableItem is missing camera or prefab reference.");
                return;
            }

            Vector3 spawnPosition = playerCamera.transform.position + playerCamera.transform.forward * spawnForwardOffset;
            Rigidbody spawned = Instantiate(throwablePrefab, spawnPosition, Quaternion.identity);
            spawned.linearVelocity = Vector3.zero;
            spawned.angularVelocity = Vector3.zero;
            spawned.AddForce(playerCamera.transform.forward * throwForce, ForceMode.VelocityChange);

            Debug.Log($"Threw item: {spawned.name}");
        }
    }
}
