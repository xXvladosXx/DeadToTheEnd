using System;
using System.Collections.Generic;
using Data.Camera;
using UnityEngine;

namespace Data.States
{
    [Serializable]
    public sealed class PlayerStateReusableData
    {
        public List<PlayerCameraRecenteringData> BackCameraRecenteringDatas { get; set; }
        public List<PlayerCameraRecenteringData> SideCameraRecenteringDatas { get; set; }

        public Vector2 MovementInput { get; set; }
        [field: SerializeField] public float MovementSpeedModifier { get; set; } = 1f;
        public float MovementSlopeSpeedModifier { get; set; } = 1f;
        public float MovementDecelerationForce { get; set; } = 1f;
        public bool ShouldWalk { get; set; }
        public bool ShouldSprint { get; set; }
        public bool CanMakeCombo { get; set; }
        public bool ComboWasMade { get; set; }
        
        private Vector3 _currentTargetRotation;
        private Vector3 _timeToReachTargetRotation;
        private Vector3 _dampedTargetRotationCurrentVelocity;
        private Vector3 _dampedTargetRotationPassedTime;

        public ref Vector3 CurrentTargetRotation => ref _currentTargetRotation;
        public ref Vector3 TimeToReachTargetRotation => ref _timeToReachTargetRotation;
        public ref Vector3 DampedTargetRotationCurrentVelocity => ref _dampedTargetRotationCurrentVelocity;
        public ref Vector3 DampedTargetRotationPassedTime => ref _dampedTargetRotationPassedTime;
        public Vector3 CurrentJumpForce { get; set; }

        public PlayerRotationData RotationData { get; set; }
        public bool IsMovingAfterStop { get; set; }
    }
}