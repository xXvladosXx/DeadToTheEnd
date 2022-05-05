using System;
using UnityEngine;

namespace Data.States
{
    [Serializable]
    public sealed class PlayerRotationData
    {
        [field: SerializeField] public Vector3 TargetRotationReachTime { get; private set; }
    }
}