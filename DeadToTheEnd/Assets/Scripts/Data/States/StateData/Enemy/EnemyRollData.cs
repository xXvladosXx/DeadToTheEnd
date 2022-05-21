using System;
using UnityEngine;

namespace Data.States.StateData.Enemy
{
    [Serializable]
    public class EnemyRollData
    {
        [field: SerializeField] public float RollSpeedModifer { get; private set; } = 10f;

    }
}