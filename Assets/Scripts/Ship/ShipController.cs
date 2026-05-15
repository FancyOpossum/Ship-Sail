using UnityEngine;
using UnityEngine.InputSystem;

namespace ShipSail.Ship
{
    /// <summary>
    /// Basic heavy-feeling ship controller using Rigidbody forces.
    /// </summary>
    [RequireComponent(typeof(Rigidbody))]
    public class ShipController : MonoBehaviour
    {
        [Header("Input")]
        [SerializeField] private InputActionReference moveAction;

        [Header("Movement")]
        [SerializeField] private float accelerationForce = 25f;
        [SerializeField] private float turnTorque = 8f;
        [SerializeField] private float maxForwardSpeed = 14f;
        [SerializeField] private float waterDrag = 1.2f;
        [SerializeField] private float angularWaterDrag = 2.5f;

        private Rigidbody rb;
        private bool isControlled;

        public bool IsControlled => isControlled;

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
            rb.linearDamping = waterDrag;
            rb.angularDamping = angularWaterDrag;
        }

        private void OnEnable()
        {
            moveAction?.action.Enable();
        }

        private void OnDisable()
        {
            moveAction?.action.Disable();
        }

        private void FixedUpdate()
        {
            if (!isControlled || moveAction == null)
            {
                return;
            }

            Vector2 input = moveAction.action.ReadValue<Vector2>();
            float throttle = input.y;
            float steer = input.x;

            Vector3 localVelocity = transform.InverseTransformDirection(rb.linearVelocity);
            if (Mathf.Abs(localVelocity.z) < maxForwardSpeed || Mathf.Sign(localVelocity.z) != Mathf.Sign(throttle))
            {
                rb.AddForce(transform.forward * throttle * accelerationForce, ForceMode.Acceleration);
            }

            rb.AddTorque(Vector3.up * steer * turnTorque, ForceMode.Acceleration);
        }

        public void SetControlled(bool controlled)
        {
            isControlled = controlled;
        }
    }
}
