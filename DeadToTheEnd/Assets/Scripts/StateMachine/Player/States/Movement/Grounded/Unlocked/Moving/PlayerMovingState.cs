namespace StateMachine.Player.States.Movement.Grounded.Moving
{
    public abstract class PlayerMovingState: PlayerGroundedState
    {
        public PlayerMovingState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();

            StartAnimation(PlayerAnimationData.WasMovingParameterHash);
            StartAnimation(PlayerAnimationData.MovingParameterHash);
        }

        public override void Exit()
        {
            base.Exit();

            StopAnimation(PlayerAnimationData.MovingParameterHash);
        }
        
    }
}