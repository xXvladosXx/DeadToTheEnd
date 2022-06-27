using System;
using Entities;
using InventorySystem;
using UnityEngine;

namespace LootSystem
{
    public class PickableObject : MonoBehaviour, IInteractable
    {
        [SerializeField] private LootContainer _lootContainer;
        
        public object ObjectOfInteraction()
        {
            Debug.Log("Picking");

            return _lootContainer;
        }

        private void Update()
        {
            if(_lootContainer.Items.Count == 0)
                Destroy(gameObject);
        }

        public string TextOfInteraction()
        {
            return "Press 6 to pick up";
        }
    }
}