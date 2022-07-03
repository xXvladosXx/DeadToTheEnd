using System.Collections.Generic;
using InventorySystem;
using TMPro;
using UI.Inventory;
using UI.Tooltip;
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
            Debug.Log(this.Item);
            itemContainerVisitor?.Visit(this);
        }

        public override void OnPointerEnter(PointerEventData eventData)
        {
            base.OnPointerEnter(eventData);
            TooltipPopup.Instance.DisplayInfo(ItemSlot.Item);
        }

        public override void OnPointerExit(PointerEventData eventData)
        {
            base.OnPointerExit(eventData);
            TooltipPopup.Instance.HideInfo();
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
                    //Slots[this].RemoveItem();
                    ItemContainer.RemoveItem(Slots[this], Slots[this].Quantity);
                    return;
                }

                if (MouseData.TempItemHover)
                {
                    TooltipPopup.Instance.DisplayInfo(ItemSlot.Item);

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
                img.sprite = Slots[this].Item.SpriteIcon;
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