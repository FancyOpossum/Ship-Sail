using UnityEngine;

namespace ShipSail.Interaction
{
    /// <summary>
    /// Interface for anything the player can interact with.
    /// </summary>
    public interface IInteractable
    {
        void Interact(GameObject interactor);
        string GetInteractionPrompt();
    }
}
