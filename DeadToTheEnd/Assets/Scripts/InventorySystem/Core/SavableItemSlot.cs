using System;

namespace InventorySystem.Core
{
    [Serializable]
    public class SavableItemSlot
    {
        public int ItemId { get; private set; }
        public int Quantity { get; private set; }
        public int Index { get; private set; }

        public SavableItemSlot(int itemId, int quantity, int index)
        {
            ItemId = itemId;
            Quantity = quantity;
            Index = index;
        }
    }
}