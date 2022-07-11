namespace StateMachine.Player.States.Movement.Grounded.Combat
{
    public class PlayerRageAttackState : PlayerAttackState
    {
        public PlayerRageAttackState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();
            StartAnimation(PlayerAnimationData.RageParameterHash);
        }

        public override void Exit()
        {
            base.Exit();
            StopAnimation(PlayerAnimationData.RageParameterHash);
        }
    }
}