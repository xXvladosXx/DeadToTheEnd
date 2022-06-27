using System;
using GameCore;
using UI.Inventory.ItemContainers;
using UnityEngine;

namespace UI
{
    public class CharacterContainerUI : ItemContainerManagerUI
    {
        [SerializeField] private EquipmentItemContainerUI _equipmentItemContainerUI;
        [SerializeField] private InventoryItemContainerUI _inventoryItemContainerUI;
        
        public override void OnCreate(InteractorsBase interactorsBase)
        {
            
        }

      
       
    }
}