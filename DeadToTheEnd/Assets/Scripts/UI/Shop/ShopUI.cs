using System;
using GameCore;
using GameCore.ShopSystem;
using InventorySystem;
using UI.Inventory;
using UI.Inventory.ItemContainers;
using UnityEngine;

namespace UI
{
    public class ShopUI: ItemContainerManagerUI, IRefreshable
    {
        [SerializeField] private SellerItemContainerUI _sellerInventoryItemContainerUI;
        [SerializeField] private InventoryItemContainerUI _playerInventoryItemContainerUI;
        [SerializeField] private TransitionRequest _transitionRequest;

        private ShopInteractor _shopInteractor;
        public override void OnCreate(InteractorsBase interactorsBase)
        {
            _shopInteractor = interactorsBase.GetInteractor<ShopInteractor>();
            _sellerInventoryItemContainerUI.Inventory = _shopInteractor.ShopItemContainer;
            _sellerInventoryItemContainerUI.RefreshInventory();
        }

        private void TryToBuy(ItemSlot obj)
        {
            _transitionRequest.Init(_shopInteractor, obj, true);
        }

        private void TryToSell(ItemSlot obj)
        {
            _transitionRequest.Init(_shopInteractor, obj, false);
        }

  
        protected override void OnEnable()
        {
            base.OnEnable();
            _sellerInventoryItemContainerUI.OnSell += TryToSell;
            _playerInventoryItemContainerUI.OnBuy += TryToBuy;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            _sellerInventoryItemContainerUI.OnSell -= TryToSell;
            _playerInventoryItemContainerUI.OnBuy -= TryToBuy;
        }

        public void Refresh()
        {
            _playerInventoryItemContainerUI.RefreshInventory();
            _sellerInventoryItemContainerUI.RefreshInventory();
        }
    }
}