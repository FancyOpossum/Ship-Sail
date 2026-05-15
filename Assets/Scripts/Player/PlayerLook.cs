using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerLook : MonoBehaviour
{
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private InputActionReference lookAction;
    [SerializeField] private float lookSensitivity = 1f;
    [SerializeField] private float maxVerticalAngle = 80f;

    private float pitch;

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
        Vector2 lookInput = lookAction != null ? lookAction.action.ReadValue<Vector2>() : Vector2.zero;

        float yaw = lookInput.x * lookSensitivity;
        float pitchDelta = lookInput.y * lookSensitivity;

        transform.Rotate(0f, yaw, 0f);

        pitch = Mathf.Clamp(pitch - pitchDelta, -maxVerticalAngle, maxVerticalAngle);
        if (cameraTransform != null)
        {
            cameraTransform.localRotation = Quaternion.Euler(pitch, 0f, 0f);
        }
    }
}
