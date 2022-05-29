namespace StateMachine.Player.States.Movement.Grounded.Stopping
{
    public class PlayerMediumStoppingState : PlayerStoppingState
    {
        public PlayerMediumStoppingState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();

            StartAnimation(PlayerAnimationData.MediumStopParameterHash);

            MainPlayer.ReusableData.MovementDecelerationForce = PlayerGroundData.StopData.MediumDeceleration;
        }

        public override void Exit()
        {
            base.Exit();

            StopAnimation(PlayerAnimationData.MediumStopParameterHash);
        }
    }
}