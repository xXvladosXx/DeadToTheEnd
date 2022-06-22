using InventorySystem;
using UnityEngine;

namespace GameCore.ShopSystem
{
    public class ShopRepository : Repository 
    {
        public Inventory ShopInventory { get; private set; }
        public override void Init()
        {
            ShopInventory = Resources.Load("Data/Shop/ShopInventory") as Inventory;
        }

        public override void OnStart()
        {
            
        }

        public override void OnCreate()
        {
            
        }
    }
}