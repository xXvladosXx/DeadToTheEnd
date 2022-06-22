using System.Collections.Generic;
using InventorySystem;
using TMPro;
using UI.Inventory;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace UI
{
    public class ItemSlotUI : ItemDragHandler
    {
        public override void Accept(IItemContainerVisitor itemContainerVisitor)
        {
            itemContainerVisitor?.Visit(this);
        }
        
        public override void OnPointerUp(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                base.OnPointerUp(eventData);
                if (MouseData.TempItemDrag.GetComponent<ItemSlotUI>() == null)
                    return;

                if (MouseData.UI == null)
                {
                    Slots[this].RemoveItem();
                    return;
                }

                if (MouseData.TempItemHover)
                {
                    Accept(MouseData.UI);
                }
            }
        }

        
        public override void OnPointerDown(PointerEventData eventData)
        {
            base.OnPointerDown(eventData);

            var rt = MouseData.TempItemDrag.AddComponent<RectTransform>();
            rt.sizeDelta = new Vector2(50, 50);
            if (Slots[this].ID >= 0)
            {
                var img = MouseData.TempItemDrag.AddComponent<Image>();
                rt.gameObject.AddComponent<ItemSlotUI>();
                img.sprite = ItemContainer.GetDatabase.GetItemByID(Slots[this].ID).SpriteIcon;
                img.raycastTarget = false;
            }

            MouseData.TempItemDrag.GetComponent<RectTransform>().position = Mouse.current.position.ReadValue();
            MouseData.TempItemDrag.GetComponent<RectTransform>().SetAsLastSibling();
        }

        public override void OnDrag(PointerEventData eventData)
        {
            base.OnDrag(eventData);
        }
    }
}