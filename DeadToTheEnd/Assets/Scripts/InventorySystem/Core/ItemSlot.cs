using System;
using System.Linq;
using UnityEngine;

namespace InventorySystem
{
     [Serializable]
    public class ItemSlot
    {
        public Item Item;
        public int ID;
        public int Index;
        [Min(0)] 
        public int Quantity;

        public ItemType[] ItemTypes;
        public bool Changeable = true;

        public ItemSlot()
        {
            Item = null;
            ID = -1;
            Quantity = 1;
            ItemTypes = new ItemType[]{};
            Changeable = true;
        }
        public ItemSlot(Item item, int quantity, int id)
        {
            Item = item;
            Quantity = quantity;
            ID = id;
        } 
        public ItemSlot(Item item, int quantity, int id, int index)
        {
            Item = item;
            Quantity = quantity;
            ID = id;
            Index = index;
        }
       
        public void RemoveItem()
        {
            Item = null;
            ID = -1;
            Quantity = 0;
        }

        public void UpdateSlot(ItemSlot item)
        {
            Item = item.Item;
            ID = item.ID;
            Quantity = item.Quantity;
        }

        public bool CanBeReplaced(Item item)
        {
            if (item == null)
                return true;
            
            if (ItemTypes.Length <= 0  || item.ItemData.Id < 0)
            {
                return true;
            }

            var anyItem = ItemTypes.Any(t => (t & item.ItemType) == t);

            return anyItem;  
        }

        public ItemSlot Clone()
        {
            return new ItemSlot(Item, Quantity, ID);
        }

        public bool IsEmpty => Item == null;
        public bool IsFull => Item.MaxItemsInInventorySlot == Quantity;
    }

    [Flags]
    [Serializable]
    public enum ItemType
    {
        Item = 1,
        Armor = Item | 1<< 2,
        Helmet = Item | 1 << 3,
        Chest = Item | 1 << 4,
        Pants = Item | 1 << 5,
        Consumable = Item | 1 << 6,
        Potion = Consumable | 1 << 7,
        Skill = 1<<8,
        Weapon = Item | 1 << 9,
        Bow = Weapon | 1<<10,
        Sword = Weapon | 1<<11,
        Stuff = Weapon | 1<<12,
        Gloves = Item | 1 << 13,
        Shoulders = Item | 1 << 14,
        Boots = Item | 1 << 15,
        Health = Potion | 1 << 16,
        Mana = Potion | 1 << 17,
    }
}