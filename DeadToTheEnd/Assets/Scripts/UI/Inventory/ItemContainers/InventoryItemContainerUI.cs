using System;
using System.Collections.Generic;
using InventorySystem;
using UI.Inventory.ItemContainers.Core;
using UnityEngine;

namespace UI
{
    public class InventoryItemContainerUI : StaticItemContainerUI
    {
        public event Action<ItemSlot> OnBuy;
        
        protected override void AppendSlots()
        {
            for (int i = Index; i < Inventory.ItemContainer.GetItemSlots.Length; i++)
            {
                var o = Instantiate(ItemSlot, Content);

                o.SetItemData(null, SlotOnUI, Inventory.ItemContainer, Inventory.ItemContainer.GetItemSlots[i]);

                SlotOnUI.Add(o, Inventory.ItemContainer.GetItemSlots[i]);
                Index++;
            }
            
            base.AppendSlots();
        }

        

        public override void Visit(ItemSlotUI itemSlotUI)
        {
            var mouseHoverSlot = SlotOnUI[MouseData.TempItemHover.GetComponent<ItemSlotUI>()];

            Inventory.ItemContainer.SwapItem(MouseData.LastDraggedUI.Inventory.ItemContainer,  itemSlotUI.ItemSlot, mouseHoverSlot);
            UpdateSlots();
        }

        public override void Visit(SellerItemSlotUI sellerItemSlotUI)
        {
            OnBuy?.Invoke(sellerItemSlotUI.ItemSlot);
        }
    }
}