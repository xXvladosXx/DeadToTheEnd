using System;
using UnityEngine;

namespace InventorySystem
{
    public abstract class Item : ScriptableObject, IItem
    {
        [SerializeField] private Sprite _sprite;
        [SerializeField] private string _id;
        [SerializeField] private string _title;
        [SerializeField] private string _description;
        [SerializeField] private int _maxItemsInInventory;
        [SerializeField] private int _sellPrice;
        [SerializeField] private ItemType _itemType;

        [SerializeField] private ItemData _itemData = new ItemData();

        public bool IsEquipped { get; set; }
        public Sprite SpriteIcon => _sprite;
        public Type Type => GetType();
        public ItemData ItemData => _itemData;
        public ItemType ItemType => _itemType;

        public string Id => _id;
        public string Title => _title;
        public string Description => _description;
        public int MaxItemsInInventorySlot => _maxItemsInInventory;
        public int SellPrice => _sellPrice;
        
        public abstract string GetInfoDisplayText();
    }
    
    [Serializable]
    public class ItemData
    {
        public int Id = -1;

        public ItemData()
        {
            Id = -1;
        }
        public ItemData(int id)
        {
            Id = id;
        }
    }
}