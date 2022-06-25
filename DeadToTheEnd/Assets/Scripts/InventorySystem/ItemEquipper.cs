using System;
using System.Collections.Generic;
using Data.States.StateData.Player;
using Entities;
using LootSystem;
using SaveSystem;
using UnityEngine;
using Utilities.Raycast;

namespace InventorySystem
{
    public class ItemEquipper : MonoBehaviour, ISavable
    {   
        [field: SerializeField] public Inventory Inventory { get; private set; }
        [field: SerializeField] public Inventory Equipment { get; private set; }


        public object CaptureState()
        {
            var savedInventories = new SavableInventories
            {
                Inventory = new List<SavableItemSlot>(),
                Equipment = new List<SavableItemSlot>(),
            };
            
            foreach (var itemSlot in Inventory.ItemContainer.GetItemSlots)
            {
                var savableItemSlot = new SavableItemSlot(itemSlot.ID, itemSlot.Quantity, itemSlot.Index);
                savedInventories.Inventory.Add(savableItemSlot);
            }
            
            foreach (var itemSlot in Equipment.ItemContainer.GetItemSlots)
            {
                var savableItemSlot = new SavableItemSlot(itemSlot.ID, itemSlot.Quantity, itemSlot.Index);
                savedInventories.Equipment.Add(savableItemSlot);
            }

            return savedInventories;
        }

        public void RestoreState(object state)
        {
            Inventory.ItemContainer.Clear();
            Equipment.ItemContainer.Clear();
            
            var savedInventories = (SavableInventories) state;

            foreach (var itemSlot in savedInventories.Inventory)
            {
                var slot = new ItemSlot(Inventory.ItemContainer.GetDatabase.GetItemByID(itemSlot.ItemId),
                    itemSlot.Quantity, itemSlot.ItemId);
                Inventory.ItemContainer.AddItem(slot, itemSlot.Index);
            }

            foreach (var itemSlot in savedInventories.Equipment)
            {
                var slot = new ItemSlot(Inventory.ItemContainer.GetDatabase.GetItemByID(itemSlot.ItemId),
                    itemSlot.Quantity, itemSlot.ItemId);
                Equipment.ItemContainer.AddItem(slot, itemSlot.Index);
            }
        }

        [Serializable]
        public class SavableInventories
        {
            public List<SavableItemSlot> Inventory;
            public List<SavableItemSlot> Equipment;
        }

        [Serializable]
        public class SavableItemSlot
        {
            public int ItemId { get; private set; }
            public int Quantity { get; private set; }
            public int Index { get; private set; }

            public SavableItemSlot(int itemId, int quantity, int index)
            {
                ItemId = itemId;
                Quantity = quantity;
                Index = index;
            }
        }
    }
}