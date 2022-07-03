using System;
using System.Collections.Generic;
using InventorySystem;
using InventorySystem.Core;
using SaveSystem;
using UnityEngine;

namespace GameCore.ShopSystem
{
    public class Seller : MonoBehaviour, ISavable
    {
        [field: SerializeField] public ItemContainer ItemContainer { get; private set; }

        public void Init(ItemContainer itemContainer)
        {
            ItemContainer = itemContainer;
        }

        public object CaptureState()
        {
            var savedInventories = new SavableInventory
            {
                Inventory = new List<SavableItemSlot>(),
            };
            
            foreach (var itemSlot in ItemContainer.GetItemSlots)
            {
                var savableItemSlot = new SavableItemSlot(itemSlot.ID, itemSlot.Quantity, itemSlot.Index);
                savedInventories.Inventory.Add(savableItemSlot);
            }

            return savedInventories;
        }

        public void RestoreState(object state)
        {
            ItemContainer.Clear();
            
            var savedInventories = (SavableInventory) state;

            foreach (var itemSlot in savedInventories.Inventory)
            {
                var slot = new ItemSlot(ItemContainer.GetDatabase.GetItemByID(itemSlot.ItemId),
                    itemSlot.Quantity, itemSlot.ItemId);
                ItemContainer.AddItem(slot, itemSlot.Index);
            }
        }
        
        [Serializable]
        public class SavableInventory
        {
            public List<SavableItemSlot> Inventory;
        }
    }
}