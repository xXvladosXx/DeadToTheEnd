using GameCore.Player;
using InventorySystem;
using SaveSystem;
using UI;
using UnityEngine;

namespace GameCore.ShopSystem
{
    public class ShopInteractor : Interactor, ISavableInteractor
    {
        public Inventory ShopItemContainer { get; private set; }
        public PlayerInteractor PlayerInteractor { get; private set; }

        private GameObject _seller;
        private ShopRepository _shopRepository;

        public override void Init()
        {
            _shopRepository = Game.GetRepository<ShopRepository>();
            PlayerInteractor = Game.GetInteractor<PlayerInteractor>();

            _seller = Object.Instantiate(Resources.Load ("Entities/Seller") as GameObject);

            ShopItemContainer = _shopRepository.ShopInventory;
            _seller.GetComponent<Seller>().Init(ShopItemContainer.ItemContainer);
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
                    PlayerInteractor.MainPlayer.Gold.RemoveGold(item.Item.SellPrice * quantity);
                    
                    PlayerInteractor.MainPlayer.ItemEquipper.Inventory.ItemContainer.AddItem(itemSlot);
                    ShopItemContainer.ItemContainer.RemoveItem(item, quantity);
                }
            }
            else
            {
                PlayerInteractor.MainPlayer.Gold.AddGold(item.Item.SellPrice* quantity);
                ShopItemContainer.ItemContainer.AddItem(itemSlot);
                PlayerInteractor.MainPlayer.ItemEquipper.Inventory.ItemContainer.RemoveItem(item, quantity);
            }
        }

        public bool HasAllRequirements(ItemSlot item, int quantity) =>
            PlayerInteractor.MainPlayer.Gold.GoldAmount >= item.Item.SellPrice * quantity 
            && PlayerInteractor.MainPlayer.ItemEquipper.Inventory.ItemContainer.HasEnoughSpace();

        public SavableEntity GetSavableEntity()
        {
            return _seller.GetComponent<SavableEntity>();
        }
    }
}