using Entities;

namespace LootSystem
{
    public interface IInteractable
    {
        object ObjectOfInteraction();
        string TextOfInteraction();

        float GetDistanceOfRaycast { get; }
    }
}