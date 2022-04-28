using Data.Collider;
using Data.States;
using StateMachine.Player;
using UnityEngine;
using Utilities;

namespace DefaultNamespace
{
    [CreateAssetMenu(menuName = "AnimatorState/MoveState")]
    public class MovementAnimatorState : BaseAnimatorState
    {
        private MainPlayer _mainPlayer;
        private SlopeData SlopeData;

        protected PlayerGroundData MovementData;
        protected PlayerAirborneData AirborneData;
        public override void OnEnter(AnimatorStateMachine characterState, Animator animator, AnimatorStateInfo stateInfo,
            PlayerInput playerInputActions)
        {
            _mainPlayer = animator.GetComponent<MainPlayer>();
            MovementData = _mainPlayer.PlayerData.GroundData;
            AirborneData = _mainPlayer.PlayerData.AirborneData;
            SlopeData = _mainPlayer.ColliderUtility.SlopeData;
            SetBaseRotationData();
        }

        public override void OnUpdate(AnimatorStateMachine characterState, Animator animator, AnimatorStateInfo stateInfo,
            PlayerInput playerInputActions)
        {
            ReadMovementInput();
            Move();
            Float();
        }
        private void Float()
        {
            Vector3 capsuleColliderCenterInWorld = _mainPlayer.ColliderUtility
                .CapsuleColliderData.Collider.bounds.center;

            Ray downwardsRayFromCapsuleCenter = new Ray(capsuleColliderCenterInWorld, Vector3.down);
            if (Physics.Raycast(downwardsRayFromCapsuleCenter, out RaycastHit raycastHit, SlopeData.FloatRayDistance, 
                    _mainPlayer.PlayerLayerData.GroundLayer))
            {
                float groundAngle = Vector3.Angle(raycastHit.normal, -downwardsRayFromCapsuleCenter.direction);
                float slopeSpeedModifier = SetSlopeSpeedModifierOnAngle(groundAngle);
                
                if(slopeSpeedModifier == 0f)
                    return;
                
                float distanceToFloatingPoint = _mainPlayer.ColliderUtility
                    .CapsuleColliderData.ColliderCenterInLocalSpace.y * _mainPlayer.transform.localScale.y - raycastHit.distance;
                
                if(distanceToFloatingPoint == 0f)
                    return;

                float amountToLift = distanceToFloatingPoint * SlopeData.StepReachForce - GetPlayerVerticalVelocity.y;
                var liftForce = new Vector3(0f, amountToLift, 0f);
                _mainPlayer.Rigidbody.AddForce(liftForce, ForceMode.VelocityChange);
            }
        }
        
        private float SetSlopeSpeedModifierOnAngle(float groundAngle)
        {
            float slopeSpeedModifier = MovementData.SlopeSpeedAngles.Evaluate(groundAngle);
            _mainPlayer.ReusableData.MovementSlopeSpeedModifier = slopeSpeedModifier;
            return slopeSpeedModifier;
        }
        public override void OnExit(AnimatorStateMachine characterState, Animator animator, AnimatorStateInfo stateInfo,
            PlayerInput playerInputActions)
        {
            
        }
        
         protected float UpdateTargetRotation(Vector3 direction, bool shouldConsiderCameraRotation = true)
        {
            float directionAngle = GetDirectionAngle(direction);

            if (shouldConsiderCameraRotation)
            {
                directionAngle = AddCameraRotationToAngle(directionAngle);
            }

            if (directionAngle != _mainPlayer.ReusableData.CurrentTargetRotation.y)
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
            directionAngle += _mainPlayer.MainCamera.eulerAngles.y;
            if (directionAngle > 360f)
            {
                directionAngle -= 360f;
            }

            return directionAngle;
        }

        private void UpdateTargetRotationData(float directionAngle)
        {
            _mainPlayer.ReusableData.CurrentTargetRotation.y = directionAngle;
            _mainPlayer.ReusableData.DampedTargetRotationPassedTime.y = 0f;
        }

        protected void RotateTowardsTargetRotation()
        {
            float currentYAngle = _mainPlayer.Rigidbody.rotation.eulerAngles.y;
            
            if(currentYAngle == _mainPlayer.ReusableData.CurrentTargetRotation.y)
                return;

            float smoothedYAngle = Mathf.SmoothDampAngle(currentYAngle, _mainPlayer.ReusableData.CurrentTargetRotation.y,
                ref _mainPlayer.ReusableData.DampedTargetRotationCurrentVelocity.y,
                _mainPlayer.ReusableData.TimeToReachTargetRotation.y - _mainPlayer.ReusableData.DampedTargetRotationPassedTime.y);

            _mainPlayer.ReusableData.DampedTargetRotationPassedTime.y += Time.deltaTime*20;
            var targetRotation = Quaternion.Euler(0f, smoothedYAngle, 0f);
            _mainPlayer.Rigidbody.MoveRotation(targetRotation);
        }
        protected void SetBaseRotationData()
        {
            _mainPlayer.ReusableData.RotationData = MovementData.BaseRotationData;
            _mainPlayer.ReusableData.TimeToReachTargetRotation =
                _mainPlayer.ReusableData.RotationData.TargetRotationReachTime;
        }
        private void Move()
        {
            if (_mainPlayer.ReusableData.MovementInput == Vector2.zero || _mainPlayer.ReusableData.MovementSpeedModifier == 0f)
                return;

            var movementDirection = GetMovementInputDirection();
            var targetRotationAngle = Rotate(movementDirection);
            var targetRotationDirection = GetTargetRotationDirection(targetRotationAngle);
            var movementSpeed = GetMovementSpeed();
            var currentPlayerHorizontalVelocity = GetPlayerHorizontalVelocity();
            
            _mainPlayer.Rigidbody.AddForce(targetRotationDirection * movementSpeed - currentPlayerHorizontalVelocity, ForceMode.VelocityChange);
        }
        private float Rotate(Vector3 direction)
        {
            float directionAngle = UpdateTargetRotation(direction);
            RotateTowardsTargetRotation();
            
            return directionAngle;
        }
        protected Vector3 GetTargetRotationDirection(float targetRotationAngle) => Quaternion.Euler(0f, targetRotationAngle, 0f) * Vector3.forward;

        protected bool IsMovingUp(float minVelocity = .1f) => GetPlayerVerticalVelocity.y > minVelocity;
        protected bool IsMovingDown(float minVelocity = .1f) => GetPlayerVerticalVelocity.y < -minVelocity;
        protected Vector3 GetPlayerHorizontalVelocity()
        {
            var playerHorizontalVelocity = _mainPlayer.Rigidbody.velocity;
            playerHorizontalVelocity.y = 0f;

            return playerHorizontalVelocity;
        }

        protected float GetMovementSpeed() => _mainPlayer.ReusableData.MovementSpeedModifier
                                              * MovementData.BaseSpeed
                                              * _mainPlayer.ReusableData.MovementSlopeSpeedModifier;

        protected void DecelerateHorizontally()
        {
            Vector3 playerHorizontalVelocity = GetPlayerHorizontalVelocity();
            
            _mainPlayer.Rigidbody
                .AddForce(-playerHorizontalVelocity * _mainPlayer.ReusableData.MovementDecelerationForce, 
                                                                        ForceMode.Acceleration);
        }
        protected void DecelerateVertically()
        {
            Vector3 playerVerticalVelocity = GetPlayerVerticalVelocity;
            
            _mainPlayer.Rigidbody
                .AddForce(-playerVerticalVelocity * _mainPlayer.ReusableData.MovementDecelerationForce, 
                                                                        ForceMode.Acceleration);
        }

        protected bool IsMovingHorizontally(float minimumMagnitude = .1f)
        {
            Vector3 playerHorizontalVelocity = GetPlayerHorizontalVelocity();
            Vector2 playerHorizontalMovement = new Vector2(playerHorizontalVelocity.x, playerHorizontalVelocity.z);
            
            return playerHorizontalMovement.magnitude > minimumMagnitude;
        }
        protected Vector3 GetMovementInputDirection() => new Vector3
            (_mainPlayer.ReusableData.MovementInput.x, 0f, _mainPlayer.ReusableData.MovementInput.y);

        private void ReadMovementInput() =>
            _mainPlayer.ReusableData.MovementInput =
                _mainPlayer
                    .InputAction.PlayerActions.Movement.ReadValue<Vector2>();

        protected Vector3 GetPlayerVerticalVelocity =>
            new Vector3(0f, _mainPlayer.Rigidbody.velocity.y, 0f);

        protected void ResetVelocity() => _mainPlayer.Rigidbody.velocity = Vector3.zero;

        protected void ResetVerticalVelocity() => _mainPlayer.Rigidbody.velocity = GetPlayerHorizontalVelocity();
    }
}