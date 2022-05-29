using System;
using UnityEngine;

namespace Data.States
{
    [Serializable]
    public sealed class PlayerRotationData
    {
        [field: SerializeField] public Vector3 TargetRotationReachTimeDefault { get; private set; }
        [field: SerializeField] public Vector3 TargetRotationReachTimeSprint { get; private set; }
    }
}