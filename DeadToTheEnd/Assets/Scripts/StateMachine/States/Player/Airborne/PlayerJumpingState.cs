using Data.States;
using StateMachine.Player;
using UnityEngine;

namespace StateMachine.States.Player.Airborne
{
    public class PlayerJumpingState : PlayerAirborneState
    {
        private bool _shouldKeepRotating;
        private bool _canStartFalling;
        private PlayerJumpData _playerJumpData;
        public PlayerJumpingState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
        {
            _playerJumpData = AirborneData.PlayerJumpData;
        }

        public override void Enter()
        {
            base.Enter();
            PlayerMovementStateMachine.ReusableData.MovementSpeedModifier = 0f;
            PlayerMovementStateMachine.ReusableData.RotationData = _playerJumpData.RotationData;
            PlayerMovementStateMachine.ReusableData.MovementDecelerationForce = _playerJumpData.DecelerationForce;

            _shouldKeepRotating = PlayerMovementStateMachine.ReusableData.MovementInput != Vector2.zero;
            
            Jump();
        }

        public override void Exit()
        {
            base.Exit();
            _canStartFalling = false;
            SetBaseRotationData();
        }

        public override void Update()
        {
            base.Update();

            if (!_canStartFalling && IsMovingUp(0f))
            {
                _canStartFalling = true;
            }
            
            if(!_canStartFalling || GetPlayerVerticalVelocity.y >0)
                return;

            PlayerMovementStateMachine.ChangeState(PlayerMovementStateMachine.PlayerFallingState);
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();

            if (_shouldKeepRotating)
            {
                RotateTowardsTargetRotation();
            }

            if (IsMovingUp())
            {
                DecelerateVertically();
            }
        }

        protected override void ResetSprintState()
        {
        }

        private void Jump()
        {
            Vector3 jumpForce = PlayerMovementStateMachine.ReusableData.CurrentJumpForce;
            Vector3 jumpDirection = PlayerMovementStateMachine.Player.transform.forward;

            if (_shouldKeepRotating)
            {
                jumpDirection =
                    GetTargetRotationDirection(PlayerMovementStateMachine.ReusableData.CurrentTargetRotation.y);
            }

            jumpForce.x *= jumpDirection.x;
            jumpForce.z *= jumpDirection.z;

            Vector3 capsuleColliderCenterInWorldSpace = PlayerMovementStateMachine.Player.ColliderUtility
                .CapsuleColliderData.Collider.bounds.center;

            Ray downwardsRayFromCapsuleCenter = new Ray(capsuleColliderCenterInWorldSpace, Vector3.down);
            if (Physics.Raycast(downwardsRayFromCapsuleCenter, out RaycastHit raycastHit, 
                    _playerJumpData.JumpToGroundRayDistance, PlayerMovementStateMachine.Player.PlayerLayerData.GroundLayer))
            {
                float groundAngle = Vector3.Angle(raycastHit.normal, -downwardsRayFromCapsuleCenter.direction);
                if (IsMovingUp())
                {
                    float forceModifier = _playerJumpData.JumpForceModifierOnSlopeUp.Evaluate(groundAngle);

                    jumpForce.x *= forceModifier;
                    jumpForce.z *= forceModifier;
                }

                if (IsMovingDown())
                {
                    float forceModifier = _playerJumpData.JumpForceModifierOnSlopeDown.Evaluate(groundAngle);

                    jumpForce.y *= forceModifier;
                }
            }
            
            ResetVelocity();
            
            PlayerMovementStateMachine.Player.Rigidbody.AddForce(jumpForce, ForceMode.VelocityChange);
        }
    }
}