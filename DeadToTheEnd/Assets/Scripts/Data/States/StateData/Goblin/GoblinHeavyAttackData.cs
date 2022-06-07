using System;
using UnityEngine;

namespace Data.States.StateData.Goblin
{
    [Serializable]
    public class GoblinHeavyAttackData
    {
        [field: SerializeField] public float WalkSpeedModifer { get; private set; }
        [field: SerializeField] public float DistanceToStartAttack { get; private set; }
        [field: SerializeField] public float AttackCooldown { get; private set; }
    }
}