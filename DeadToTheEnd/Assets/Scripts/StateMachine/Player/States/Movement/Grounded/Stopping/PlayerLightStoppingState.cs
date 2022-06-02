using StateMachine.Player.States.Movement.Grounded.Moving;

namespace StateMachine.Player.States.Movement.Grounded.Stopping
{
    public class PlayerLightStoppingState : PlayerMovingState
    {
        public PlayerLightStoppingState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();

            MainPlayer.PlayerStateReusable.MovementDecelerationForce = PlayerGroundData.StopData.LightDeceleration;
        }
    }
}