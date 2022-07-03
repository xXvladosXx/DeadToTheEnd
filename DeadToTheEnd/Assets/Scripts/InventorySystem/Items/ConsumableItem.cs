using Data.Stats;
using Entities.Core;
using UnityEngine;

namespace InventorySystem
{
    public abstract class ConsumableItem : InventoryItem
    {
        [SerializeField] protected Stat Stat;
        [SerializeField] protected float Value;
        
        public abstract void UseItem(AliveEntity aliveEntity);
    }
}