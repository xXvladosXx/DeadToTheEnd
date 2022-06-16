﻿using System;
using UnityEngine;

namespace InventorySystem
{
    [Serializable]
    public class ItemContainer : IItemContainer
    {
        [SerializeField] private ItemSlot[] _itemSlots;
        [SerializeField] private ItemDatabase _database;

        public ItemSlot[] GetItemsSlots => _itemSlots;
        public ItemDatabase GetDatabase => _database;

        public event Action OnItemUpdate;
        public event Action OnItemContainerUpdate;

        public ItemContainer(int size)
        {
            _itemSlots = new ItemSlot[size];
        }

        public ItemSlot AddItem(ItemSlot itemSlot)
        {
            foreach (var slot in _itemSlots)
            {
                if (slot.Item == itemSlot.Item)
                {
                    int slotRemainingSpace = slot.Item.MaxItemsInInventorySlot - slot.Quantity;
                    if (itemSlot.Quantity <= slotRemainingSpace)
                    {
                        slot.Quantity += itemSlot.Quantity;
                        itemSlot.RemoveItem();
                        OnItemUpdate?.Invoke();
                        return itemSlot;
                    }

                    if (slotRemainingSpace > 0)
                    {
                        slot.Quantity += slotRemainingSpace;
                        itemSlot.Quantity -= slotRemainingSpace;
                    }
                    
                }
            }

            for (int i = 0; i < _itemSlots.Length; i++)
            {
                if (_itemSlots[i].Item == null)
                {
                    if (itemSlot.Quantity <= itemSlot.Item.MaxItemsInInventorySlot)
                    {
                        SetSlot(itemSlot);
                        OnItemUpdate?.Invoke();
                        return itemSlot;
                    }

                    _itemSlots[i] = new ItemSlot(itemSlot.Item, itemSlot.Item.MaxItemsInInventorySlot, itemSlot.ID);
                    itemSlot.Quantity -= itemSlot.Item.MaxItemsInInventorySlot;
                }
            }

            OnItemUpdate?.Invoke();
            return itemSlot;
        }

        private void SetSlot(ItemSlot itemSlot)
        {
            foreach (var slot in _itemSlots)
            {
                if (slot.ID > -1 || !slot.CanBeReplaced(_database.GetItemByID(itemSlot.ID))) continue;

                slot.UpdateSlot(itemSlot);
                break;
            }
        }
        
        public void RemoveItem(ItemSlot itemSlot)
        {
            for (int i = 0; i < _itemSlots.Length; i++)
            {
                if (_itemSlots[i].Item == null) continue;
                if (_itemSlots[i] == itemSlot)
                {
                    _itemSlots[i].RemoveItem();
                    OnItemUpdate?.Invoke();
                    OnItemContainerUpdate?.Invoke();
                    return;
                }
            }
        }

        public void RemoveAt(int slotIndex)
        {
            if (slotIndex < 0 || slotIndex > _itemSlots.Length - 1)
                return;

            _itemSlots[slotIndex] = new ItemSlot();

            OnItemUpdate?.Invoke();
        }

        public bool SwapItem(ItemContainer uiItemContainer, ItemSlot draggedItem, ItemSlot replacedItem)
        {
            if (draggedItem == replacedItem) return true;
            if (!draggedItem.CanBeReplaced(replacedItem.Item)) return false;
            if (!replacedItem.CanBeReplaced(draggedItem.Item)) return false;

            if (draggedItem.ID == replacedItem.ID)
            {
                if (replacedItem.Quantity != replacedItem.Item.MaxItemsInInventorySlot)
                {            
                    return AddToSlot(draggedItem, replacedItem);
                }
            }

            var temp = new ItemSlot(replacedItem.Item, replacedItem.Quantity, replacedItem.ID);
            replacedItem.UpdateSlot(draggedItem);
            draggedItem.UpdateSlot(temp);

            OnItemUpdate?.Invoke();
            uiItemContainer.OnItemUpdate?.Invoke();

            return true;
        }

        private bool AddToSlot(ItemSlot draggedItem, ItemSlot replacedItem)
        {
            var fits = draggedItem.Quantity + replacedItem.Quantity <= replacedItem.Item.MaxItemsInInventorySlot;
            var amountToAdd =
                fits ? draggedItem.Quantity : replacedItem.Item.MaxItemsInInventorySlot - replacedItem.Quantity;

            var amountLeft = draggedItem.Quantity - amountToAdd;
            var clonedItem = draggedItem.Clone();
            clonedItem.Quantity = amountToAdd;
            replacedItem.Quantity += amountToAdd;

            Debug.Log($"Item added {clonedItem} {clonedItem.Item.ItemData} {clonedItem.Quantity}");
            OnItemUpdate?.Invoke();

            if (amountLeft <= 0)
            {
                RemoveItem(draggedItem);
                OnItemUpdate?.Invoke();

                return true;
            }

            draggedItem.Quantity = amountLeft;
            OnItemUpdate?.Invoke();

            return true;
        }

        public bool HasItem(Item item)
        {
            foreach (var slot in _itemSlots)
            {
                if (slot.Item == null)
                {
                    continue;
                }

                if (slot.Item != item)
                {
                    continue;
                }

                return true;
            }

            return false;
        }

        public int GetTotalQuantity(Item item)
        {
            int totalCount = 0;
            foreach (var slot in _itemSlots)
            {
                if (slot.Item == null) continue;
                if (slot.Item != item) continue;

                totalCount += slot.Quantity;
            }

            return totalCount;
        }

        public ItemSlot GetItemSlot<T>() where T : ItemSlot
        {
            var def = typeof(T);

            foreach (var itemSlot in _itemSlots)
            {
                if (itemSlot.GetType() == def)
                    return itemSlot;
            }

            return null;
        }

        public ItemSlot GetSlotByIndex(int index) => _itemSlots[index];

        public void AddSlots(int newSlots)
        {
            var oldLenght = _itemSlots.Length;
            Array.Resize(ref _itemSlots, _itemSlots.Length + newSlots);

            for (int i = oldLenght; i < _itemSlots.Length; i++)
            {
                _itemSlots[i] = new ItemSlot();
            }
            
            OnItemContainerUpdate?.Invoke();
        }
    }
}