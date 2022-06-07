using UnityEngine;

namespace Combat.ColliderActivators
{
    public class ShortAttackColliderActivator : AttackColliderActivator
    {
        [field: SerializeField] public bool RightSword { get; private set; }
    }
}