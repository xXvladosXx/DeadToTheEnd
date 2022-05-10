using System;
using UnityEngine;

namespace Data.States.StateData.Enemy
{
    [Serializable]
    public class EnemyFollowData
    {
        [field: SerializeField] public float DistanceToStartOrdinaryAttack { get; private set; } = 2f;
        [field: SerializeField] public float DistanceToStartDashAttack { get; private set; } = 4f;
        [field: SerializeField] public float DistanceToRollBack { get; private set; } = 4f;
    }
}