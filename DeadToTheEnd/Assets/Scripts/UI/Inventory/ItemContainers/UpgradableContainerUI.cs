using System.Collections.Generic;
using Entities;
using GameCore;
using GameCore.Player;
using InventorySystem;
using UI.Inventory.ItemContainers.Core;
using UnityEngine;

namespace UI.Inventory.ItemContainers
{
    public class UpgradableContainerUI : InventoryItemContainerUI
    {
        [SerializeField] private InventorySystem.Inventory _skillInventory;

        
        public override void CreateSlots()
        {
            base.CreateSlots();
            foreach (var itemDragHandler in SlotOnUI.Keys)
            {
                var upgradableSlot = itemDragHandler as UpgradableSlotUI;
                
                upgradableSlot.SetUser(InteractorsBase.GetInteractor<PlayerInteractor>().MainPlayer);
                upgradableSlot.OnItemUpgrade += AddItem;
            }
        }

        private void AddItem(Item obj)
        {
            _skillInventory.ItemContainer.AddItem(new ItemSlot(obj, 1, obj.ItemData.Id));
        }
    }
}