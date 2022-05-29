using System;
using System.Collections.Generic;
using Data.Animations;
using Data.Camera;
using Data.Collider;
using Data.Combat;
using Data.States;
using DefaultNamespace;
using Entities;
using UnityEngine;
using UnityEngine.InputSystem;
using PlayerInput = Utilities.PlayerInput;

namespace AnimatorStateMachine.Movement
{
    public abstract class BaseMovementAnimatorState  : BaseAnimatorState
    {
        protected MainPlayer MainPlayer;
        private SlopeData SlopeData;
       
        protected PlayerGroundData GroundedData;

        protected PlayerAnimationData PlayerAnimationData;
        private float _time;

        public override void OnEnter(DefaultNamespace.AnimatorStateMachine characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            MainPlayer = animator.GetComponent<MainPlayer>();
            GroundedData = MainPlayer.PlayerData.GroundData;
            PlayerAnimationData = MainPlayer.PlayerAnimationData;
            MainPlayer.Animator.applyRootMotion = false;
            
            _time = 0;
            AddInputCallbacks();

            InitializeData();
            ResetVelocity();

        }

        private void HealthOnDamageTaken(AttackData attackData)
        {
            if(MainPlayer.ReusableData.IsKnockned)
                return;

            if (MainPlayer.ReusableData.WasHit)
            {
                MainPlayer.Animator.SetBool(PlayerAnimationData.KnockdownParameterHash, true);
                return;
            }
            
            switch (attackData.AttackType)
            {
                case AttackType.Knock:
                    MainPlayer.Animator.SetBool(PlayerAnimationData.KnockdownParameterHash, true);
                    break;
                case AttackType.Medium:
                    MainPlayer.Animator.SetBool(PlayerAnimationData.MediumHitParameterHash, true);
                    break;
                case AttackType.Easy:
                    MainPlayer.Animator.SetBool(PlayerAnimationData.EasyHitParameterHash, true);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public override void OnUpdate(DefaultNamespace.AnimatorStateMachine characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            ReadMovementInputWithNormalization();
            ReadMovementInputWithoutNormalization();
            Move();
        }

      
        protected void TargetLocked()
        {
                Transform transform;
                (transform = MainPlayer.transform).LookAt(MainPlayer.ReusableData.Target);
                transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
        }

        public override void OnExit(DefaultNamespace.AnimatorStateMachine characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            RemoveInputCallbacks();
        }

        protected void SetBaseCameraRecenteringData()
        {
            MainPlayer.ReusableData.SideCameraRecenteringDatas = GroundedData.SideCameraRecenteringDatas;
            MainPlayer.ReusableData.BackCameraRecenteringDatas = GroundedData.BackCameraRecenteringDatas;
        }

        protected void SetBaseRotationData()
        {
            MainPlayer.ReusableData.RotationData = GroundedData.BaseRotationData;

            MainPlayer.ReusableData.TimeToReachTargetRotation = MainPlayer.ReusableData.RotationData.TargetRotationReachTimeDefault;
                 }
        protected virtual void AddInputCallbacks()
        {
            MainPlayer.InputAction.PlayerActions.Dash.performed += OnDashStarted;
            MainPlayer.InputAction.PlayerActions.Movement.performed += OnMovementPerformed;
            MainPlayer.InputAction.PlayerActions.Movement.canceled += OnMovementCanceled;
            MainPlayer.InputAction.PlayerActions.Attack.performed += OnAttackCalled;
        }

        private void OnAttackCalled(InputAction.CallbackContext obj)
        {
            MainPlayer.Animator.SetBool(PlayerAnimationData.Attack1ParameterHash, true);
        }

        protected virtual void OnMovementCanceled(InputAction.CallbackContext obj)
        {
            
        }

        private void OnMovementPerformed(InputAction.CallbackContext obj)
        {
            MainPlayer.Animator.SetBool(MainPlayer.PlayerAnimationData.MovingParameterHash, true);
        }

        private void OnDashStarted(InputAction.CallbackContext obj)
        {
            MainPlayer.Animator.SetBool(MainPlayer.PlayerAnimationData.SprintParameterHash, true);
        }
        protected virtual void RemoveInputCallbacks()
        {
            MainPlayer.InputAction.PlayerActions.Dash.performed -= OnDashStarted;
            MainPlayer.InputAction.PlayerActions.Movement.performed -= OnMovementPerformed;
            MainPlayer.InputAction.PlayerActions.Movement.canceled -= OnMovementCanceled;
            MainPlayer.InputAction.PlayerActions.Attack.performed -= OnAttackCalled;
        }
        
        private void InitializeData()
        {
            SetBaseCameraRecenteringData();

            SetBaseRotationData();
        }

        protected void ReadMovementInputWithNormalization()
        {
            MainPlayer.ReusableData.MovementInputWithNormalization = MainPlayer.InputAction.PlayerActions.Movement.ReadValue<Vector2>();
        }

        private void ReadMovementInputWithoutNormalization()
        {
            MainPlayer.ReusableData.MovementInputWithoutNormalization = MainPlayer.InputAction.PlayerActions.MovementWithoutNormalization.ReadValue<Vector2>();
        }

        protected void Move()
        {
            if (MainPlayer.ReusableData.MovementInputWithNormalization == Vector2.zero ||
                MainPlayer.ReusableData.MovementSpeedModifier == 0f)
            {            
                MainPlayer.Animator.SetFloat(PlayerAnimationData.SpeedParameterHash, 0f, 0.05f, Time.deltaTime);
                MainPlayer.Animator.SetBool(PlayerAnimationData.MovingParameterHash, false);
                _time = 0;
                
                return;
            }
          
            var movementDirection = GetMovementInputDirection();
            var targetRotationAngle = Rotate(movementDirection);
           
            
            var targetRotationDirection = GetTargetRotationDirection(targetRotationAngle);
            var movementSpeed = GetSmoothMovementSpeed();
            
            var currentPlayerHorizontalVelocity = GetPlayerHorizontalVelocity();
            
            MainPlayer.Animator.SetBool(PlayerAnimationData.MovingParameterHash, true);
            MainPlayer.Animator.SetBool(PlayerAnimationData.WasMovingParameterHash, true);
            MainPlayer.Animator.SetFloat(PlayerAnimationData.SpeedParameterHash, MainPlayer.ReusableData.MovementSpeedModifier, 0.3f, Time.deltaTime);
            
            MainPlayer.Rigidbody.AddForce(targetRotationDirection * movementSpeed - currentPlayerHorizontalVelocity, ForceMode.VelocityChange);
        }

        private float GetSmoothMovementSpeed()
        {
            _time += Time.deltaTime * GroundedData.SmoothSpeedModifier;
            float movementSpeed = GetMaxMovementSpeed();
            if (MainPlayer.ReusableData.IsMovingAfterStop)
            {
                movementSpeed = Mathf.Lerp(0, GetMaxMovementSpeed(), _time);
            }

            if (GetMaxMovementSpeed() == movementSpeed)
            {
                MainPlayer.ReusableData.IsMovingAfterStop = false;
            }

            return movementSpeed;
        }

        protected Vector3 GetMovementInputDirection()
        {
            return new Vector3(MainPlayer.ReusableData.MovementInputWithNormalization.x, 0f, MainPlayer.ReusableData.MovementInputWithNormalization.y);
        }

        protected float Rotate(Vector3 direction)
        {
            float directionAngle = UpdateTargetRotation(direction);

            RotateTowardsTargetRotation();

            return directionAngle;
        }

        protected float UpdateTargetRotation(Vector3 direction, bool shouldConsiderCameraRotation = true)
        {
            float directionAngle = GetDirectionAngle(direction);

            if (shouldConsiderCameraRotation)
            {
                directionAngle = AddCameraRotationToAngle(directionAngle);
            }

            if (directionAngle != MainPlayer.ReusableData.CurrentTargetRotation.y)
            {
                UpdateTargetRotationData(directionAngle);
            }

            return directionAngle;
        }

        private float GetDirectionAngle(Vector3 direction)
        {
            float directionAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;

            if (directionAngle < 0f)
            {
                directionAngle += 360f;
            }

            return directionAngle;
        }

        private float AddCameraRotationToAngle(float angle)
        {
            angle += MainPlayer.MainCamera.eulerAngles.y;

            if (angle > 360f)
            {
                angle -= 360f;
            }

            return angle;
        }

        private void UpdateTargetRotationData(float targetAngle)
        {
            MainPlayer.ReusableData.CurrentTargetRotation.y = targetAngle;

            MainPlayer.ReusableData.DampedTargetRotationPassedTime.y = 0f;
        }

        protected void RotateTowardsTargetRotation()
        {
            float currentYAngle = MainPlayer.Rigidbody.rotation.eulerAngles.y;

            if (currentYAngle == MainPlayer.ReusableData.CurrentTargetRotation.y)
            {
                return;
            }

            float smoothedYAngle = Mathf.SmoothDampAngle(currentYAngle, MainPlayer.ReusableData.CurrentTargetRotation.y,
                ref MainPlayer.ReusableData.DampedTargetRotationCurrentVelocity.y, 
                MainPlayer.ReusableData.TimeToReachTargetRotation.y - MainPlayer.ReusableData.DampedTargetRotationPassedTime.y);

            MainPlayer.ReusableData.DampedTargetRotationPassedTime.y += Time.deltaTime;

            Quaternion targetRotation = Quaternion.Euler(0f, smoothedYAngle, 0f);

            MainPlayer.Rigidbody.MoveRotation(targetRotation);
        }

        protected Vector3 GetTargetRotationDirection(float targetRotationAngle)
        {
            return Quaternion.Euler(0f, targetRotationAngle, 0f) * Vector3.forward;
        }

        protected float GetMaxMovementSpeed(bool shouldConsiderSlopes = true)
        {
            float movementSpeed = GroundedData.BaseSpeed * MainPlayer.ReusableData.MovementSpeedModifier;

            if (shouldConsiderSlopes)
            {
                movementSpeed *= MainPlayer.ReusableData.MovementSlopeSpeedModifier;
            }

            return movementSpeed;
        }
        
       
        protected Vector3 GetPlayerHorizontalVelocity()
        {
            Vector3 playerHorizontalVelocity = MainPlayer.Rigidbody.velocity;

            playerHorizontalVelocity.y = 0f;

            return playerHorizontalVelocity;
        }

        protected Vector3 GetPlayerVerticalVelocity()
        {
            return new Vector3(0f, MainPlayer.Rigidbody.velocity.y, 0f);
        }

        protected void SetCameraRecenteringState(float cameraVerticalAngle, List<PlayerCameraRecenteringData> cameraRecenteringData)
        {
            foreach (PlayerCameraRecenteringData recenteringData in cameraRecenteringData)
            {
                if (!recenteringData.IsWithinRange(cameraVerticalAngle))
                {
                    continue;
                }

                EnableCameraRecentering(recenteringData.WaitTime, recenteringData.RecenteringTime);

                return;
            }

            DisableCameraRecentering();
        }

        protected void EnableCameraRecentering(float waitTime = -1f, float recenteringTime = -1f)
        {
            float movementSpeed = GetMaxMovementSpeed();

            if (movementSpeed == 0f)
            {
                movementSpeed = GroundedData.BaseSpeed;
            }

            MainPlayer.CameraUtility.EnableRecentering(waitTime, recenteringTime, GroundedData.BaseSpeed, movementSpeed);
        }

        protected void DisableCameraRecentering()
        {
            MainPlayer.CameraUtility.DisableRecentering();
        }

        protected void ResetVelocity()
        {
            MainPlayer.Rigidbody.velocity = Vector3.zero;
        }

        protected void ResetVerticalVelocity()
        {
            Vector3 playerHorizontalVelocity = GetPlayerHorizontalVelocity();

            MainPlayer.Rigidbody.velocity = playerHorizontalVelocity;
        }

        protected void DecelerateHorizontally()
        {
            Vector3 playerHorizontalVelocity = GetPlayerHorizontalVelocity();

            MainPlayer.Rigidbody.AddForce(-playerHorizontalVelocity * MainPlayer.ReusableData.MovementDecelerationForce, ForceMode.Acceleration);
        }

        protected void DecelerateVertically()
        {
            Vector3 playerVerticalVelocity = GetPlayerVerticalVelocity();

            MainPlayer.Rigidbody.AddForce(-playerVerticalVelocity * MainPlayer.ReusableData.MovementDecelerationForce, ForceMode.Acceleration);
        }

        protected bool IsMovingHorizontally(float minimumMagnitude = 0.1f)
        {
            Vector3 playerHorizontaVelocity = GetPlayerHorizontalVelocity();

            Vector2 playerHorizontalMovement = new Vector2(playerHorizontaVelocity.x, playerHorizontaVelocity.z);

            return playerHorizontalMovement.magnitude > minimumMagnitude;
        }

        protected bool IsMovingUp(float minimumVelocity = 0.1f)
        {
            return GetPlayerVerticalVelocity().y > minimumVelocity;
        }

        protected bool IsMovingDown(float minimumVelocity = 0.1f)
        {
            return GetPlayerVerticalVelocity().y < -minimumVelocity;
        }
    }
}