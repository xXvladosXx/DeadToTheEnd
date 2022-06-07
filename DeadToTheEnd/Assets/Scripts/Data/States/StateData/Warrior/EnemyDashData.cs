using System;
using UnityEngine;

namespace Data.States.StateData.Enemy
{
    [Serializable]
    public class EnemyDashData
    {
        [field: SerializeField] public float TimeBeforeAttack { get; private set; } = 1;
        [field: SerializeField] public float DashSpeedModifier { get; private set; } = 300;
        [field: SerializeField] public float DashAttackCooldown { get; set; } = 20f;
        [field: SerializeField] public float DashSecondSpeedModifierFirstPhase { get; private set; } = 20;
        [field: SerializeField] public float DashSecondSpeedModifierSecondPhase { get; private set; } = 5;
        [field: SerializeField] public float DashSecondAttackCooldown { get; set; } = 20f;
    }
}