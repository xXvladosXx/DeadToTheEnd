using System;
using System.Collections.Generic;
using System.Linq;
using Data.States.StateData.Player;
using Data.Stats;
using Data.Stats.Core;
using Entities;
using Entities.Core;
using InventorySystem.Core;
using LootSystem;
using SaveSystem;
using UnityEngine;
using Utilities.Raycast;

namespace InventorySystem
{
    public class ItemEquipper : MonoBehaviour, ISavable, IModifier
    {
        [field: SerializeField] public Item DefaultWeapon { get; private set; }
        [field: SerializeField] public Inventory Inventory { get; private set; }
        [field: SerializeField] public Inventory Equipment { get; private set; }
        [field: SerializeField] public Inventory QuickBarItems { get; private set; }

        public Item CurrentWeapon { get; private set; }
        public event Action OnStatModified;

        private void Awake()
        {
            CurrentWeapon = DefaultWeapon;
            
            foreach (var itemSlot in Equipment.ItemContainer.GetItemSlots)
            {
                if (itemSlot.Item != null)
                {
                    if (itemSlot.Item.ItemType == ItemType.ShortSword)
                    {
                        CurrentWeapon = itemSlot.Item;
                    }
                }
            }
           
            Equipment.ItemContainer.OnItemUpdate += EquipmentChanged;
        }

        private void EquipmentChanged()
        {
            var hasCurrentWeapon = Equipment.ItemContainer.HasItem(CurrentWeapon);
            TryToChangeWeapon(ItemType.ShortSword);

            if (!hasCurrentWeapon && CurrentWeapon == null)
            {
                TryToChangeWeapon(ItemType.LongSword);
            }

            OnStatModified?.Invoke();
        }

        public bool TryToChangeWeapon(ItemType weaponType)
        {
            var possibleWeapon =
                Equipment.ItemContainer.GetItemSlots.FirstOrDefault(slot =>
                    slot.Item != null && slot.Item.ItemType == weaponType)
                    ?.Item;
            if (weaponType == ItemType.ShortSword && possibleWeapon == null)
            {
                CurrentWeapon = DefaultWeapon;
                return true;
            }
            
            if (possibleWeapon == null) return false;

            CurrentWeapon = possibleWeapon;
            return true;
        }

        public void TryToUseItem(AliveEntity aliveEntity, int index)
        {
            var consumableItem = QuickBarItems.ItemContainer.GetItemSlots[index].Item as ConsumableItem;
            if (consumableItem != null)
            {
                consumableItem.UseItem(aliveEntity);
                QuickBarItems.ItemContainer.RemoveItem(QuickBarItems.ItemContainer.GetItemSlots[index], 1);
            }
            
            OnStatModified?.Invoke();
        }

        public object CaptureState()
        {
            var savedInventories = new SavableInventories
            {
                Inventory = new List<SavableItemSlot>(),
                Equipment = new List<SavableItemSlot>(),
                QuickBarItems = new List<SavableItemSlot>()
            };

            foreach (var itemSlot in Inventory.ItemContainer.GetItemSlots)
            {
                var savableItemSlot = new SavableItemSlot(itemSlot.ID, itemSlot.Quantity, itemSlot.Index);
                savedInventories.Inventory.Add(savableItemSlot);
            }

            foreach (var itemSlot in Equipment.ItemContainer.GetItemSlots)
            {
                var savableItemSlot = new SavableItemSlot(itemSlot.ID, itemSlot.Quantity, itemSlot.Index);
                savedInventories.Equipment.Add(savableItemSlot);
            }

            foreach (var itemSlot in QuickBarItems.ItemContainer.GetItemSlots)
            {
                var savableItemSlot = new SavableItemSlot(itemSlot.ID, itemSlot.Quantity, itemSlot.Index);
                savedInventories.QuickBarItems.Add(savableItemSlot);
            }

            return savedInventories;
        }

        public void RestoreState(object state)
        {
            Inventory.ItemContainer.Clear();
            Equipment.ItemContainer.Clear();
            QuickBarItems.ItemContainer.Clear();

            var savedInventories = (SavableInventories) state;

            foreach (var itemSlot in savedInventories.Inventory)
            {
                var slot = new ItemSlot(Inventory.ItemContainer.GetDatabase.GetItemByID(itemSlot.ItemId),
                    itemSlot.Quantity, itemSlot.ItemId);
                Inventory.ItemContainer.AddItem(slot, itemSlot.Index);
            }

            foreach (var itemSlot in savedInventories.Equipment)
            {
                var slot = new ItemSlot(Inventory.ItemContainer.GetDatabase.GetItemByID(itemSlot.ItemId),
                    itemSlot.Quantity, itemSlot.ItemId);
                Equipment.ItemContainer.AddItem(slot, itemSlot.Index);
            }

            foreach (var itemSlot in savedInventories.QuickBarItems)
            {
                var slot = new ItemSlot(Inventory.ItemContainer.GetDatabase.GetItemByID(itemSlot.ItemId),
                    itemSlot.Quantity, itemSlot.ItemId);
                QuickBarItems.ItemContainer.AddItem(slot, itemSlot.Index);
            }
        }


        public IEnumerable<IBonus> AddBonus(Stat[] stats)
        {
            IEnumerable<IBonus> AllModifierBonuses(IModifier modifier)
                => modifier.AddBonus(stats);

            var itemSlots =
                Equipment.ItemContainer.GetItemSlots;
            var modifiableItems = new List<IModifier>();
            foreach (var itemSlot in itemSlots)
            {
                if (itemSlot.Item is IModifier modifier)
                {
                    if (modifier.GetType() == typeof(Weapon.Weapon) && itemSlot.Item != CurrentWeapon)
                    {
                        continue;
                    }
                    modifiableItems.Add(modifier);
                }
            }

            return modifiableItems.SelectMany(AllModifierBonuses);
        }

        [Serializable]
        public class SavableInventories
        {
            public List<SavableItemSlot> Inventory;
            public List<SavableItemSlot> Equipment;
            public List<SavableItemSlot> QuickBarItems;
        }
    }
}