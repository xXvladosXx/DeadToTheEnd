using System;
using UnityEngine;

namespace Data.States.StateData.Enemy
{
    [Serializable]
    public sealed class EnemyIdleData
    {
        [field: SerializeField] public float TimeOfIdlePositioning { get; private set; } = 1;
        [field: SerializeField] public float IdleSpeedModifer { get; private set; } = 0f;
        [field: SerializeField] public float RotationSpeedModifer { get; private set; } = 15;
        [field: SerializeField] public float DistanceToFindTarget { get; private set; } = 10;
    }
}