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
        }
        public override void Update()
        {
            base.Update();

            RotateToPoint();
        }
        public override void Exit()
        {
            base.Exit();
        }
    }
}