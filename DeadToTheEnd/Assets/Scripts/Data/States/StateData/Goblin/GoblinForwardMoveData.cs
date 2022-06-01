using System;
using UnityEngine;

namespace Data.States.StateData.Goblin
{
    [Serializable]
    public class GoblinForwardMoveData
    {
        [field: SerializeField] public float WalkSpeedModifer { get; private set; }
        [field: SerializeField] public float DistanceToStop { get; private set; }
        [field: SerializeField] public float RotationSpeedModifer { get; private set; }
        [field: SerializeField] public float WalkMaxTime { get; private set; }
        [field: SerializeField] public float WalkMinTime { get; private set; }
    }
}