using System.Collections.Generic;
using InventorySystem;
using UI.Inventory;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    public abstract class ItemContainerUI: MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IItemContainerVisitor
    {
        public InventorySystem.Inventory Inventory;
        
        protected Dictionary<ItemDragHandler, ItemSlot> SlotOnUI = new Dictionary<ItemDragHandler, ItemSlot>();
        protected int Index;

        public Dictionary<ItemDragHandler, ItemSlot> GetSlotOnUI => SlotOnUI;
        
        private void Awake()
        {
            Init();
        }

        private void OnEnable()
        {
            Inventory.ItemContainer.OnItemContainerUpdate += AppendSlots;
            Inventory.ItemContainer.OnItemUpdate += UpdateSlots;
        }

        private void OnDisable()
        {
            Inventory.ItemContainer.OnItemContainerUpdate -= AppendSlots;
            Inventory.ItemContainer.OnItemUpdate -= UpdateSlots;
        }
        protected virtual void AppendSlots()
        {
            UpdateSlots();
        }

        protected abstract void CreateSlots();
        protected abstract void Init();

        public void UpdateSlots()
        {
            foreach (var slot in SlotOnUI)
            {
                if (slot.Value.ID >= 0)
                {
                    slot.Key.UpdateData(slot.Value, new Color(1,1,1,1));
                }
                else
                {
                    slot.Key.UpdateData(null, new Color(1,1,1,0));
                }
            }
        }
        public void OnPointerEnter(PointerEventData eventData)
        {
            MouseData.UI = this;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            MouseData.UI = null;
        }

        public abstract void Visit(ItemSlotUI itemSlotUI);
        public abstract void Visit(SellerItemSlotUI sellerItemSlotUI);
    }
}