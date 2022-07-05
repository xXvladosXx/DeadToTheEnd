using Entities.Core;
using UnityEngine;

namespace InventorySystem.Potions
{
    [CreateAssetMenu (menuName = "InventorySystem/HealthPotion")]
    public class HealthPotion : ConsumableItem
    {
        public override void UseItem(AliveEntity aliveEntity)
        {
            aliveEntity.Health.Increase(Value);
        }
    }
}