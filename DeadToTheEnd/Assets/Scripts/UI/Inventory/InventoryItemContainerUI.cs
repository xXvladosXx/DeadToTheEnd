using System;
using System.Collections.Generic;
using InventorySystem;
using UnityEngine;

namespace UI
{
    public class InventoryItemContainerUI: ItemContainerUI
    {
        [SerializeField] private ItemSlotUI _itemSlot;
        [SerializeField] private Transform _content;

        protected override void AppendSlots()
        {
            for (int i = Index; i < Inventory.ItemContainer.GetItemsSlots.Length; i++)
            {
                var o = Instantiate(_itemSlot, _content);

                o.SetItemData(null, SlotOnUI, Inventory.ItemContainer);

                SlotOnUI.Add(o, Inventory.ItemContainer.GetItemsSlots[i]);
                Index++;
            }
            
            base.AppendSlots();
        }

        protected override void CreateSlots()
        {
            SlotOnUI = new Dictionary<ItemSlotUI, ItemSlot>();
            Index = 0;
            
            foreach (var itemSlot in Inventory.ItemContainer.GetItemsSlots)
            {
                var o = Instantiate(_itemSlot, _content);
                o.SetItemData(Inventory.ItemContainer.GetDatabase.GetItemByID(itemSlot.ID), SlotOnUI, Inventory.ItemContainer);
                SlotOnUI.Add(o, itemSlot);
                Index++;
            }
        }
        
        public  void OnCreate()
        {
            
        }

        protected override void OnInitialize()
        {
            //Inventory = Game.GetInteractor<PlayerInteractor>().Player.GetComponentInChildren<ItemEquipper>().GetInventory;
            CreateSlots();  
            Inventory.ItemContainer.OnItemContainerUpdate += AppendSlots;
            Inventory.ItemContainer.OnItemUpdate += UpdateSlots;
                        
            UpdateSlots();
        }

        public  void OnStart()
        {
        }
    }
}