using System;
using UnityEngine;

namespace Data.States.StateData.Enemy
{
    [Serializable]
    public sealed class EnemyIdleData
    {
        [field: SerializeField] public float TimeOfIdlePositioning { get; private set; }
        [field: SerializeField] public float IdleSpeedModifer { get; private set; } = 0f;

    }
}