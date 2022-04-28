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
        protected PlayerAirborneData AirborneData;
        public PlayerMovementState(PlayerMovementStateMachine playerMovementStateMachine)
        {
            PlayerMovementStateMachine = playerMovementStateMachine;

            MovementData = PlayerMovementStateMachine.Player.PlayerData.GroundData;
            AirborneData = PlayerMovementStateMachine.Player.PlayerData.AirborneData;
            
            Init();
        }

        private void Init()
        {
            SetBaseRotationData();
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

        public virtual void OnAnimationEnterEvent()
        {
            
        }

        public virtual void OnAnimationExitEvent()
        {
            
        }

        public virtual void OnAnimationTransitionEvent()
        {
            
        }

        public virtual void OnTriggerEnter(Collider collider)
        {
            if (PlayerMovementStateMachine.Player.PlayerLayerData.IsGroundLayer(collider.gameObject.layer))
            {
                OnContactWithGround(collider);
            }
        }

        public virtual void OnTriggerExit(Collider collider)
        {
            if (PlayerMovementStateMachine.Player.PlayerLayerData.IsGroundLayer(collider.gameObject.layer))
            {
                OnContactWithGroundExited(collider);
                return;
            }
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

            if (directionAngle != PlayerMovementStateMachine.ReusableData.CurrentTargetRotation.y)
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
        private float AddCameraRotationToAngle(float directionAngle)
        {
            directionAngle += PlayerMovementStateMachine.Player.MainCamera.eulerAngles.y;
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

        protected void RotateTowardsTargetRotation()
        {
            float currentYAngle = PlayerMovementStateMachine.Player.Rigidbody.rotation.eulerAngles.y;
            
            if(currentYAngle == PlayerMovementStateMachine.ReusableData.CurrentTargetRotation.y)
                return;

            float smoothedYAngle = Mathf.SmoothDampAngle(currentYAngle, PlayerMovementStateMachine.ReusableData.CurrentTargetRotation.y,
                ref PlayerMovementStateMachine.ReusableData.DampedTargetRotationCurrentVelocity.y,
                PlayerMovementStateMachine.ReusableData.TimeToReachTargetRotation.y - PlayerMovementStateMachine.ReusableData.DampedTargetRotationPassedTime.y);

            PlayerMovementStateMachine.ReusableData.DampedTargetRotationPassedTime.y += Time.deltaTime;
            var targetRotation = Quaternion.Euler(0f, smoothedYAngle, 0f);
            PlayerMovementStateMachine.Player.Rigidbody.MoveRotation(targetRotation);
        }
        protected void SetBaseRotationData()
        {
            PlayerMovementStateMachine.ReusableData.RotationData = MovementData.BaseRotationData;
            PlayerMovementStateMachine.ReusableData.TimeToReachTargetRotation =
                PlayerMovementStateMachine.ReusableData.RotationData.TargetRotationReachTime;
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
            
            PlayerMovementStateMachine.Player.Rigidbody.AddForce(targetRotationDirection * movementSpeed - currentPlayerHorizontalVelocity, ForceMode.VelocityChange);
        }

        protected Vector3 GetTargetRotationDirection(float targetRotationAngle) => Quaternion.Euler(0f, targetRotationAngle, 0f) * Vector3.forward;

        protected bool IsMovingUp(float minVelocity = .1f) => GetPlayerVerticalVelocity.y > minVelocity;
        protected bool IsMovingDown(float minVelocity = .1f) => GetPlayerVerticalVelocity.y < -minVelocity;
        protected Vector3 GetPlayerHorizontalVelocity()
        {
            var playerHorizontalVelocity = PlayerMovementStateMachine.Player.Rigidbody.velocity;
            playerHorizontalVelocity.y = 0f;

            return playerHorizontalVelocity;
        }

        protected float GetMovementSpeed() => PlayerMovementStateMachine.ReusableData.MovementSpeedModifier
                                              * MovementData.BaseSpeed
                                              * PlayerMovementStateMachine.ReusableData.MovementSlopeSpeedModifier;

        protected void DecelerateHorizontally()
        {
            Vector3 playerHorizontalVelocity = GetPlayerHorizontalVelocity();
            
            PlayerMovementStateMachine.Player.Rigidbody
                .AddForce(-playerHorizontalVelocity * PlayerMovementStateMachine.ReusableData.MovementDecelerationForce, 
                                                                        ForceMode.Acceleration);
        }
        protected void DecelerateVertically()
        {
            Vector3 playerVerticalVelocity = GetPlayerVerticalVelocity;
            
            PlayerMovementStateMachine.Player.Rigidbody
                .AddForce(-playerVerticalVelocity * PlayerMovementStateMachine.ReusableData.MovementDecelerationForce, 
                                                                        ForceMode.Acceleration);
        }

        protected bool IsMovingHorizontally(float minimumMagnitude = .1f)
        {
            Vector3 playerHorizontalVelocity = GetPlayerHorizontalVelocity();
            Vector2 playerHorizontalMovement = new Vector2(playerHorizontalVelocity.x, playerHorizontalVelocity.z);
            
            return playerHorizontalMovement.magnitude > minimumMagnitude;
        }
        protected Vector3 GetMovementInputDirection() => new Vector3
            (PlayerMovementStateMachine.ReusableData.MovementInput.x, 0f, PlayerMovementStateMachine.ReusableData.MovementInput.y);

        private void ReadMovementInput() =>
            PlayerMovementStateMachine.ReusableData.MovementInput =
                PlayerMovementStateMachine.Player
                    .InputAction.PlayerActions.Movement.ReadValue<Vector2>();

        protected Vector3 GetPlayerVerticalVelocity =>
            new Vector3(0f, PlayerMovementStateMachine.Player.Rigidbody.velocity.y, 0f);

        protected void ResetVelocity() => PlayerMovementStateMachine.Player.Rigidbody.velocity = Vector3.zero;

        protected void ResetVerticalVelocity() => PlayerMovementStateMachine.Player.Rigidbody.velocity = GetPlayerHorizontalVelocity();
        protected virtual void AddInputActionsCallbacks() => PlayerMovementStateMachine.Player.InputAction.PlayerActions.Walk.started += OnWalkStarted;

        protected virtual void RemoveInputActionsCallbacks() => PlayerMovementStateMachine.Player.InputAction.PlayerActions.Walk.started -= OnWalkStarted;

        protected virtual void OnWalkStarted(InputAction.CallbackContext obj) => 
            PlayerMovementStateMachine.ReusableData.ShouldWalk = !PlayerMovementStateMachine.ReusableData.ShouldWalk;
        
        protected virtual void OnContactWithGround(Collider collider)
        {
        }
        
        protected virtual void OnContactWithGroundExited(Collider collider)
        {
            
        }
    }
}