using System;
using UnityEngine;

namespace Data.States.StateData.Enemy
{
    [Serializable]
    public sealed class EnemyRunData
    {
        [field: SerializeField] public float RunSpeedModifer { get; private set; }
    }
}