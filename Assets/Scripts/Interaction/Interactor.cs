using UnityEngine;

public class Interactor : MonoBehaviour
{
    [SerializeField] private float interactRange = 2f;

    public bool TryInteract()
    {
        if (!Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, interactRange))
        {
            return false;
        }

        IInteractable interactable = hit.collider.GetComponentInParent<IInteractable>();
        if (interactable == null)
        {
            return false;
        }

        interactable.Interact(this);
        return true;
    }
}
