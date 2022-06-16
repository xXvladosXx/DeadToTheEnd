namespace InventorySystem
{
    public interface IItemContainer
    {
        ItemSlot AddItem(ItemSlot itemSlot);
        void RemoveItem(ItemSlot itemSlot);
        void RemoveAt(int slotIndex);
        bool HasItem(Item item);
        int GetTotalQuantity(Item item);

        ItemSlot GetItemSlot<T>() where T : ItemSlot;
    }
}