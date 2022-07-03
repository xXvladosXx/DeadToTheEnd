using System;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;

namespace InventorySystem
{
    [Serializable]
    public class ItemContainer : IItemContainer
    {
        [SerializeField] private ItemSlot[] _itemSlots;
        [SerializeField] private ItemDatabase _database;
        public ItemSlot[] GetItemSlots => _itemSlots;
        public ItemDatabase GetDatabase => _database;

        public event Action OnItemUpdate;
        public event Action OnItemContainerUpdate;

        public ItemContainer(int size)
        {
            _itemSlots = new ItemSlot[size];
        }

        public void Init()
        {
            for (int i = 0; i < _itemSlots.Length; i++)
            {
                _itemSlots[i] ??= new ItemSlot();
            }

            if (_database == null)
            {
                _database = Resources.Load<ItemDatabase>("Database");
            }

            for (int i = 0; i < _itemSlots.Length; i++)
            {
                if (_itemSlots[i].Item == null)
                {
                    _itemSlots[i].RemoveItem();
                }
                else
                {
                    if (_itemSlots[i].Quantity == 0)
                    {
                        _itemSlots[i].Quantity = 1;
                    }
                }

                _itemSlots[i].Index = i;
            }
        }

        public void AddItem(ItemSlot itemSlot, int itemSlotIndex)
        {
            _itemSlots[itemSlotIndex].UpdateSlot(itemSlot);
            OnItemUpdate?.Invoke();
        }

        public void AddItem(ItemSlot itemSlot)
        {
            foreach (var slot in _itemSlots)
            {
                if (slot.Item == itemSlot.Item && itemSlot.Item != null)
                {
                    int slotRemainingSpace = slot.Item.MaxItemsInInventorySlot - slot.Quantity;
                    if (itemSlot.Quantity <= slotRemainingSpace)
                    {
                        slot.Quantity += itemSlot.Quantity;
                        itemSlot.RemoveItem();
                        OnItemUpdate?.Invoke();
                        return;
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
                if (_itemSlots[i].Item == null && itemSlot.Item != null)
                {
                    if (itemSlot.Quantity <= itemSlot.Item.MaxItemsInInventorySlot)
                    {
                        SetSlot(itemSlot);
                        OnItemUpdate?.Invoke();
                        return;
                    }

                    _itemSlots[i] = new ItemSlot(itemSlot.Item, itemSlot.Item.MaxItemsInInventorySlot, itemSlot.ID);
                    itemSlot.Quantity -= itemSlot.Item.MaxItemsInInventorySlot;
                }
            }

            OnItemUpdate?.Invoke();
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

        public void RemoveItem(ItemSlot itemSlot, int quantity)
        {
            Debug.Log(this + " " + _itemSlots.Length);
            for (int i = 0; i < _itemSlots.Length; i++)
            {
                if (_itemSlots[i].Item == null) continue;
                if (_itemSlots[i] == itemSlot)
                {
                    if (quantity == itemSlot.Quantity)
                    {
                        _itemSlots[i].RemoveItem();
                    }
                    else
                    {
                        _itemSlots[i].Quantity -= quantity;
                    }

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
                    //not fully works
                    return AddToSlot(draggedItem, replacedItem);
                }
            }

            var temp = new ItemSlot(replacedItem.Item, replacedItem.Quantity, replacedItem.ID);
            
            if(replacedItem.Changeable)
                replacedItem.UpdateSlot(draggedItem);
            
            if(draggedItem.Changeable)
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
                RemoveItem(draggedItem, draggedItem.Quantity);
                OnItemUpdate?.Invoke();

                return true;
            }

            draggedItem.Quantity = amountLeft;
            OnItemUpdate?.Invoke();

            return true;
        }

        public bool HasEnoughSpace()
        {
            int freeSlots = _itemSlots.Count(itemSlot => itemSlot.Item == null || itemSlot.ID < 0);

            return freeSlots > 0;
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

        public bool HasItems()
        {
            var hasItems = false;
            foreach (var slot in _itemSlots)
            {
                if (slot.Item != null)
                {
                    hasItems = true;
                }
            }

            return hasItems;
        }

        public void Clear(ClearingMode clearingMode = ClearingMode.JustClear)
        {
            if (clearingMode == ClearingMode.JustClear)
            {
                foreach (var itemsSlot in _itemSlots)
                {
                    itemsSlot.RemoveItem();
                }
            }
            else
            {
                for (int i = 0; i < _itemSlots.Length; i++)
                {
                    _itemSlots[i] = new ItemSlot();
                }
            }
        }

        public enum ClearingMode
        {
            JustClear,
            ClearAndRemove
        }
    }
}