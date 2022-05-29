namespace StateMachine.Player.States.Movement.Grounded.Stopping
{
    public class PlayerHardStoppingState: PlayerStoppingState
    {
        public PlayerHardStoppingState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();

            StartAnimation(PlayerAnimationData.HardStopParameterHash);

            MainPlayer.ReusableData.MovementDecelerationForce = PlayerGroundData.StopData.HardDeceleration;

        }

        public override void Exit()
        {
            base.Exit();

            StopAnimation(PlayerAnimationData.HardStopParameterHash);
        }

        
        protected override void OnMove()
        {
            if (MainPlayer.ReusableData.ShouldWalk)
            {
                return;
            }

            PlayerStateMachine.ChangeState(PlayerStateMachine.PlayerRunningState);
        }
    }
}