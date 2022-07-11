using Entities.Core;
using InventorySystem;

namespace UI.Inventory.ItemContainers.Core
{
    public interface ILearnable
    {
        void PossibleToLearn(ItemContainer alreadyLearnedObjects, AliveEntity user);
    }
}