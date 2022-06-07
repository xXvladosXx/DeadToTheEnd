using System;
using UnityEngine;

namespace Data.States.StateData.Enemy
{
    [Serializable]
    public class EnemyPatrolData
    {
        [field: SerializeField] public float RadiusToPatrol { get; private set; } = 10f;
        [field: SerializeField] public float DistanceToFindTarget { get; private set; } = 10f;
    }
}