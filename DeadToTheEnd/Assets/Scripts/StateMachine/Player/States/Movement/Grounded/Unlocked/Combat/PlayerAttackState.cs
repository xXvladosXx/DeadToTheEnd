using CameraManage;
using Data.Combat;
using UnityEngine;
using UnityEngine.InputSystem;

namespace StateMachine.Player.States.Movement.Grounded.Combat
{
    public class PlayerAttackState : PlayerGroundedState
    {
        private bool _stopRotating;
        public PlayerAttackState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
        {
        }
        
        public override void Enter()
        {
            base.Enter();

            ResetAnimatorSpeed();
            MainPlayer.PlayerStateReusable.ShouldAttack = false;
            MainPlayer.PlayerStateReusable.IsMovingAfterStop = true;
            
            StartAnimation(PlayerAnimationData.Attack1ParameterHash);
            
            MainPlayer.Animator.applyRootMotion = true;
            _stopRotating = false;
        }
        
        public override void Exit()
        {
            base.Exit();
            
            StopAnimation(PlayerAnimationData.Attack1ParameterHash);
            MainPlayer.Animator.applyRootMotion = false;
        }

        public override void Update()
        {
            base.Update();
            
            if(_stopRotating) return;
            RotateToPoint();
        }

        public override void FixedUpdate()
        {
            Float();
        }

        protected override void OnLockedPerformed(InputAction.CallbackContext obj)
        {
        }


        public override void TriggerOnStateAnimationEnterEvent()
        {
            base.TriggerOnStateAnimationEnterEvent();
            MainPlayer.InputAction.PlayerActions.Attack.performed += OnAttackPerformed;
        }

        public override void TriggerOnStateAnimationExitEvent()
        {
            base.TriggerOnStateAnimationExitEvent();

            _stopRotating = true;
            MainPlayer.InputAction.PlayerActions.Attack.performed -= OnAttackPerformed;
        }
        
        public override void TriggerOnStateAnimationHandleEvent()
        {
            base.TriggerOnStateAnimationHandleEvent();
            PlayerStateMachine.ChangeState(PlayerStateMachine.PlayerIdleState);
        }
        protected override void OnAttackPerformed(InputAction.CallbackContext obj)
        {
            MainPlayer.PlayerStateReusable.ShouldCombo = true;

            PlayerStateMachine.ChangeState(PlayerStateMachine.PlayerComboAttackState);
        }

       
    }
}