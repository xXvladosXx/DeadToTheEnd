using System;
using UnityEngine;

namespace Data.States
{
    [Serializable]
    public class PlayerGroundData
    {
        [field: SerializeField] public float BaseSpeed { get; private set; } = 5f;
        [field: SerializeField] public float GroundToFallRayDistance { get; private set; } = 1f;
        [field: SerializeField] public AnimationCurve SlopeSpeedAngles { get; private set; }
        [field: SerializeField] public PlayerRotationData BaseRotationData { get; private set; }
        [field: SerializeField] public PlayerWalkData WalkData { get; private set; }
        [field: SerializeField] public PlayerRunData RunData { get; private set; }
        [field: SerializeField] public PlayerSprintData SprintData { get; private set; }
        [field: SerializeField] public PlayerDashData DashData { get; private set; }
        [field: SerializeField] public PlayerStopData StopData { get; private set; }
    }
}