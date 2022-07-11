using System.Collections.Generic;
using Entities;
using GameCore;
using GameCore.Player;
using InventorySystem;
using SkillsSystem;
using TMPro;
using UI.Inventory.ItemContainers.Core;
using UnityEngine;

namespace UI.Inventory.ItemContainers
{
    public class SkillContainerUI : InventoryItemContainerUI, IRefreshable
    {
        [SerializeField] private InventorySystem.Inventory _skillInventory;
        [SerializeField] private TextMeshProUGUI _points;
        
        private MainPlayer _mainPlayer;
        
        public override void CreateSlots()
        {
            base.CreateSlots();
            _mainPlayer = InteractorsBase.GetInteractor<PlayerInteractor>().MainPlayer;

            foreach (var itemDragHandler in SlotOnUI.Keys)
            {
                var upgradableSlot = itemDragHandler as SkillSlotUI;
                upgradableSlot.PossibleToLearn(_skillInventory.ItemContainer, _mainPlayer);
                
                upgradableSlot.OnItemUpgrade += AddItem;
            }

            _points.text = _mainPlayer.SkillManager.UnassignedPoints.ToString();
            _mainPlayer.SkillManager.OnPointsChange += ChangePoints;
        }

        private void ChangePoints()
        {
            _points.text = _mainPlayer.SkillManager.UnassignedPoints.ToString();
            foreach (var itemDragHandler in SlotOnUI.Keys)
            {
                var upgradableSlot = itemDragHandler as SkillSlotUI;
                upgradableSlot.PossibleToLearn(_skillInventory.ItemContainer, _mainPlayer);
            }

        }

        private void AddItem(Item obj)
        {
            _skillInventory.ItemContainer.AddItem(new ItemSlot(obj, 1, obj.ItemData.Id));
        }

        public void Refresh()
        {
            _points.text = _mainPlayer.SkillManager.UnassignedPoints.ToString();
        }
    }
}