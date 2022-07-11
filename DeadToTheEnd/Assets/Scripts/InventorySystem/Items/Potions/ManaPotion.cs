using Entities.Core;
using UnityEngine;

namespace InventorySystem.Potions
{
    [CreateAssetMenu (menuName = "InventorySystem/ManaPotion")]
    public class ManaPotion : ConsumableItem
    {
        public override void UseItem(AliveEntity aliveEntity)
        {
            aliveEntity.Mana.Increase(Value);
        }
    }
}