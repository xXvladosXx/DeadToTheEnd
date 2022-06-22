using System;
using GameCore;
using GameCore.ShopSystem;
using InventorySystem;
using UI.Inventory;
using UnityEngine;

namespace UI
{
    public class ShopUI: UIElement
    {
        [SerializeField] private SellerItemContainerUI _inventoryItemContainerUI;
        [SerializeField] private InventoryItemContainerUI _playerInventoryItemContainerUI;
        [SerializeField] private TransitionRequest _transitionRequest;

        private ShopInteractor _shopInteractor;
        public override void OnCreate(InteractorsBase interactorsBase)
        {
            _shopInteractor = interactorsBase.GetInteractor<ShopInteractor>();
            _inventoryItemContainerUI.Inventory = _shopInteractor.ShopItemContainer;
            _inventoryItemContainerUI.RefreshInventory();
        }

        private void TryToBuy(ItemSlot obj)
        {
            _shopInteractor.StartTransition(obj, true, obj.Quantity);
            //_transitionRequest.Init(_shopInteractor, obj);
        }

        private void TryToSell(ItemSlot obj)
        {
            _transitionRequest.Init(_shopInteractor, obj);
        }

        public override void OnInitialize()
        {
        }

        public override void OnStart()
        {
        }

        private void OnEnable()
        {
            _inventoryItemContainerUI.OnSell += TryToSell;
            _playerInventoryItemContainerUI.OnBuy += TryToBuy;
        }

        private void OnDisable()
        {
            _inventoryItemContainerUI.OnSell -= TryToSell;
            _playerInventoryItemContainerUI.OnBuy -= TryToBuy;
        }
    }
}