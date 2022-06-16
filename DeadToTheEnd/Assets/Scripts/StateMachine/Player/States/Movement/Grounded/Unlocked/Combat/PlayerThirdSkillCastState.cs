namespace StateMachine.Player.States.Movement.Grounded.Combat
{
    public class PlayerThirdSkillCastState : PlayerSkillCastState
    {
        public PlayerThirdSkillCastState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
        {
        }
        public override void Enter()
        {
            base.Enter();
            
            StartAnimation(PlayerAnimationData.ThirdSkillParameterHash);
        }
        public override void Update()
        {
            base.Update();

            RotateToPoint();
        }
        public override void Exit()
        {
            base.Exit();
            
            StopAnimation(PlayerAnimationData.ThirdSkillParameterHash);
        }

        public override void TriggerOnStateAnimationExitEvent()
        {
            base.TriggerOnStateAnimationExitEvent();
            PlayerStateMachine.ChangeState(PlayerStateMachine.PlayerIdleState);
        }
    }
}