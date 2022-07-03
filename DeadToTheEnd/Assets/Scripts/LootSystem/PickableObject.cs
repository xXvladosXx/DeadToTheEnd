using System;
using System.Collections.Generic;
using Entities;
using InventorySystem;
using UnityEngine;
using Random = UnityEngine.Random;

namespace LootSystem
{
    public class PickableObject : MonoBehaviour, IInteractable
    {
        [SerializeField] private LootContainer _lootContainer;
        [SerializeField] private float _distanceOfRaycast;
        
        private ItemContainer _drop;

        private void Awake()
        {
            _drop = _lootContainer.GetDrop();
        }

        public void SpawnLoot(Vector3 position, float distanceOfPickableRaycast)
        {
            var loot = Instantiate(gameObject, position, Quaternion.identity);
            _distanceOfRaycast = distanceOfPickableRaycast;
        }
        
        public object ObjectOfInteraction() => _drop;

        public string TextOfInteraction() => "Press 6 to pick up";

        public float GetDistanceOfRaycast => _distanceOfRaycast;
    }
}