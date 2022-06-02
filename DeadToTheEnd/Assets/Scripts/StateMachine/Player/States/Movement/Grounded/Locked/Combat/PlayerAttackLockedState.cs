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
            
            StopAnimation(PlayerAnimationData.Attack1ParameterHash);
            MainPlayer.Animator.applyRootMotion = false;
        }

        protected override void AddInputCallbacks()
        {
            MainPlayer.Health.OnDamageTaken += OnDamageTaken;
            MainPlayer.InputAction.PlayerActions.Dash.canceled += OnDashStarted;
            MainPlayer.InputAction.PlayerActions.Dash.performed += OnDefensePerformed;
        }

        protected override void RemoveInputCallbacks()
        {
            MainPlayer.Health.OnDamageTaken -= OnDamageTaken;
            MainPlayer.InputAction.PlayerActions.Dash.canceled -= OnDashStarted;
            MainPlayer.InputAction.PlayerActions.Dash.performed -= OnDefensePerformed;
        }

        public override void OnAnimationEnterEvent()
        {
            base.OnAnimationEnterEvent();

            //MainPlayer.AttackColliderActivator.enabled = true;
            //MainPlayer.AttackColliderActivator.ActivateCollider(.2f, new AttackData());
            MainPlayer.InputAction.PlayerActions.Attack.performed += OnAttackPerformed;
        }

        public override void OnAnimationExitEvent()
        {
            base.OnAnimationExitEvent();
           // MainPlayer.AttackColliderActivator.enabled = false;
           // MainPlayer.AttackColliderActivator.DeactivateCollider();
            MainPlayer.InputAction.PlayerActions.Attack.performed -= OnAttackPerformed;
        }
        protected override void OnLockedPerformed(InputAction.CallbackContext obj)
        {
        }

        public override void OnAnimationHandleEvent()
        {
            base.OnAnimationHandleEvent();
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