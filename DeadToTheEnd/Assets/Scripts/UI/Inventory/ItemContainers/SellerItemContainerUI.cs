using System;
using InventorySystem;
using UI.Inventory;
using UI.Inventory.ItemContainers.Core;
using UnityEngine;

namespace UI
{
    public class SellerItemContainerUI : StaticItemContainerUI
    {
        public event Action<ItemSlot> OnSell;
        public override void Visit(ItemSlotUI itemSlotUI)
        {
            OnSell?.Invoke(itemSlotUI.ItemSlot);
        }

        public override void Visit(SellerItemSlotUI sellerItemSlotUI)
        {
            Debug.Log("Prohibited");
        }
    }
}