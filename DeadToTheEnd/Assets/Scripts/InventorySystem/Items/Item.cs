using System;
using InventorySystem.ItemInfo;
using UnityEngine;

namespace InventorySystem
{
    public abstract class Item : ScriptableObject, IItem
    {
        [field: SerializeField] public Rarity Rarity { get; private set; }
        
        [SerializeField] private Sprite _sprite;
        [SerializeField] private string _name;
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

        public string Name => _name;
        public string Description => _description;
        public int MaxItemsInInventorySlot => _maxItemsInInventory;
        public int SellPrice => _sellPrice;

        public string ColouredName()
        {
            string hexColour = ColorUtility.ToHtmlStringRGB(Rarity.Color);
            return $"<color=#{hexColour}>{Name}</color>";
        }
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