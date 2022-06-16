using System.Collections.Generic;
using InventorySystem;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    public abstract class ItemContainerUI: MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] protected Inventory Inventory;
        
        protected Dictionary<ItemSlotUI, ItemSlot> SlotOnUI = new Dictionary<ItemSlotUI, ItemSlot>();
        protected int Index;

        public Inventory GetInventory => Inventory;
        public Dictionary<ItemSlotUI, ItemSlot> GetSlotOnUI => SlotOnUI;
        
        private void Awake()
        {
            OnInitialize();
        }
        
        protected virtual void AppendSlots()
        {
            UpdateSlots();
        }

        protected abstract void CreateSlots();
        protected abstract void OnInitialize();
        protected void UpdateSlots()
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
    }
}