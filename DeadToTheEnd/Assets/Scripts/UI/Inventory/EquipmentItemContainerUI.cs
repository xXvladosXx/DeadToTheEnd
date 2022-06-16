using System.Collections.Generic;
using InventorySystem;
using UnityEngine;

namespace UI
{
    public class EquipmentItemContainerUI : ItemContainerUI
    {
        [SerializeField] private ItemSlotUI[] _slots;

        protected override void OnInitialize()
        {
            //Inventory = Game.GetInteractor<PlayerInteractor>().Player.GetComponentInChildren<ItemEquipper>().GetEquipment;
            
            CreateSlots();

            Inventory.ItemContainer.OnItemContainerUpdate += AppendSlots;
            Inventory.ItemContainer.OnItemUpdate += UpdateSlots;
            
            UpdateSlots();
        }

        protected override void CreateSlots()
        {
            SlotOnUI = new Dictionary<ItemSlotUI, ItemSlot>();
            Index = 0;

            foreach (var inventorySlot in Inventory.ItemContainer.GetItemsSlots)
            {
                var o = _slots[Index];
                o.SetItemData(Inventory.ItemContainer.GetDatabase.GetItemByID(inventorySlot.ID), SlotOnUI, Inventory.ItemContainer);

                SlotOnUI.Add(o, inventorySlot);
                Index++;
            }
        }
    }
}