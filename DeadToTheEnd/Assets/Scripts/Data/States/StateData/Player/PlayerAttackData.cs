using System;
using UnityEngine;

namespace Data.States.StateData.Player
{
    [Serializable]
    public class PlayerAttackData
    {
        [field: SerializeField] public float DistanceOfRaycast { get; private set; }
        [field: SerializeField] public float DistanceToStopMoving { get; private set; }
        [field: SerializeField] public float Force { get; private set; }
    }
}