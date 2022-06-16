using UnityEngine.InputSystem;

namespace StateMachine.Player.States.Movement.Grounded.Combat
{
    public class PlayerSkillCastState : PlayerGroundedState
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
        }

        public override void Exit()
        {
            base.Exit();
            MainPlayer.PlayerStateReusable.StopReading = false;

            MainPlayer.Animator.applyRootMotion = false;
            StopAnimation(PlayerAnimationData.SkillParameterHash);
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

        public override void TriggerOnStateAnimationExitEvent()
        {
            base.TriggerOnStateAnimationExitEvent();
            
            PlayerStateMachine.ChangeState(PlayerStateMachine.PlayerIdleState);
        }
    }
}