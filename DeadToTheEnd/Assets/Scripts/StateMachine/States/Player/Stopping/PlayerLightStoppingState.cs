using StateMachine.Player;

namespace StateMachine.States.Player.Stopping
{
    public class PlayerLightStoppingState : PlayerStoppingState
    {
        public PlayerLightStoppingState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();
            PlayerMovementStateMachine.ReusableData.MovementDecelerationForce = MovementData.StopData.LightDeceleration;
            PlayerMovementStateMachine.ReusableData.CurrentJumpForce = AirborneData.PlayerJumpData.WeakForce;

        }
    }
}