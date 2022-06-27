namespace StateMachine.Player.States.Movement.Grounded.Combat
{
    public class PlayerFirstSkillCastState : PlayerSkillCastState
    {
        public PlayerFirstSkillCastState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();   
            RotateToPoint();
        }
    }
}