using Data.Combat;
using UnityEngine.InputSystem;

namespace StateMachine.Player.States.Movement.Grounded.Combat
{
    public class PlayerAttackState : PlayerGroundedState
    {
        public PlayerAttackState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
        {
        }
        
        public override void Enter()
        {
            base.Enter();
            
            StartAnimation(PlayerAnimationData.Attack1ParameterHash);
            MainPlayer.Animator.applyRootMotion = true;

        }

        public override void Exit()
        {
            base.Exit();
            
            StopAnimation(PlayerAnimationData.Attack1ParameterHash);
            MainPlayer.Animator.applyRootMotion = false;
        }

        public override void FixedUpdate()
        {
            Float();
        }

        protected override void OnLockedPerformed(InputAction.CallbackContext obj)
        {
        }


        public override void OnAnimationEnterEvent()
        {
            base.OnAnimationEnterEvent();
            MainPlayer.InputAction.PlayerActions.Attack.performed += OnAttackPerformed;
        }

        public override void OnAnimationExitEvent()
        {
            base.OnAnimationExitEvent();
            
            MainPlayer.InputAction.PlayerActions.Attack.performed -= OnAttackPerformed;
        }
        
        public override void OnAnimationHandleEvent()
        {
            base.OnAnimationHandleEvent();
            PlayerStateMachine.ChangeState(PlayerStateMachine.PlayerIdleState);
        }
        protected override void OnAttackPerformed(InputAction.CallbackContext obj)
        {
            MainPlayer.ReusableData.ShouldCombo = true;

            PlayerStateMachine.ChangeState(PlayerStateMachine.PlayerComboAttackState);
        }

    }
}