using System;
using UnityEngine;

namespace Data.States.StateData.Enemy
{
    [Serializable]
    public sealed class EnemyWalkData
    {
        [field: SerializeField] public float WalkSpeedModifer { get; private set; }
        [field: SerializeField] public float WalkMinTime { get; private set; } = 3;
        [field: SerializeField] public float WalkMaxTime { get; private set; } = 5;
        [field: SerializeField] public float StoppingDistance { get; private set; } = 3;
    }
}