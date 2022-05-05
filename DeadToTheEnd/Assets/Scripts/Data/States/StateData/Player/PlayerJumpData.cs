using System;
using UnityEngine;

namespace Data.States
{
    [Serializable]
    public sealed class PlayerJumpData
    {
        [field: SerializeField] public Vector3 StationaryForce { get; private set; }
        [field: SerializeField] public Vector3 WeakForce { get; private set; }
        [field: SerializeField] public Vector3 MediumForce { get; private set; }
        [field: SerializeField] public Vector3 StrongForce { get; private set; }
        [field: SerializeField] public AnimationCurve JumpForceModifierOnSlopeUp { get; private set; }
        [field: SerializeField] public AnimationCurve JumpForceModifierOnSlopeDown { get; private set; }
        [field: SerializeField] public float JumpToGroundRayDistance { get; private set; } = 2f;
        [field: SerializeField] public PlayerRotationData RotationData { get; private set; }
        [field: SerializeField] public float DecelerationForce { get; private set; } = 1.5f;
    }
}