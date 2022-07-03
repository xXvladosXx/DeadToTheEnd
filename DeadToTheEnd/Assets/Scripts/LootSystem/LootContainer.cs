using System;
using System.Collections.Generic;
using InventorySystem;
using UnityEngine;
using Random = UnityEngine.Random;

namespace LootSystem
{
    [CreateAssetMenu (menuName = "LootSystem/Loot")]
    public class LootContainer : ScriptableObject
    {
        [field: SerializeField] public List<Drop> Items { get; private set; }
        private ItemContainer _droppedLoot = new ItemContainer(6);

        public ItemContainer GetDrop()
        {
            _droppedLoot = new ItemContainer(6);
            _droppedLoot.Init();
            
            foreach (var drop in Items)
            {
                int roll = Random.Range(0, 100);
                roll -= drop.Probability;

                if (roll >= 0) continue;
                
                var randomQuantity = Random.Range(drop.MinAmount, drop.MaxAmount);
                var itemSlot = new ItemSlot(drop.Item, randomQuantity, drop.Item.ItemData.Id);
                    
                _droppedLoot.AddItem(itemSlot);
            }

            return _droppedLoot;
        }
    }

    [Serializable]
    public class Drop
    {
        public Item Item;
        public int MinAmount;
        public int MaxAmount;
        public int Probability;
    }
}