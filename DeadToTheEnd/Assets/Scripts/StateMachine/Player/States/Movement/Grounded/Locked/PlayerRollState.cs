using UnityEngine;
using UnityEngine.InputSystem;

namespace StateMachine.Player.States.Movement.Grounded
{
    public class PlayerRollState : PlayerGroundedState
    {
        private int _consecutiveDashesUsed;

        public PlayerRollState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
        {
        }
        
         public override void Enter()
        {
            base.Enter();
            MainPlayer.PlayerStateReusable.MovementSpeedModifier = PlayerGroundData.RollData.SpeedModifier;
            
            StartAnimation(PlayerAnimationData.RollParameterHash);

            MainPlayer.PlayerStateReusable.RotationData = PlayerGroundData.DashData.RotationData;
            Roll();
        }

         protected override void OnLockedPerformed(InputAction.CallbackContext obj)
         {
         }

        public override void Exit()
        {
            base.Exit();
            MainPlayer.PlayerStateReusable.StopReading = false;

            StopAnimation(PlayerAnimationData.RollParameterHash);
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();
            RotateTowardsTargetRotation();
        }

        protected override void AddInputCallbacks()
        {
            MainPlayer.InputAction.PlayerActions.Dash.canceled += OnDefenseCanceled;
            MainPlayer.InputAction.PlayerActions.Dash.performed += OnDefensePerformed;
        }
        
        protected override void RemoveInputCallbacks()
        {
            MainPlayer.InputAction.PlayerActions.Dash.canceled -= OnDefenseCanceled;
            MainPlayer.InputAction.PlayerActions.Dash.performed -= OnDefensePerformed;
        }
        
        private void OnDefenseCanceled(InputAction.CallbackContext obj)
        {
            MainPlayer.PlayerStateReusable.ShouldBlock = !MainPlayer.PlayerStateReusable.ShouldBlock;
        }

        private void OnDefensePerformed(InputAction.CallbackContext obj)
        {
            MainPlayer.PlayerStateReusable.ShouldBlock = true;
        }
        
        public override void TriggerOnStateAnimationEnterEvent()
        {
            MainPlayer.PlayerStateReusable.StopReading = true;
        }
        
        public override void TriggerOnStateAnimationHandleEvent()
        {
            if (MainPlayer.PlayerStateReusable.ShouldBlock)
            {
                PlayerStateMachine.ChangeState(PlayerStateMachine.PlayerDefenseState);
                return;
            }
            
            PlayerStateMachine.ChangeState(PlayerStateMachine.PlayerLockedMovementState);
        }

        public override void TriggerOnStateAnimationExitEvent()
        {
            if (MainPlayer.PlayerStateReusable.ShouldBlock)
            {
                PlayerStateMachine.ChangeState(PlayerStateMachine.PlayerDefenseState);
            }
        }

        private void Roll()
        {
            Vector3 dashDirection = MainPlayer.transform.forward;

            dashDirection.y = 0f;

            UpdateTargetRotation(dashDirection, false);

            if (MainPlayer.PlayerStateReusable.MovementInputWithNormalization != Vector2.zero)
            {
                UpdateTargetRotation(GetMovementInputDirection());

                dashDirection = GetTargetRotationDirection(MainPlayer.PlayerStateReusable.CurrentTargetRotation.y);
            }

            MainPlayer.Rigidbody.velocity = dashDirection * GetMovementSpeed(false);
        }
    }
}