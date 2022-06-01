using UnityEngine;

namespace Combat.ColliderActivators
{
    public class ShortSwordColliderActivator : SwordColliderActivator
    {
        [field: SerializeField] public bool RightSword { get; private set; }
    }
}