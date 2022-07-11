using System;
using System.Collections.Generic;
using Entities;
using GameCore;
using GameCore.Player;
using InventorySystem;
using LootSystem;
using UI.Inventory.ItemContainers;
using UI.Inventory.ItemContainers.Core;
using UnityEngine;

namespace UI.Loot
{
    public class LootUI : ItemContainerManagerUI
    {
        [SerializeField] private InventoryItemContainerUI _lootItemContainer;
        
        private MainPlayer _mainPlayer;
        private ItemContainer _lastDrop;
        private InteractableChecker _interactableChecker;

        public override void OnCreate(InteractorsBase interactorsBase)
        {
            _mainPlayer = interactorsBase.GetInteractor<PlayerInteractor>().MainPlayer;
            _interactableChecker = _mainPlayer.GetComponent<InteractableChecker>();
            _interactableChecker.OnLootOpen += ShowLoot;
        }

        private void ShowLoot(ItemContainer drop)
        {
            Show();
            _lootItemContainer.RefreshInventory();
            _lastDrop = drop;

            foreach (var item in drop.GetItemSlots)
            {
                _lootItemContainer.Inventory.ItemContainer.AddItem(item);
            }

            gameObject.SetActive(true);
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            if(_interactableChecker == null) return;
            _interactableChecker.CurrentInteractableObject = null;
            if(_lastDrop == null) return;
            
            for (int i = 0; i < _lootItemContainer.Inventory.ItemContainer.GetItemSlots.Length; i++)
            {
                _lastDrop.GetItemSlots[i] = _lootItemContainer.Inventory.ItemContainer.GetItemSlots[i];
            }
            _lootItemContainer.Inventory.ItemContainer.Clear(ItemContainer.ClearingMode.ClearAndRemove);

            _lastDrop = null;
        }
    }
}