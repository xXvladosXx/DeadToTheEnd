using System;
using System.Collections.Generic;
using InventorySystem;
using UnityEngine;

namespace LootSystem
{
    [CreateAssetMenu (menuName = "LootSystem/Loot")]
    public class LootContainer : ScriptableObject
    {
        [field: SerializeField] public List<Loot> Items { get; private set; }
    }

    [Serializable]
    public class Loot
    {
        public Item Item;
        public int Quantity;
    }
}