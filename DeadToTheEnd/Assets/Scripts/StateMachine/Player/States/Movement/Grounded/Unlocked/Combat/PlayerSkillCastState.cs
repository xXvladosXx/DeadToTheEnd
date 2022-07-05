using Data.Combat;
using UnityEngine;
using UnityEngine.InputSystem;

namespace StateMachine.Player.States.Movement.Grounded.Combat
{
    public abstract class PlayerSkillCastState : PlayerGroundedState
    {
        public PlayerSkillCastState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();
            
            ResetAnimatorSpeed();
            
            MainPlayer.PlayerStateReusable.IsMovingAfterStop = true;
            MainPlayer.PlayerStateReusable.StopReading = true;
            
            MainPlayer.Animator.applyRootMotion = true;
            
            StartAnimation(PlayerAnimationData.SkillParameterHash);
            StartAnimation(Animator.StringToHash(MainPlayer.PlayerStateReusable.SkillAnimToPlay));
        }

        public override void Exit()
        {
            base.Exit();
            MainPlayer.PlayerStateReusable.StopReading = false;

            MainPlayer.Animator.applyRootMotion = false;
            
            StopAnimation(PlayerAnimationData.SkillParameterHash);
            StopAnimation(Animator.StringToHash(MainPlayer.PlayerStateReusable.SkillAnimToPlay));
        }
        public override void FixedUpdate()
        {
            Float();
        }
        protected override void OnDashStarted(InputAction.CallbackContext context)
        {
        }

        protected override void OnLockedPerformed(InputAction.CallbackContext obj)
        {
        }

        protected override void OnSecondSkillPerformed(InputAction.CallbackContext obj)
        {
        }
        protected override void OnFirstSkillPerformed(InputAction.CallbackContext obj)
        {
        }
        protected override void OnFourthSkillPerformed(InputAction.CallbackContext obj)
        {
        }
        protected override void OnThirdSkillPerformed(InputAction.CallbackContext obj)
        {
        }

        protected override void OnFifthSkillPerformed(InputAction.CallbackContext obj)
        {
        }

        protected override void OnDamageTaken(AttackData attackData)
        {
        }

        public override void TriggerOnStateAnimationExitEvent()
        {
            base.TriggerOnStateAnimationExitEvent();
            
            PlayerStateMachine.ChangeState(PlayerStateMachine.PlayerIdleState);
        }
    }
}