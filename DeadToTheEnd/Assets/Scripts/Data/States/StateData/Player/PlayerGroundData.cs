using System;
using System.Collections.Generic;
using Data.Camera;
using UnityEngine;

namespace Data.States.StateData.Player
{
    [Serializable]
    public sealed class PlayerGroundData
    {
        [field: SerializeField] public float BaseSpeed { get; private set; } = 5f;
        [field: SerializeField] public float SmoothSpeedModifier { get; private set; } = 1.5f;
        [field: SerializeField] public float GroundToFallRayDistance { get; private set; } = 1f;
        [field: SerializeField] public AnimationCurve SlopeSpeedAngles { get; private set; }
        [field: SerializeField] public PlayerRotationData BaseRotationData { get; private set; }
        [field: SerializeField] public WalkData WalkData { get; private set; }
        [field: SerializeField] public PlayerIdleData IdleData { get; private set; }
        [field: SerializeField] public PlayerRunData PlayerRunData { get; private set; }
        [field: SerializeField] public PlayerSprintData SprintData { get; private set; }
        [field: SerializeField] public PlayerDashData DashData { get; private set; }
        [field: SerializeField] public PlayerStopData StopData { get; private set; }
        [field: SerializeField] public PlayerRollData RollData { get; private set; }
        [field: SerializeField] public PlayerAttackData AttackData { get; private set; }
        [field: SerializeField] public List<PlayerCameraRecenteringData> SideCameraRecenteringDatas { get; private set; }
        [field: SerializeField] public List<PlayerCameraRecenteringData> BackCameraRecenteringDatas { get; private set; }
    }
}