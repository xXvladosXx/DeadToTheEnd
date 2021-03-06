using Data.Combat;
using StateMachine.Player.States.Movement.Grounded.Locked;
using UnityEngine;
using UnityEngine.InputSystem;

namespace StateMachine.Player.States.Movement.Grounded.Combat
{
    public class PlayerAttackLockedState : PlayerLockedState
    {
        public PlayerAttackLockedState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();
            MainPlayer.DefenseColliderActivator.enabled = false;

            StartAnimation(PlayerAnimationData.Attack1ParameterHash);
            MainPlayer.Animator.applyRootMotion = true;
        }

        public override void Exit()
        {
            base.Exit();
            MainPlayer.InputAction.PlayerActions.Attack.performed -= OnAttackPerformed;

            StopAnimation(PlayerAnimationData.Attack1ParameterHash);
            MainPlayer.Animator.applyRootMotion = false;
        }

        protected override void AddInputCallbacks()
        {
            MainPlayer.AttackCalculator.OnDamageTaken += OnDamageTaken;
            MainPlayer.InputAction.PlayerActions.Dash.canceled += OnDashStarted;
            MainPlayer.InputAction.PlayerActions.Dash.performed += OnDefensePerformed;
        }

        protected override void RemoveInputCallbacks()
        {
            MainPlayer.AttackCalculator.OnDamageTaken -= OnDamageTaken;
            MainPlayer.InputAction.PlayerActions.Dash.canceled -= OnDashStarted;
            MainPlayer.InputAction.PlayerActions.Dash.performed -= OnDefensePerformed;
        }

        public override void TriggerOnStateAnimationEnterEvent()
        {
            base.TriggerOnStateAnimationEnterEvent();
            
            MainPlayer.InputAction.PlayerActions.Attack.performed += OnAttackPerformed;
        }

        public override void TriggerOnStateAnimationExitEvent()
        {
            base.TriggerOnStateAnimationExitEvent();
          
            MainPlayer.InputAction.PlayerActions.Attack.performed -= OnAttackPerformed;
        }
        protected override void OnLockedPerformed(InputAction.CallbackContext obj)
        {
        }

        public override void TriggerOnStateAnimationHandleEvent()
        {
            base.TriggerOnStateAnimationHandleEvent();
            PlayerStateMachine.ChangeState(PlayerStateMachine.PlayerLockedMovementState);
        }
        protected override void OnAttackPerformed(InputAction.CallbackContext obj)
        {
            MainPlayer.PlayerStateReusable.ShouldCombo = true;

            PlayerStateMachine.ChangeState(PlayerStateMachine.PlayerComboLockedAttackState);
        }

        private void OnDefensePerformed(InputAction.CallbackContext obj)
        {
            MainPlayer.PlayerStateReusable.ShouldBlock = !MainPlayer.PlayerStateReusable.ShouldBlock;
        }

        protected override void OnDashStarted(InputAction.CallbackContext context)
        {
            MainPlayer.PlayerStateReusable.ShouldBlock = false;
        }

        public override void FixedUpdate()
        {
            UnmovableLocked();
        }
    }
}