using System;
using UnityEngine;

namespace Data.States
{
    [Serializable]
    public class PlayerSprintData
    {
        [field: SerializeField] public float SpeedModifier { get; private set; } = 1.7f;
        [field: SerializeField] public float SprintToRunTime { get; private set; } = 1f;
        [field: SerializeField] public float RunToWalkTime { get; private set; } = .5f;
    }
}