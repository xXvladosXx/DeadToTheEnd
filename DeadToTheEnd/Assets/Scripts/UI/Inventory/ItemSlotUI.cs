using System.Collections.Generic;
using InventorySystem;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace UI
{
    public class ItemSlotUI : ItemDragHandler
    {
        [SerializeField] private Image _image;
        [SerializeField] private TextMeshProUGUI _text;

        private Dictionary<ItemSlotUI, ItemSlot> _slots;
        private ItemContainer _itemContainer;
        private Item _item;


        public void SetItemData(Item item, Dictionary<ItemSlotUI, ItemSlot> slots, ItemContainer itemContainer)
        {
            _item = item;
            _slots = slots;
            _itemContainer = itemContainer;

            if (_item != null)
            {
                _image.sprite = item.SpriteIcon;
            }
        }

        public void UpdateData(ItemSlot itemSlot, Color color)
        {
            _image.color = color;

            if (itemSlot == null)
            {
                _image.sprite = null;
                _text.text = "";
            }
            else
            {
                var greaterThanOne = itemSlot.Quantity > 1;
                _image.sprite = itemSlot.Item.SpriteIcon;
                _text.text = greaterThanOne ? itemSlot.Quantity.ToString() : "";
            }
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
                    _slots[this].RemoveItem();
                    return;
                }

                if (MouseData.TempItemHover)
                {
                    var mouseHoverSlot = MouseData.UI.GetSlotOnUI[MouseData.TempItemHover.GetComponent<ItemSlotUI>()];

                    _itemContainer.SwapItem(MouseData.UI.GetInventory.ItemContainer, _slots[this], mouseHoverSlot);

                    return;
                }
            }
        }

        public override void OnPointerDown(PointerEventData eventData)
        {
            base.OnPointerDown(eventData);

            var rt = MouseData.TempItemDrag.AddComponent<RectTransform>();
            rt.sizeDelta = new Vector2(50, 50);
            if (_slots[this].ID >= 0)
            {
                var img = MouseData.TempItemDrag.AddComponent<Image>();
                rt.gameObject.AddComponent<ItemSlotUI>();
                img.sprite = _itemContainer.GetDatabase.GetItemByID(_slots[this].ID).SpriteIcon;
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