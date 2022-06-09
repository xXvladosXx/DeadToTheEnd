using System;
using System.Collections.Generic;
using CameraManage;
using Data.Animations;
using Data.Camera;
using Data.Combat;
using Data.States;
using Entities;
using UnityEngine;
using UnityEngine.InputSystem;

namespace StateMachine.Player.States.Movement
{
    public abstract class PlayerMovementState : IState
    {
        protected PlayerStateMachine PlayerStateMachine;

        protected PlayerAnimationData PlayerAnimationData;

        protected readonly MainPlayer MainPlayer;
        protected readonly PlayerGroundData PlayerGroundData;
        private float _calculateTime;
        
        public PlayerMovementState(PlayerStateMachine playerStateMachine)
        {
            PlayerStateMachine = playerStateMachine;

            MainPlayer = PlayerStateMachine.MainPlayer;
            PlayerGroundData = PlayerStateMachine.MainPlayer.PlayerData.GroundData;
            PlayerAnimationData = MainPlayer.PlayerAnimationData;

            InitializeData();
        }

        private void InitializeData()
        {
            SetBaseCameraRecenteringData();

            SetBaseRotationData();
        }

        public virtual void Enter()
        {
            AddInputCallbacks();
        }

        public virtual void Exit()
        {
            RemoveInputCallbacks();
        }

        public virtual void Update()
        {     
        }

        public virtual void FixedUpdate()
        {
            Move();
        }

        public virtual void OnAnimationEnterEvent()
        {
        }

        public virtual void OnAnimationExitEvent()
        {
        }

        public virtual void OnAnimationHandleEvent()
        {
        }

        public virtual void HandleInput()
        {
            if(!MainPlayer.PlayerStateReusable.StopReading)
                ReadMovementInput();
        }

        private void ReadMovementInput()
        {
            MainPlayer.PlayerStateReusable.MovementInputWithNormalization =
                MainPlayer.InputAction.PlayerActions.Movement.ReadValue<Vector2>();
            MainPlayer.PlayerStateReusable.MovementInputWithoutNormalization = MainPlayer.InputAction.PlayerActions
                .MovementWithoutNormalization.ReadValue<Vector2>();
        }

        protected virtual void AddInputCallbacks()
        {
            MainPlayer.InputAction.PlayerActions.Movement.performed += OnMovementPerformed;
            MainPlayer.InputAction.PlayerActions.Movement.canceled += OnMovementCanceled;
            MainPlayer.InputAction.PlayerActions.Attack.performed += OnAttackPerformed;
            MainPlayer.InputAction.PlayerActions.Locked.performed += OnLockedPerformed;
            MainPlayer.AttackCalculator.OnDamageTaken += OnDamageTaken;
        }

        
        protected virtual void OnLockedPerformed(InputAction.CallbackContext obj)
        {
            if (MainPlayer.GetComponent<EnemyLockOn>().FindTarget())
            {
                MainPlayer.PlayerStateReusable.LockedState = true;
                MainPlayer.PlayerStateReusable.Target = MainPlayer.GetComponent<EnemyLockOn>().ScanNearBy();
                MainPlayer.LongSwordActivator.ActivateSword();
                
                PlayerStateMachine.ChangeState(PlayerStateMachine.PlayerLockedMovementState);
                
                foreach (var shortSwordActivator in MainPlayer.ShortSwordsActivator)
                    shortSwordActivator.DeactivateSword();
                
                CinemachineCameraSwitcher.Instance.ChangeCamera();
            }
        }

        protected virtual void OnDamageTaken(AttackData attackData)
        {
            if(MainPlayer.PlayerStateReusable.IsKnocked) return;

            MainPlayer.PlayerStateReusable.LastHitFromTarget = attackData.User.transform;
            switch (attackData.AttackType)
            {
                case AttackType.Knock:
                    PlayerStateMachine.ChangeState(PlayerStateMachine.PlayerKnockHitState);
                    break;
                case AttackType.Medium:
                    PlayerStateMachine.ChangeState(PlayerStateMachine.PlayerMediumHitState);
                    break;
                case AttackType.Easy:
                    PlayerStateMachine.ChangeState(PlayerStateMachine.PlayerLightHitState);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }    
        }
        
        protected virtual void OnAttackPerformed(InputAction.CallbackContext obj)
        {
            MainPlayer.PlayerStateReusable.ShouldAttack = true;
        }

        protected virtual void OnMovementCanceled(InputAction.CallbackContext obj)
        {
            DisableCameraRecentering();
        }

        protected virtual void OnMovementPerformed(InputAction.CallbackContext obj)
        {
        }

        protected virtual void RemoveInputCallbacks()
        {
            MainPlayer.InputAction.PlayerActions.Movement.performed -= OnMovementPerformed;
            MainPlayer.InputAction.PlayerActions.Movement.canceled -= OnMovementCanceled;
            MainPlayer.InputAction.PlayerActions.Attack.performed -= OnAttackPerformed;
            MainPlayer.InputAction.PlayerActions.Locked.performed -= OnLockedPerformed;
            MainPlayer.AttackCalculator.OnDamageTaken -= OnDamageTaken;
        }

        protected void SetBaseCameraRecenteringData()
        {
            MainPlayer.PlayerStateReusable.SideCameraRecenteringDatas = PlayerGroundData.SideCameraRecenteringDatas;
            MainPlayer.PlayerStateReusable.BackCameraRecenteringDatas = PlayerGroundData.BackCameraRecenteringDatas;
        }

        protected void SetBaseRotationData()
        {
            MainPlayer.PlayerStateReusable.RotationData = PlayerGroundData.BaseRotationData;
            MainPlayer.PlayerStateReusable.TimeToReachTargetRotation =
                MainPlayer.PlayerStateReusable.RotationData.TargetRotationReachTimeDefault;
        }

        protected void StartAnimation(int animation)
        {
            MainPlayer.Animator.SetBool(animation, true);
        }

        protected void StopAnimation(int animation)
        {
            MainPlayer.Animator.SetBool(animation, false);
        }

        private void Move()
        {
            if (MainPlayer.PlayerStateReusable.MovementInputWithNormalization == Vector2.zero ||
                MainPlayer.PlayerStateReusable.MovementSpeedModifier == 0f)
            {
                MainPlayer.Animator.SetFloat(PlayerAnimationData.SpeedParameterHash, 0f,
                    0.05f, UnityEngine.Time.deltaTime);
                MainPlayer.Animator.SetBool(PlayerAnimationData.MovingParameterHash, false);
                return;
            }

            var movementDirection = GetMovementInputDirection();

            var targetRotationAngle = Rotate(movementDirection);

            var targetRotationDirection = GetTargetRotationDirection(targetRotationAngle);
            var movementSpeed = GetSmoothMovementSpeed();

            var currentPlayerHorizontalVelocity = GetPlayerHorizontalVelocity();

            MainPlayer.Animator.SetBool(PlayerAnimationData.MovingParameterHash, true);
            MainPlayer.Animator.SetBool(PlayerAnimationData.WasMovingParameterHash, true);
            MainPlayer.Animator.SetFloat(PlayerAnimationData.SpeedParameterHash,
                MainPlayer.PlayerStateReusable.MovementSpeedModifier,
                0.5f, UnityEngine.Time.deltaTime);

            MainPlayer.Rigidbody.AddForce(targetRotationDirection * movementSpeed - currentPlayerHorizontalVelocity,
                ForceMode.VelocityChange);
        }
        protected void LookAtHitDirection()
        {
            Transform transform;
            (transform = MainPlayer.transform).LookAt(MainPlayer.PlayerStateReusable.LastHitFromTarget);
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
        }
        private float GetSmoothMovementSpeed()
        {
            _calculateTime += UnityEngine.Time.deltaTime ;

            float movementSpeed = GetMaxMovementSpeed();
            
            if (MainPlayer.PlayerStateReusable.IsMovingAfterStop)
            {
                movementSpeed = Mathf.Lerp(0, GetMaxMovementSpeed(), _calculateTime);
            }

            if (GetMaxMovementSpeed() == movementSpeed)
            {
                MainPlayer.PlayerStateReusable.IsMovingAfterStop = false;
            }

            return movementSpeed;
        }

        protected void ResetAnimatorSpeed()
        {
            _calculateTime = 0;
            MainPlayer.Animator.SetFloat(PlayerAnimationData.SpeedParameterHash, 0);
        }

        protected float GetMaxMovementSpeed(bool shouldConsiderSlopes = true)
        {
            float movementSpeed = PlayerGroundData.BaseSpeed * MainPlayer.PlayerStateReusable.MovementSpeedModifier;

            if (shouldConsiderSlopes)
            {
                movementSpeed *= MainPlayer.PlayerStateReusable.MovementSlopeSpeedModifier;
            }

            return movementSpeed;
        }

        protected Vector3 GetMovementInputDirection()
        {
            return new Vector3(MainPlayer.PlayerStateReusable.MovementInputWithNormalization.x, 0f,
                MainPlayer.PlayerStateReusable.MovementInputWithNormalization.y);
        }

        private float Rotate(Vector3 direction)
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

            if (directionAngle != MainPlayer.PlayerStateReusable.CurrentTargetRotation.y)
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
            MainPlayer.PlayerStateReusable.CurrentTargetRotation.y = targetAngle;

            MainPlayer.PlayerStateReusable.DampedTargetRotationPassedTime.y = 0f;
        }

        protected void RotateTowardsTargetRotation()
        {
            float currentYAngle = MainPlayer.Rigidbody.rotation.eulerAngles.y;

            if (currentYAngle == MainPlayer.PlayerStateReusable.CurrentTargetRotation.y)
            {
                return;
            }

            float smoothedYAngle = Mathf.SmoothDampAngle(currentYAngle, MainPlayer.PlayerStateReusable.CurrentTargetRotation.y,
                ref MainPlayer.PlayerStateReusable.DampedTargetRotationCurrentVelocity.y,
                MainPlayer.PlayerStateReusable.TimeToReachTargetRotation.y
                - MainPlayer.PlayerStateReusable.DampedTargetRotationPassedTime.y);

            MainPlayer.PlayerStateReusable.DampedTargetRotationPassedTime.y += UnityEngine.Time.deltaTime;

            Quaternion targetRotation = Quaternion.Euler(0f, smoothedYAngle, 0f);

            MainPlayer.Rigidbody.MoveRotation(targetRotation);
        }

        protected Vector3 GetTargetRotationDirection(float targetRotationAngle)
        {
            return Quaternion.Euler(0f, targetRotationAngle, 0f) * Vector3.forward;
        }

        protected float GetMovementSpeed(bool shouldConsiderSlopes = true)
        {
            float movementSpeed = PlayerGroundData.BaseSpeed * MainPlayer.PlayerStateReusable.MovementSpeedModifier;

            if (shouldConsiderSlopes)
            {
                movementSpeed *= MainPlayer.PlayerStateReusable.MovementSlopeSpeedModifier;
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

            MainPlayer.Rigidbody.AddForce(-playerHorizontalVelocity * MainPlayer.PlayerStateReusable.MovementDecelerationForce,
                ForceMode.Acceleration);
        }

        protected void DecelerateVertically()
        {
            Vector3 playerVerticalVelocity = GetPlayerVerticalVelocity();

            MainPlayer.Rigidbody.AddForce(-playerVerticalVelocity * MainPlayer.PlayerStateReusable.MovementDecelerationForce,
                ForceMode.Acceleration);
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