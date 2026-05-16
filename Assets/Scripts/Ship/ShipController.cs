using UnityEngine;

public class ShipController : MonoBehaviour
{
    [SerializeField] private float forwardSpeed = 4f;
    [SerializeField] private float turnSpeed = 30f;

    public void Drive(float throttle, float steering)
    {
        transform.position += transform.forward * (throttle * forwardSpeed * Time.deltaTime);
        transform.Rotate(0f, steering * turnSpeed * Time.deltaTime, 0f);
    }
}
