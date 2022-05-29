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
            MainPlayer.ReusableData.MovementSpeedModifier = PlayerGroundData.RollData.SpeedModifier;
            
            StartAnimation(PlayerAnimationData.RollParameterHash);

            MainPlayer.ReusableData.RotationData = PlayerGroundData.DashData.RotationData;
            Roll();
        }

         protected override void OnLockedPerformed(InputAction.CallbackContext obj)
         {
         }

        public override void Exit()
        {
            base.Exit();
            MainPlayer.ReusableData.StopReading = false;

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
            MainPlayer.ReusableData.ShouldBlock = !MainPlayer.ReusableData.ShouldBlock;
        }

        private void OnDefensePerformed(InputAction.CallbackContext obj)
        {
            MainPlayer.ReusableData.ShouldBlock = true;
        }
        
        public override void OnAnimationEnterEvent()
        {
            MainPlayer.ReusableData.StopReading = true;
        }
        
        public override void OnAnimationHandleEvent()
        {
            if (MainPlayer.ReusableData.ShouldBlock)
            {
                PlayerStateMachine.ChangeState(PlayerStateMachine.PlayerDefenseState);
                return;
            }
            
            PlayerStateMachine.ChangeState(PlayerStateMachine.PlayerLockedMovementState);
        }

        public override void OnAnimationExitEvent()
        {
            if (MainPlayer.ReusableData.ShouldBlock)
            {
                PlayerStateMachine.ChangeState(PlayerStateMachine.PlayerDefenseState);
            }
        }

        private void Roll()
        {
            Vector3 dashDirection = MainPlayer.transform.forward;

            dashDirection.y = 0f;

            UpdateTargetRotation(dashDirection, false);

            if (MainPlayer.ReusableData.MovementInputWithNormalization != Vector2.zero)
            {
                UpdateTargetRotation(GetMovementInputDirection());

                dashDirection = GetTargetRotationDirection(MainPlayer.ReusableData.CurrentTargetRotation.y);
            }

            MainPlayer.Rigidbody.velocity = dashDirection * GetMovementSpeed(false);
        }
    }
}