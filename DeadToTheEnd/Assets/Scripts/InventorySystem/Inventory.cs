using UnityEngine;

namespace InventorySystem
{
    [CreateAssetMenu (menuName = "InventorySystem/Inventory")]
    public class Inventory : ScriptableObject
    {
        public ItemContainer ItemContainer = new ItemContainer(20);

        private void OnEnable()
        {
            foreach (var itemSlot in ItemContainer.GetItemsSlots)
            {
                if (itemSlot.Item != null)
                {
                    itemSlot.ID = itemSlot.Item.ItemData.Id;
                }
            }
        }
    }
}