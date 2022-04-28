using Data.Collider;
using StateMachine.Player;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

namespace StateMachine.States.Player
{
    public class PlayerGroundedState : PlayerMovementState
    {
        private SlopeData SlopeData;
        public PlayerGroundedState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
        {
            SlopeData = PlayerMovementStateMachine.Player.ColliderUtility.SlopeData;
        }

        public override void Enter()
        {
            base.Enter();

            UpdateShouldSprintState();
        }
        
        public override void FixedUpdate()
        {
            base.FixedUpdate();
            Float();
        }

        private void Float()
        {
            Vector3 capsuleColliderCenterInWorld = PlayerMovementStateMachine.Player.ColliderUtility
                .CapsuleColliderData.Collider.bounds.center;

            Ray downwardsRayFromCapsuleCenter = new Ray(capsuleColliderCenterInWorld, Vector3.down);
            if (Physics.Raycast(downwardsRayFromCapsuleCenter, out RaycastHit raycastHit, SlopeData.FloatRayDistance, 
                    PlayerMovementStateMachine.Player.PlayerLayerData.GroundLayer))
            {
                float groundAngle = Vector3.Angle(raycastHit.normal, -downwardsRayFromCapsuleCenter.direction);
                float slopeSpeedModifier = SetSlopeSpeedModifierOnAngle(groundAngle);
                
                if(slopeSpeedModifier == 0f)
                    return;
                
                float distanceToFloatingPoint = PlayerMovementStateMachine.Player.ColliderUtility
                    .CapsuleColliderData.ColliderCenterInLocalSpace.y * PlayerMovementStateMachine.Player.transform.localScale.y - raycastHit.distance;
                
                if(distanceToFloatingPoint == 0f)
                    return;

                float amountToLift = distanceToFloatingPoint * SlopeData.StepReachForce - GetPlayerVerticalVelocity.y;
                var liftForce = new Vector3(0f, amountToLift, 0f);
                PlayerMovementStateMachine.Player.Rigidbody.AddForce(liftForce, ForceMode.VelocityChange);
            }
        }

        private float SetSlopeSpeedModifierOnAngle(float groundAngle)
        {
            float slopeSpeedModifier = MovementData.SlopeSpeedAngles.Evaluate(groundAngle);
            PlayerMovementStateMachine.ReusableData.MovementSlopeSpeedModifier = slopeSpeedModifier;
            return slopeSpeedModifier;
        }
        private void UpdateShouldSprintState()
        {
            if(!PlayerMovementStateMachine.ReusableData.ShouldSprint)
                return;

            if (PlayerMovementStateMachine.ReusableData.MovementInput != Vector2.zero)
                return;

            PlayerMovementStateMachine.ReusableData.ShouldSprint = false;
        }
        protected virtual void OnMove()
        {
            if (PlayerMovementStateMachine.ReusableData.ShouldSprint)
            {
                PlayerMovementStateMachine.ChangeState(PlayerMovementStateMachine.SprintingState);
                return;
            }
            
            if (PlayerMovementStateMachine.ReusableData.ShouldWalk)
            {
                PlayerMovementStateMachine.ChangeState(PlayerMovementStateMachine.WalkingState);
            }
            
            PlayerMovementStateMachine.ChangeState(PlayerMovementStateMachine.RunningState);
        }
        protected override void AddInputActionsCallbacks()
        {
            base.AddInputActionsCallbacks();
            PlayerMovementStateMachine.Player.InputAction.PlayerActions.Movement.canceled += OnMovementCanceled;
            PlayerMovementStateMachine.Player.InputAction.PlayerActions.Dash.started += OnDashStarted;
            PlayerMovementStateMachine.Player.InputAction.PlayerActions.Jump.started += OnJumpStarted;
        }
        
        protected override void RemoveInputActionsCallbacks()
        {
            base.RemoveInputActionsCallbacks();
            PlayerMovementStateMachine.Player.InputAction.PlayerActions.Movement.canceled -= OnMovementCanceled;
            PlayerMovementStateMachine.Player.InputAction.PlayerActions.Dash.started -= OnDashStarted;
            PlayerMovementStateMachine.Player.InputAction.PlayerActions.Jump.started -= OnJumpStarted;
        }
        
        protected virtual void OnDashStarted(InputAction.CallbackContext obj)
        {
            PlayerMovementStateMachine.ChangeState(PlayerMovementStateMachine.DashState);
        }

        protected virtual void OnMovementCanceled(InputAction.CallbackContext obj)
        {
            PlayerMovementStateMachine.ChangeState(PlayerMovementStateMachine.IdleState);
        }
        
        protected virtual void OnJumpStarted(InputAction.CallbackContext obj)
        {
            PlayerMovementStateMachine.ChangeState(PlayerMovementStateMachine.PlayerJumpingState);
        }

        protected override void OnContactWithGroundExited(Collider collider)
        {
            base.OnContactWithGroundExited(collider);
            if (IsThereGroundUnderneath())
                return;

            Vector3 capsuleColliderCenterInWorld = PlayerMovementStateMachine.Player.ColliderUtility
                .CapsuleColliderData.Collider.bounds.center;
            Ray downRay = new Ray(capsuleColliderCenterInWorld - PlayerMovementStateMachine.Player.ColliderUtility.CapsuleColliderData.ColliderVerticalExtents, Vector3.down);

            if (!Physics.Raycast(downRay, out RaycastHit _, MovementData.GroundToFallRayDistance, 
                    PlayerMovementStateMachine.Player.PlayerLayerData.GroundLayer))
            {
                OnFall();
            }
        }

        private bool IsThereGroundUnderneath()
        {
            var groundCheckCollider = PlayerMovementStateMachine.Player
                .ColliderUtility.TriggerColliderData.GroundCheckCollider;
            
            Vector3 groundColliderCenter = groundCheckCollider.bounds.center;

            Collider[] overlappedGroundColliders = Physics.OverlapBox(groundColliderCenter, 
                groundCheckCollider.bounds.extents, groundCheckCollider.transform.rotation, 
                PlayerMovementStateMachine.Player.PlayerLayerData.GroundLayer);

            return overlappedGroundColliders.Length > 0;
        }

        protected virtual void OnFall()
        {
            PlayerMovementStateMachine.ChangeState(PlayerMovementStateMachine.PlayerFallingState);
        }
    }
}