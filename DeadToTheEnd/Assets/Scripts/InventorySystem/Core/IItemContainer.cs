namespace InventorySystem
{
    public interface IItemContainer
    {
        void AddItem(ItemSlot itemSlot);
        void RemoveItem(ItemSlot itemSlot, int quantity);
        void RemoveAt(int slotIndex);
        bool HasItem(Item item);
        int GetTotalQuantity(Item item);

        ItemSlot GetItemSlot<T>() where T : ItemSlot;
    }
}