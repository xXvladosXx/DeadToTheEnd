using UnityEngine;

namespace Combat.ColliderActivators
{
    public class ShortSwordColliderActivator : LongSwordColliderActivator
    {
        [field: SerializeField] public bool RightSword { get; private set; }
    }
}