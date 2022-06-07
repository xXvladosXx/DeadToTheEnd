using System;
using System.Collections.Generic;
using Data.Camera;
using Data.States.StateData;
using UnityEngine;
using Utilities;

namespace Data.States
{
    [Serializable]
    public sealed class PlayerStateReusableData : IReusable
    {
        public List<PlayerCameraRecenteringData> BackCameraRecenteringDatas { get; set; }
        public List<PlayerCameraRecenteringData> SideCameraRecenteringDatas { get; set; }

        public Vector2 MovementInputWithNormalization { get; set; }
        public Vector2 MovementInputWithoutNormalization { get; set; }
        
        public float MovementSpeedModifier { get; set; } = 1f;
        public float MovementSlopeSpeedModifier { get; set; } = 1f;
        public float MovementDecelerationForce { get; set; } = 1f;
        public bool ShouldSprint { get; set; }
        public bool ShouldBlock { get; set; }
        public bool ShouldCombo { get; set; }
        public bool StopReading { get; set; }
        
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
        
        public Transform Target { get; set; }
        public bool IsKnocked { get; set; }

        public bool IsBlocking { get; set; }
        public bool IsRolling { get; set; }
        public bool IsTargetBehind { get; set; }
        public bool IsPerformingAction { get; set; }
        public bool IsRotatingWithRootMotion { get; set; }
        public float MovementSpeed { get; set; }
        public bool ShouldAttack { get; set; }
        public bool LockedState { get; set; }
    }
}