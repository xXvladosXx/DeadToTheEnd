using System.Collections.Generic;
using GameCore;
using InventorySystem;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace UI.Inventory.ItemContainers.Core
{
    public abstract class ItemContainerUI: MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IItemContainerVisitor
    {
        [field: SerializeField] public InventorySystem.Inventory Inventory { get; set; }
        
        protected Dictionary<ItemDragHandler, ItemSlot> SlotOnUI = new Dictionary<ItemDragHandler, ItemSlot>();
        protected int Index;

        
        protected virtual void OnEnable()
        {
            Inventory.ItemContainer.OnItemContainerUpdate += AppendSlots;
            Inventory.ItemContainer.OnItemUpdate += UpdateSlots;
        }

        protected virtual void OnDisable()
        {
            Inventory.ItemContainer.OnItemContainerUpdate -= AppendSlots;
            Inventory.ItemContainer.OnItemUpdate -= UpdateSlots;
        }
        protected virtual void AppendSlots()
        {
            UpdateSlots();
        }

        public abstract void CreateSlots();
        public abstract void Init(InteractorsBase interactorsBase);

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
            MouseData.CurrentUI = this;
            if(MouseData.StartedDrag) return;
            MouseData.LastDraggedUI = this;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            MouseData.CurrentUI = null;
            if(MouseData.StartedDrag) return;
            MouseData.LastDraggedUI = null;
        }

        public abstract void Visit(ItemSlotUI itemSlotUI);
        public abstract void Visit(SellerItemSlotUI sellerItemSlotUI);
    }
}