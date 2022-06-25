using System;
using Entities;
using GameCore;
using GameCore.Player;
using InventorySystem;
using LootSystem;
using UnityEngine;

namespace UI.Loot
{
    public class LootUI : ItemContainerManagerUI
    {
        [SerializeField] private ItemContainerUI _lootItemContainer;
        [SerializeField] private InventoryItemContainerUI _inventoryItemContainer;
        
        private MainPlayer _mainPlayer;

        public override void OnCreate(InteractorsBase interactorsBase)
        {
            _mainPlayer = interactorsBase.GetInteractor<PlayerInteractor>().MainPlayer;
            _mainPlayer.GetComponent<InteractableChecker>().OnLootOpen += ShowLoot;
        }

        private void ShowLoot(LootContainer obj)
        {
            Show();
            _lootItemContainer.Inventory.ItemContainer.Clear();
            foreach (var item in obj.Items)
            {
                var itemSlot = new ItemSlot(item.Item, item.Quantity, item.Item.ItemData.Id);
                _lootItemContainer.Inventory.ItemContainer.AddItem(itemSlot);
            }
            
            gameObject.SetActive(true);
        }

        public override void OnInitialize()
        {
            
        }

        public override void OnStart()
        {
            
        }
        
        
    }
}