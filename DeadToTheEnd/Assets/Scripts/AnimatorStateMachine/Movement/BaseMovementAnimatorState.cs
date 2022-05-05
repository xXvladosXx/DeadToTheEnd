using System.Collections.Generic;
using Data.Animations;
using Data.Camera;
using Data.Collider;
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
        protected MainPlayer Player;
        private SlopeData SlopeData;
       
        protected PlayerGroundData GroundedData;
        protected PlayerAirborneData AirborneData;

        protected AnimationData PlayerAnimationData;
        private float _time;

        public override void OnEnter(DefaultNamespace.AnimatorStateMachine characterState, Animator animator, AnimatorStateInfo stateInfo,
            PlayerInput playerInputActions)
        {
            Player = animator.GetComponent<MainPlayer>();
            GroundedData = Player.PlayerData.GroundData;
            AirborneData = Player.PlayerData.AirborneData;
            PlayerAnimationData = Player.AnimationData;
            Player.Animator.applyRootMotion = false;
            _time = 0;
            AddInputCallbacks();

            InitializeData();
            ResetVelocity();
        }

        public override void OnUpdate(DefaultNamespace.AnimatorStateMachine characterState, Animator animator, AnimatorStateInfo stateInfo,
            PlayerInput playerInputActions)
        {
            ReadMovementInput();
            Move();
        }

        public override void OnExit(DefaultNamespace.AnimatorStateMachine characterState, Animator animator, AnimatorStateInfo stateInfo,
            PlayerInput playerInputActions)
        {
            RemoveInputCallbacks();
        }

        protected void SetBaseCameraRecenteringData()
        {
            Player.ReusableData.SideCameraRecenteringDatas = GroundedData.SideCameraRecenteringDatas;
            Player.ReusableData.BackCameraRecenteringDatas = GroundedData.BackCameraRecenteringDatas;
        }

        protected void SetBaseRotationData()
        {
            Player.ReusableData.RotationData = GroundedData.BaseRotationData;

            Player.ReusableData.TimeToReachTargetRotation = Player.ReusableData.RotationData.TargetRotationReachTime;
        }
        protected virtual void AddInputCallbacks()
        {
            Player.InputAction.PlayerActions.Dash.performed += OnDashStarted;
            Player.InputAction.PlayerActions.Movement.performed += OnMovementPerformed;
            Player.InputAction.PlayerActions.Movement.canceled += OnMovementCanceled;
            Player.InputAction.PlayerActions.Attack1.performed += OnAttackCalled;
        }

        private void OnAttackCalled(InputAction.CallbackContext obj)
        {
            Player.Animator.SetBool(PlayerAnimationData.Attack1ParameterHash, true);
        }

        protected virtual void OnMovementCanceled(InputAction.CallbackContext obj)
        {
            
        }

        private void OnMovementPerformed(InputAction.CallbackContext obj)
        {
            Player.Animator.SetBool(Player.AnimationData.MovingParameterHash, true);
        }

        private void OnDashStarted(InputAction.CallbackContext obj)
        {
            Player.Animator.SetBool(Player.AnimationData.SprintParameterHash, true);
        }
        protected virtual void RemoveInputCallbacks()
        {
            Player.InputAction.PlayerActions.Dash.performed -= OnDashStarted;
            Player.InputAction.PlayerActions.Movement.performed -= OnMovementPerformed;
            Player.InputAction.PlayerActions.Movement.canceled -= OnMovementCanceled;
            Player.InputAction.PlayerActions.Attack1.performed -= OnAttackCalled;
        }
        
        private void InitializeData()
        {
            SetBaseCameraRecenteringData();

            SetBaseRotationData();
        }
        private void ReadMovementInput()
        {
            Player.ReusableData.MovementInput = Player.InputAction.PlayerActions.Movement.ReadValue<Vector2>();
        }

        private void Move()
        {
            if (Player.ReusableData.MovementInput == Vector2.zero ||
                Player.ReusableData.MovementSpeedModifier == 0f)
            {            
                Player.Animator.SetFloat(PlayerAnimationData.SpeedParameterHash, 0f, 0.05f, Time.deltaTime);
                Player.Animator.SetBool(PlayerAnimationData.MovingParameterHash, false);

                return;
            }
          
            var movementDirection = GetMovementInputDirection();
            var targetRotationAngle = Rotate(movementDirection);
            var targetRotationDirection = GetTargetRotationDirection(targetRotationAngle);
            var movementSpeed = GetSmoothMovementSpeed();

            var currentPlayerHorizontalVelocity = GetPlayerHorizontalVelocity();
            
            Player.Animator.SetBool(PlayerAnimationData.MovingParameterHash, true);
            Player.Animator.SetBool(PlayerAnimationData.WasMovingParameterHash, true);
            Player.Animator.SetFloat(PlayerAnimationData.SpeedParameterHash, Player.ReusableData.MovementSpeedModifier, 0.3f, Time.deltaTime);
            
            Player.Rigidbody.AddForce(targetRotationDirection * movementSpeed - currentPlayerHorizontalVelocity, ForceMode.VelocityChange);
        }

        private float GetSmoothMovementSpeed()
        {
            _time += Time.deltaTime * GroundedData.SmoothSpeedModifier;
            float movementSpeed = GetMaxMovementSpeed();
            if (Player.ReusableData.IsMovingAfterStop)
            {
                movementSpeed = Mathf.Lerp(0, GetMaxMovementSpeed(), _time);
            }

            if (GetMaxMovementSpeed() == movementSpeed)
            {
                Player.ReusableData.IsMovingAfterStop = false;
            }

            return movementSpeed;
        }

        protected Vector3 GetMovementInputDirection()
        {
            return new Vector3(Player.ReusableData.MovementInput.x, 0f, Player.ReusableData.MovementInput.y);
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

            if (directionAngle != Player.ReusableData.CurrentTargetRotation.y)
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
            angle += Player.MainCamera.eulerAngles.y;

            if (angle > 360f)
            {
                angle -= 360f;
            }

            return angle;
        }

        private void UpdateTargetRotationData(float targetAngle)
        {
            Player.ReusableData.CurrentTargetRotation.y = targetAngle;

            Player.ReusableData.DampedTargetRotationPassedTime.y = 0f;
        }

        protected void RotateTowardsTargetRotation()
        {
            float currentYAngle = Player.Rigidbody.rotation.eulerAngles.y;

            if (currentYAngle == Player.ReusableData.CurrentTargetRotation.y)
            {
                return;
            }

            float smoothedYAngle = Mathf.SmoothDampAngle(currentYAngle, Player.ReusableData.CurrentTargetRotation.y, ref Player.ReusableData.DampedTargetRotationCurrentVelocity.y, Player.ReusableData.TimeToReachTargetRotation.y - Player.ReusableData.DampedTargetRotationPassedTime.y);

            Player.ReusableData.DampedTargetRotationPassedTime.y += Time.deltaTime;

            Quaternion targetRotation = Quaternion.Euler(0f, smoothedYAngle, 0f);

            Player.Rigidbody.MoveRotation(targetRotation);
        }

        protected Vector3 GetTargetRotationDirection(float targetRotationAngle)
        {
            return Quaternion.Euler(0f, targetRotationAngle, 0f) * Vector3.forward;
        }

        protected float GetMaxMovementSpeed(bool shouldConsiderSlopes = true)
        {
            float movementSpeed = GroundedData.BaseSpeed * Player.ReusableData.MovementSpeedModifier;

            if (shouldConsiderSlopes)
            {
                movementSpeed *= Player.ReusableData.MovementSlopeSpeedModifier;
            }

            return movementSpeed;
        }
        
       
        protected Vector3 GetPlayerHorizontalVelocity()
        {
            Vector3 playerHorizontalVelocity = Player.Rigidbody.velocity;

            playerHorizontalVelocity.y = 0f;

            return playerHorizontalVelocity;
        }

        protected Vector3 GetPlayerVerticalVelocity()
        {
            return new Vector3(0f, Player.Rigidbody.velocity.y, 0f);
        }

        protected virtual void OnContactWithGround(Collider collider)
        {
        }

        protected virtual void OnContactWithGroundExited(Collider collider)
        {
        }

        protected void UpdateCameraRecenteringState(Vector2 movementInput)
        {
            if (movementInput == Vector2.zero)
            {
                return;
            }

            if (movementInput == Vector2.up)
            {
                DisableCameraRecentering();

                return;
            }

            float cameraVerticalAngle = Player.MainCamera.eulerAngles.x;

            if (cameraVerticalAngle >= 270f)
            {
                cameraVerticalAngle -= 360f;
            }

            cameraVerticalAngle = Mathf.Abs(cameraVerticalAngle);

            if (movementInput == Vector2.down)
            {
                SetCameraRecenteringState(cameraVerticalAngle, Player.ReusableData.BackCameraRecenteringDatas);

                return;
            }

            SetCameraRecenteringState(cameraVerticalAngle, Player.ReusableData.SideCameraRecenteringDatas);
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

            Player.CameraUtility.EnableRecentering(waitTime, recenteringTime, GroundedData.BaseSpeed, movementSpeed);
        }

        protected void DisableCameraRecentering()
        {
            Player.CameraUtility.DisableRecentering();
        }

        protected void ResetVelocity()
        {
            Player.Rigidbody.velocity = Vector3.zero;
        }

        protected void ResetVerticalVelocity()
        {
            Vector3 playerHorizontalVelocity = GetPlayerHorizontalVelocity();

            Player.Rigidbody.velocity = playerHorizontalVelocity;
        }

        protected void DecelerateHorizontally()
        {
            Vector3 playerHorizontalVelocity = GetPlayerHorizontalVelocity();

            Player.Rigidbody.AddForce(-playerHorizontalVelocity * Player.ReusableData.MovementDecelerationForce, ForceMode.Acceleration);
        }

        protected void DecelerateVertically()
        {
            Vector3 playerVerticalVelocity = GetPlayerVerticalVelocity();

            Player.Rigidbody.AddForce(-playerVerticalVelocity * Player.ReusableData.MovementDecelerationForce, ForceMode.Acceleration);
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