using System.Collections.Generic;
using GameCore;
using InventorySystem;
using UnityEngine;

namespace UI.Inventory.ItemContainers.Core
{
    public abstract class StaticItemContainerUI : ItemContainerUI
    {
        [SerializeField] protected ItemDragHandler ItemSlot;
        [SerializeField] protected Transform Content;

        protected InteractorsBase InteractorsBase;
        
        public override void Init(InteractorsBase interactorsBase)
        {
            InteractorsBase = interactorsBase;
            
            CreateSlots();
            UpdateSlots();
        }


        public override void CreateSlots()
        {
            SlotOnUI = new Dictionary<ItemDragHandler, ItemSlot>();
            Index = 0;

            foreach (var itemSlot in Inventory.ItemContainer.GetItemSlots)
            {
                if(ItemSlot == null) continue;
                var o = Instantiate(ItemSlot, Content);
                o.SetItemData(Inventory.ItemContainer.GetDatabase.GetItemByID(itemSlot.ID),
                    SlotOnUI, Inventory.ItemContainer, Inventory.ItemContainer.GetItemSlots[Index]);
                SlotOnUI.Add(o, itemSlot);
                Index++;
            }
        }

        public void RefreshInventory()
        {
            foreach (Transform child in Content)
            {
                Destroy(child.gameObject);
            }
            SlotOnUI.Clear();
            
            Init(InteractorsBase);
        }
    }
}