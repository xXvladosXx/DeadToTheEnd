using System;
using UnityEngine;

namespace Data.States.StateData.Enemy
{
    [Serializable]
    public class EnemyAttackData
    {
        [field: SerializeField] public float DistanceToStartOrdinaryAttack { get; private set; } = 3f;
        [field: SerializeField] public float DistanceToStartDashAttack { get; private set; } = 10f;
        [field: SerializeField] public float DistanceToStartSecondDashAttack { get; private set; } = 10f;
        [field: SerializeField] public float DistanceToStartComboFirstAttack { get; private set; } = 5f;
        [field: SerializeField] public float DistanceToStartComboSecondAttack { get; private set; } = 6f;
        [field: SerializeField] public float MinTimeToAttack { get; private set; } = .5f;
        [field: SerializeField] public float MaxTimeToAttack { get; private set; } = 2f;
    }
}