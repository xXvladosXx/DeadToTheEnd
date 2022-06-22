using GameCore.Player;
using InventorySystem;
using UI;
using UnityEngine;

namespace GameCore.ShopSystem
{
    public class ShopInteractor : Interactor
    {
        private ShopRepository _shopRepository;
        public Inventory ShopItemContainer { get; private set; }
        public PlayerInteractor PlayerInteractor { get; private set; }
        
        public override void Init()
        {
            _shopRepository = Game.GetRepository<ShopRepository>();
            PlayerInteractor = Game.GetInteractor<PlayerInteractor>();
            
            ShopItemContainer = _shopRepository.ShopInventory;
        }


        public void StartTransition(ItemSlot item, bool buying, int quantity)
        {
            var itemSlot = new ItemSlot
            {
                Item = item.Item,
                ID = item.ID,
                Quantity = quantity
            };
            
            if (buying)
            {
                bool pass = HasAllRequirements(item, quantity);
                if (pass)
                {
                    PlayerInteractor.MainPlayer.Gold.RemoveGold(item.Item.SellPrice);
                    
                    PlayerInteractor.MainPlayer.ItemEquipper.Inventory.ItemContainer.AddItem(itemSlot);
                    ShopItemContainer.ItemContainer.RemoveItem(item, quantity);
                }
            }
            else
            {
                PlayerInteractor.MainPlayer.Gold.AddGold(item.Item.SellPrice);
                ShopItemContainer.ItemContainer.AddItem(itemSlot);
                PlayerInteractor.MainPlayer.ItemEquipper.Inventory.ItemContainer.RemoveItem(item, quantity);
            }
        }

        public bool HasAllRequirements(ItemSlot item, int quantity) =>
            PlayerInteractor.MainPlayer.Gold.GoldAmount >= item.Item.SellPrice * quantity 
            && PlayerInteractor.MainPlayer.ItemEquipper.Inventory.ItemContainer.HasEnoughSpace();
    }
}