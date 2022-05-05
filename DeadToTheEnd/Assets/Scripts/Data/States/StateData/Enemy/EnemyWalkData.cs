using System;
using UnityEngine;

namespace Data.States.StateData.Enemy
{
    [Serializable]
    public sealed class EnemyWalkData
    {
        [field: SerializeField] public float WalkSpeedModifer { get; private set; }
    }
}