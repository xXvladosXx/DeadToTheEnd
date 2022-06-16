using System;
using UnityEngine;

namespace InventorySystem
{
    public interface IItem
    {
        string Id { get; }
        string Title { get; }
        string Description { get; }
        int MaxItemsInInventorySlot { get; }
        bool IsEquipped { get; set; }
        Sprite SpriteIcon { get; }
        Type Type { get; }
        ItemData ItemData { get; }
        ItemType ItemType { get; }
    }
}