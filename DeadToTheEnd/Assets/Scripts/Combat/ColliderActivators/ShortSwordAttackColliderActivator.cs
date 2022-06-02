using UnityEngine;

namespace Combat.ColliderActivators
{
    public class ShortSwordAttackColliderActivator : SwordAttackColliderActivator
    {
        [field: SerializeField] public bool RightSword { get; private set; }
    }
}