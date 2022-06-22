using System;
using InventorySystem;
using UI.Inventory;
using UnityEngine;

namespace UI
{
    public class SellerItemContainerUI : StaticItemContainerUI
    {
        public event Action<ItemSlot> OnSell;
        public override void Visit(ItemSlotUI itemSlotUI)
        {
            Debug.Log("Sold");
            OnSell?.Invoke(itemSlotUI.ItemSlot);
        }

        public override void Visit(SellerItemSlotUI sellerItemSlotUI)
        {
            Debug.Log("Loh");
        }
    }
}