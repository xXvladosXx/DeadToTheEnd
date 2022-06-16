namespace StateMachine.Player.States.Movement.Grounded.Combat
{
    public class PlayerFourthSkillCastState : PlayerSkillCastState
    {
        public PlayerFourthSkillCastState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();
            
            StartAnimation(PlayerAnimationData.FourthSkillParameterHash);
        }

        public override void Exit()
        {
            base.Exit();
            
            StopAnimation(PlayerAnimationData.FourthSkillParameterHash);
        }

        public override void TriggerOnStateAnimationExitEvent()
        {
            base.TriggerOnStateAnimationExitEvent();
            PlayerStateMachine.ChangeState(PlayerStateMachine.PlayerIdleState);
        }
    }
}