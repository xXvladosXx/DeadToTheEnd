using System;
using UnityEngine;

namespace Data.States.StateData.Goblin
{
    [Serializable]
    public class GoblinFollowData
    {
        [field: SerializeField] public float MinTimeToWait { get; private set; }
        [field: SerializeField] public float MaxTimeToWait { get; private set; }
        [field: SerializeField] public float DistanceToRoll { get; private set; }
        [field: SerializeField] public float DistanceToStrafe { get; private set; }

    }
}