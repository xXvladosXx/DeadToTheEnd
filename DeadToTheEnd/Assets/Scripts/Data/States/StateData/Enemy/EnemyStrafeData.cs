using System;
using UnityEngine;

namespace Data.States.StateData.Enemy
{
    [Serializable]
    public class EnemyStrafeData
    {
        [field: SerializeField] public float StrafeSpeedModifer { get; private set; } = .7f;
        [field: SerializeField] public float StrafeMinTime { get; private set; } = 4;
        [field: SerializeField] public float StrafeMaxTime { get; private set; } = 6;
    }
}