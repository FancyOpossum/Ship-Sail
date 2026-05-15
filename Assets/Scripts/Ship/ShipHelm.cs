using UnityEngine;

[RequireComponent(typeof(ShipController))]
public class ShipHelm : MonoBehaviour, IInteractable
{
    [SerializeField] private float throttle;
    [SerializeField] private float steering;

    private ShipController shipController;

    private void Awake()
    {
        shipController = GetComponent<ShipController>();
    }

    private void Update()
    {
        shipController.Drive(throttle, steering);
    }

    public void Interact(Interactor interactor)
    {
        enabled = !enabled;
    }
}
