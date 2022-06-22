using UnityEngine;

namespace InventorySystem
{
    [CreateAssetMenu (menuName = "InventorySystem/Inventory")]
    public class Inventory : ScriptableObject
    {
        public ItemContainer ItemContainer = new ItemContainer(40);

        private void OnEnable()
        {
            ItemContainer.Init();
            
            foreach (var itemSlot in ItemContainer.GetItemSlots)
            {
                if (itemSlot.Item != null)
                {
                    itemSlot.ID = itemSlot.Item.ItemData.Id;
                }
            }
        }
    }
}