using System.Collections.Generic;
using GameCore;
using InventorySystem;
using UI.Inventory.ItemContainers.Core;
using UnityEngine;

namespace UI
{
    public class EquipmentItemContainerUI : ItemContainerUI
    {
        [SerializeField] private ItemSlotUI[] _slots;

        public override void Init(InteractorsBase interactorsBase)
        {
            CreateSlots();
            UpdateSlots();
        }
        

        public override void CreateSlots()
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
        private void Update()
        {
            UpdateSlots();
        }
        public override void Visit(ItemSlotUI itemSlotUI)
        {
            var mouseHoverSlot = SlotOnUI[MouseData.TempItemHover.GetComponent<ItemSlotUI>()];

            Inventory.ItemContainer.SwapItem(MouseData.UI.Inventory.ItemContainer,  itemSlotUI.ItemSlot, mouseHoverSlot);
            Debug.Log("VE");
            UpdateSlots();
        }

        public override void Visit(SellerItemSlotUI sellerItemSlotUI)
        {
            
        }
    }
}