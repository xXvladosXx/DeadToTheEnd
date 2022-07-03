using System;
using System.Collections.Generic;
using Entities;
using InventorySystem;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace UI
{
    public class UpgradableSlotUI : ItemSlotUI
    {
        [SerializeField] private Button _upgradeButton;
        private MainPlayer _mainPlayer;

        public event Action<Item> OnItemUpgrade;

        public override void OnPointerUp(PointerEventData eventData)
        {
            if (_upgradeButton.gameObject.activeSelf) return;
            base.OnPointerUp(eventData);
        }

        public override void OnDrag(PointerEventData eventData)
        {
            if (_upgradeButton.gameObject.activeSelf) return;
            base.OnDrag(eventData);
        }

        public override void OnPointerDown(PointerEventData eventData)
        {
            if (_upgradeButton.gameObject.activeSelf) return;
            base.OnPointerDown(eventData);
        }

        public override void SetItemData(Item item, Dictionary<ItemDragHandler, ItemSlot> slots,
            ItemContainer itemContainer, ItemSlot itemSlot)
        {
            base.SetItemData(item, slots, itemContainer, itemSlot);

            if (itemContainer.HasItem(item))
            {
                _upgradeButton.gameObject.SetActive(false);
            }

            _upgradeButton.onClick.AddListener(UpgradeItem);

            if (item != null)
                _upgradeButton.image.sprite = item.SpriteIcon;
        }

        public void SetUser(MainPlayer mainPlayer)
        {
            _mainPlayer = mainPlayer;
            var item = Slots[this].Item;
            if (item is not UpgradableItem upgradableItem) return;

            if (!upgradableItem.CheckAllRequirements(_mainPlayer))
            {
                _upgradeButton.interactable = false;
            }
        }

        private void UpgradeItem()
        {
            OnItemUpgrade?.Invoke(Slots[this].Item);
            _upgradeButton.gameObject.SetActive(false);
            Debug.Log("WWW");
        }
    }
}