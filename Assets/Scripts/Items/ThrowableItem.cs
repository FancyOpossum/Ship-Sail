using UnityEngine;

public class ThrowableItem : MonoBehaviour
{
    [SerializeField] private Rigidbody itemRigidbody;
    [SerializeField] private float throwForce = 8f;

    public void Throw(Vector3 direction)
    {
        if (itemRigidbody == null)
        {
            return;
        }

        itemRigidbody.isKinematic = false;
        itemRigidbody.AddForce(direction.normalized * throwForce, ForceMode.VelocityChange);
    }
}
