using StateMachine.Player.States.Movement.Grounded.Locked;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

namespace StateMachine.Player.States.Movement.Grounded.Defense
{
    public class PlayerDefenseImpactState : PlayerLockedState
    {
        public PlayerDefenseImpactState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();
            MainPlayer.PlayerStateReusable.MovementSpeedModifier = 0;
            StartAnimation(PlayerAnimationData.DefenseImpactParameterHash);
        }

        public override void FixedUpdate()
        {
            UnmovableLocked();
        }

        protected override void AddInputCallbacks()
        {
            MainPlayer.InputAction.PlayerActions.Dash.canceled += OnDefenseCanceled;
            MainPlayer.InputAction.PlayerActions.Dash.performed += OnDefensePerformed;
        }

        private void OnDefensePerformed(InputAction.CallbackContext obj)
        {
            MainPlayer.PlayerStateReusable.ShouldBlock = false;
        }

        private void OnDefenseCanceled(InputAction.CallbackContext obj)
        {
            MainPlayer.PlayerStateReusable.ShouldBlock = false;
        }

        protected override void RemoveInputCallbacks()
        {
            MainPlayer.InputAction.PlayerActions.Dash.canceled -= OnDefenseCanceled;
            MainPlayer.InputAction.PlayerActions.Dash.performed -= OnDefensePerformed;
        }

        public override void TriggerOnStateAnimationHandleEvent()
        {
            if (MainPlayer.PlayerStateReusable.ShouldBlock)
            {
                PlayerStateMachine.ChangeState(PlayerStateMachine.PlayerDefenseState);
            }
            else
            {
                PlayerStateMachine.ChangeState(PlayerStateMachine.PlayerLockedMovementState);
            }
        }

        public override void Exit()
        {
            base.Exit();
            StopAnimation(PlayerAnimationData.DefenseImpactParameterHash);
        }
    }
}