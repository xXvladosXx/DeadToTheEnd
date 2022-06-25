using System.Collections.Generic;
using InventorySystem;
using UnityEngine;

namespace UI
{
    public class EquipmentItemContainerUI : ItemContainerUI
    {
        [SerializeField] private ItemSlotUI[] _slots;

        protected override void Init()
        {
            CreateSlots();
            UpdateSlots();
        }
        

        protected override void CreateSlots()
        {
            SlotOnUI = new Dictionary<ItemDragHandler, ItemSlot>();
            Index = 0;

            foreach (var inventorySlot in Inventory.ItemContainer.GetItemSlots)
            {
                var o = _slots[Index];
                o.SetItemData(Inventory.ItemContainer.GetDatabase.GetItemByID(inventorySlot.ID), 
                    SlotOnUI, Inventory.ItemContainer, Inventory.ItemContainer.GetItemSlots[Index]);

                SlotOnUI.Add(o, inventorySlot);
                Index++;
            }
        }
        
        public override void Visit(ItemSlotUI itemSlotUI)
        {
            var mouseHoverSlot = GetSlotOnUI[MouseData.TempItemHover.GetComponent<ItemSlotUI>()];

            Inventory.ItemContainer.SwapItem(MouseData.UI.Inventory.ItemContainer,  itemSlotUI.ItemSlot, mouseHoverSlot);
        }

        public override void Visit(SellerItemSlotUI sellerItemSlotUI)
        {
            
        }
    }
}