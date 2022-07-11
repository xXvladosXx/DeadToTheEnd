using System.Collections.Generic;
using InventorySystem;
using TMPro;
using UI.Inventory;
using UI.Inventory.ItemContainers.Core;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace UI
{
    public abstract class ItemDragHandler: MonoBehaviour,  IPointerDownHandler, IPointerUpHandler, IDragHandler, IPointerEnterHandler, IPointerExitHandler
    {
        public Item Item { get; private set; }
        public ItemSlot ItemSlot { get; private set; }

        [SerializeField] protected Image Image;
        [SerializeField] protected TextMeshProUGUI Text;

        protected Dictionary<ItemDragHandler, ItemSlot> Slots;
        protected ItemContainer ItemContainer;
        
        
        private bool _isHovering;

        public abstract void Accept(IItemContainerVisitor itemContainerVisitor);
        
        public virtual void OnPointerDown(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                MouseData.StartedDrag = true;
                MouseData.LastItemClicked = gameObject.GetComponent<ItemDragHandler>();
                var mouseObj = new GameObject
                {
                    transform =
                    {
                        parent = transform.GetComponentInParent<Canvas>().transform
                    }
                };
               
                MouseData.TempItemDrag = mouseObj;
            }
        }

        public virtual void OnPointerUp(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                MouseData.StartedDrag = false;

                Destroy(MouseData.TempItemDrag);
            }
        }

        public virtual void OnDrag(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                if (MouseData.TempItemDrag == null) return;

                MouseData.TempItemDrag.GetComponent<RectTransform>().position = Mouse.current.position.ReadValue();
                MouseData.TempItemDrag.GetComponent<RectTransform>().SetAsLastSibling();
            }
        }

        public virtual void OnPointerEnter(PointerEventData eventData)
        {
            MouseData.TempItemHover = gameObject;
            _isHovering = true;
        }

        public virtual void OnPointerExit(PointerEventData eventData)
        {
            MouseData.TempItemHover = null;
            _isHovering = false;
        }
        
        public virtual void SetItemData(Item item, Dictionary<ItemDragHandler, ItemSlot> slots, ItemContainer itemContainer,
            ItemSlot itemSlot)
        {
            Item = item;
            Slots = slots;
            ItemSlot = itemSlot;
            ItemContainer = itemContainer;

            if (Item != null)
            {
                Image.sprite = item.SpriteIcon;
            }
        }

        public void UpdateData(ItemSlot itemSlot, Color color)
        {
            Image.color = color;

            if (itemSlot == null || itemSlot.ID < 0)
            {
                Image.sprite = null;
                Image.color = color;
                Text.text = "";
            }
            else
            {
                var greaterThanOne = itemSlot.Quantity > 1;
                Image.sprite = itemSlot.Item.SpriteIcon;
                Image.color = color;

                Text.text = greaterThanOne ? itemSlot.Quantity.ToString() : "";
            }
        }
        protected virtual void OnDisable()
        {
            if (_isHovering)
            {
                _isHovering = false;
            }
        }
    }

    public static class MouseData
    {
        public static ItemContainerUI LastDraggedUI { get; set; }
        public static ItemContainerUI CurrentUI { get; set; }
        public static bool StartedDrag { get; set; }
        public static GameObject TempItemDrag { get; set; }
        public static GameObject TempItemHover { get; set; }
        public static ItemDragHandler LastItemClicked { get; set; }
    }
}