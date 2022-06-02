using UnityEngine;
using UnityEngine.InputSystem;

namespace StateMachine.Player.States.Movement.Grounded
{
    public abstract class PlayerGroundedState : PlayerMovementState
    {
        public PlayerGroundedState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
        {
        }
        
         public override void Enter()
        {
            base.Enter();

            StartAnimation(PlayerAnimationData.GroundedParameterHash);

            UpdateShouldSprintState();
        }

        public override void Exit()
        {
            base.Exit();

            StopAnimation(PlayerAnimationData.GroundedParameterHash);
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();

            Float();
        }

        private void UpdateShouldSprintState()
        {
            if (!MainPlayer.PlayerStateReusable.ShouldSprint)
            {
                return;
            }

            if (MainPlayer.PlayerStateReusable.MovementInputWithNormalization != Vector2.zero)
            {
                return;
            }

            MainPlayer.PlayerStateReusable.ShouldSprint = false;
        }

        protected void Float()
        {
            Vector3 capsuleColliderCenterInWorld = MainPlayer.ColliderUtility
                .CapsuleColliderData.Collider.bounds.center;

            Ray downwardsRayFromCapsuleCenter = new Ray(capsuleColliderCenterInWorld, Vector3.down);
            if (Physics.Raycast(downwardsRayFromCapsuleCenter, out RaycastHit raycastHit, MainPlayer.ColliderUtility.SlopeData.FloatRayDistance, 
                    MainPlayer.PlayerLayerData.GroundLayer))
            {
                float groundAngle = Vector3.Angle(raycastHit.normal, -downwardsRayFromCapsuleCenter.direction);
                float slopeSpeedModifier = SetSlopeSpeedModifierOnAngle(groundAngle);
                
                if(slopeSpeedModifier == 0f)
                    return;
                
                float distanceToFloatingPoint = MainPlayer.ColliderUtility
                    .CapsuleColliderData.ColliderCenterInLocalSpace.y * MainPlayer.transform.localScale.y - raycastHit.distance;
                
                if(distanceToFloatingPoint == 0f)
                    return;

                float amountToLift = distanceToFloatingPoint * MainPlayer.ColliderUtility.SlopeData.StepReachForce - GetPlayerVerticalVelocity().y;
                var liftForce = new Vector3(0f, amountToLift, 0f);
                MainPlayer.Rigidbody.AddForce(liftForce, ForceMode.VelocityChange);
            }
        }

        private float SetSlopeSpeedModifierOnAngle(float angle)
        {
            float slopeSpeedModifier = PlayerGroundData.SlopeSpeedAngles.Evaluate(angle);

            if (MainPlayer.PlayerStateReusable.MovementSlopeSpeedModifier != slopeSpeedModifier)
            {
                MainPlayer.PlayerStateReusable.MovementSlopeSpeedModifier = slopeSpeedModifier;
            }

            return slopeSpeedModifier;
        }

        protected override void AddInputCallbacks()
        {
            base.AddInputCallbacks();

            MainPlayer.InputAction.PlayerActions.Dash.started += OnDashStarted;
        }

        protected override void RemoveInputCallbacks()
        {
            base.RemoveInputCallbacks();

            MainPlayer.InputAction.PlayerActions.Dash.started -= OnDashStarted;
        }

        protected virtual void OnDashStarted(InputAction.CallbackContext context)
        {
            PlayerStateMachine.ChangeState(PlayerStateMachine.PlayerDashState);
        }


        protected virtual void OnMove()
        {
            if (MainPlayer.PlayerStateReusable.ShouldSprint)
            {
                PlayerStateMachine.ChangeState(PlayerStateMachine.PlayerSprintingState);

                return;
            }

            if (MainPlayer.PlayerStateReusable.ShouldWalk)
            {
                PlayerStateMachine.ChangeState(PlayerStateMachine.PlayerWalkingState);

                return;
            }

            PlayerStateMachine.ChangeState(PlayerStateMachine.PlayerRunningState);
        }

        protected override void OnMovementPerformed(InputAction.CallbackContext context)
        {
            base.OnMovementPerformed(context);

            UpdateTargetRotation(GetMovementInputDirection());
        }
    }
}