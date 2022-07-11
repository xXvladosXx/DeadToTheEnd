using System;
using UnityEngine;

namespace InventorySystem
{
    public interface IItem
    {
        string Name { get; }
        string Description { get; }
        int MaxItemsInInventorySlot { get; }
        Sprite SpriteIcon { get; }
        Type Type { get; }
        ItemData ItemData { get; }
        ItemType ItemType { get; }
    }
}