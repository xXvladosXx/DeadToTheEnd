using Data.States;
using StateMachine.Player;
using UnityEngine;
using UnityEngine.InputSystem;

namespace StateMachine.States.Player
{
    public class PlayerMovementState : IState
    {
        protected PlayerMovementStateMachine PlayerMovementStateMachine;
        protected PlayerGroundData MovementData;
        public PlayerMovementState(PlayerMovementStateMachine playerMovementStateMachine)
        {
            PlayerMovementStateMachine = playerMovementStateMachine;

            MovementData = PlayerMovementStateMachine.MainPlayer.PlayerData.GroundData;
            Init();
        }

        private void Init()
        {
            PlayerMovementStateMachine.ReusableData.TimeToReachTargetRotation = MovementData.BaseRotationData.TargetRotationReachTime;
        }

        public virtual void Enter()
        {
            AddInputActionsCallbacks();
        }

        public virtual void Exit()
        {
            RemoveInputActionsCallbacks();
        }

        public virtual void HandleInput()
        {
            ReadMovementInput();
        }

        public virtual void Update()
        {
            
        }

        public virtual void FixedUpdate()
        {
            Move();
        }

        private float Rotate(Vector3 direction)
        {
            float directionAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;

            directionAngle = UpdateTargetRotation(directionAngle);
            RotateTowardsTargetRotation();
            
            return directionAngle;
        }

        private float UpdateTargetRotation(float directionAngle, bool shouldConsiderCameraRotation = true)
        {
            if (directionAngle < 0f)
            {
                directionAngle += 360f;
            }

            if (shouldConsiderCameraRotation)
            {
                directionAngle = AddCameraRotationToAngle(directionAngle);
            }

            if (directionAngle != PlayerMovementStateMachine.ReusableData.CurrentTargetRotation.y)
            {
                UpdateTargetRotationData(directionAngle);
            }

            return directionAngle;
        }

        private float AddCameraRotationToAngle(float directionAngle)
        {
            directionAngle += PlayerMovementStateMachine.MainPlayer.MainCamera.eulerAngles.y;
            if (directionAngle > 360f)
            {
                directionAngle -= 360f;
            }

            return directionAngle;
        }

        private void UpdateTargetRotationData(float directionAngle)
        {
            PlayerMovementStateMachine.ReusableData.CurrentTargetRotation.y = directionAngle;
            PlayerMovementStateMachine.ReusableData.DampedTargetRotationPassedTime.y = 0f;
        }

        private void RotateTowardsTargetRotation()
        {
            float currentYAngle = PlayerMovementStateMachine.MainPlayer.Rigidbody.rotation.eulerAngles.y;
            
            if(currentYAngle == PlayerMovementStateMachine.ReusableData.CurrentTargetRotation.y)
                return;

            float smoothedYAngle = Mathf.SmoothDampAngle(currentYAngle, PlayerMovementStateMachine.ReusableData.CurrentTargetRotation.y,
                ref PlayerMovementStateMachine.ReusableData.DampedTargetRotationCurrentVelocity.y,
                PlayerMovementStateMachine.ReusableData.TimeToReachTargetRotation.y - PlayerMovementStateMachine.ReusableData.DampedTargetRotationPassedTime.y);

            PlayerMovementStateMachine.ReusableData.DampedTargetRotationPassedTime.y += Time.deltaTime;
            var targetRotation = Quaternion.Euler(0f, smoothedYAngle, 0f);
            PlayerMovementStateMachine.MainPlayer.Rigidbody.MoveRotation(targetRotation);
        }

        private void Move()
        {
            if (PlayerMovementStateMachine.ReusableData.MovementInput == Vector2.zero || PlayerMovementStateMachine.ReusableData.MovementSpeedModifier == 0f)
                return;

            var movementDirection = GetMovementInputDirection();
            var targetRotationAngle = Rotate(movementDirection);
            var targetRotationDirection = GetTargetRotationDirection(targetRotationAngle);
            var movementSpeed = GetMovementSpeed();
            var currentPlayerHorizontalVelocity = GetPlayerHorizontalVelocity();
            
            PlayerMovementStateMachine.MainPlayer.Rigidbody.AddForce(targetRotationDirection * movementSpeed - currentPlayerHorizontalVelocity, ForceMode.VelocityChange);
        }

        private Vector3 GetTargetRotationDirection(float targetRotationAngle) => Quaternion.Euler(0f, targetRotationAngle, 0f) * Vector3.forward;

        protected Vector3 GetPlayerHorizontalVelocity()
        {
            var playerHorizontalVelocity = PlayerMovementStateMachine.MainPlayer.Rigidbody.velocity;
            playerHorizontalVelocity.y = 0f;

            return playerHorizontalVelocity;
        }

        protected float GetMovementSpeed() => PlayerMovementStateMachine.ReusableData.MovementSpeedModifier * MovementData.BaseSpeed;

        protected Vector3 GetMovementInputDirection() => new Vector3(PlayerMovementStateMachine.ReusableData.MovementInput.x, 0f, PlayerMovementStateMachine.ReusableData.MovementInput.y);

        private void ReadMovementInput()
        {
            PlayerMovementStateMachine.ReusableData.MovementInput =
                PlayerMovementStateMachine.MainPlayer
                    .InputAction.PlayerActions.Movement.ReadValue<Vector2>();
        }

        protected void ResetVelocity()
        {
            PlayerMovementStateMachine.MainPlayer.Rigidbody.velocity = Vector3.zero;
        }
        
        protected virtual void AddInputActionsCallbacks()
        {
            PlayerMovementStateMachine.MainPlayer.InputAction.PlayerActions.Walk.started += OnWalkStarted;
        }
        protected virtual void RemoveInputActionsCallbacks()
        {
            PlayerMovementStateMachine.MainPlayer.InputAction.PlayerActions.Walk.started -= OnWalkStarted;
        }
        
        protected virtual void OnWalkStarted(InputAction.CallbackContext obj)
        {
            PlayerMovementStateMachine.ReusableData.ShouldWalk = !PlayerMovementStateMachine.ReusableData.ShouldWalk;
        }
    }
}