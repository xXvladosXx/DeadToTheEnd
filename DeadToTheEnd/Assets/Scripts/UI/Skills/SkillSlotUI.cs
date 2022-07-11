using System;
using System.Collections.Generic;
using Entities;
using Entities.Core;
using InventorySystem;
using SkillsSystem;
using UI.Inventory.ItemContainers.Core;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace UI
{
    public class SkillSlotUI : ItemSlotUI, ILearnable
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

        private void UpgradeItem(UpgradableItem upgradableItem, IUser user)
        {
            OnItemUpgrade?.Invoke(Slots[this].Item);
            
            _upgradeButton.onClick.RemoveAllListeners();
            _upgradeButton.gameObject.SetActive(false);
            
            foreach (var requirement in upgradableItem.RequirementsToUpgrade)
            {
                requirement.ApplyRequirement(user);
            }
        }

        public void PossibleToLearn(ItemContainer alreadyLearnedObjects, AliveEntity user)
        {
            _upgradeButton.gameObject.SetActive(!alreadyLearnedObjects.HasItem(Item));
            
            if (Item is UpgradableItem upgradable)
            {
                if (!upgradable.CheckAllRequirements(user))
                {
                    _upgradeButton.interactable = false;
                }
                else
                {
                    _upgradeButton.interactable = true;
                    _upgradeButton.onClick.AddListener(() => UpgradeItem(upgradable, user));
                }
            }

            if (Item != null)
                _upgradeButton.image.sprite = Item.SpriteIcon;
        }
    }
}