using StateMachine.Player;

namespace StateMachine.States.Player.Stopping
{
    public class PlayerMediumStoppingState : PlayerStoppingState
    {
        public PlayerMediumStoppingState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
        {
        }
        
        public override void Enter()
        {
            base.Enter();
            PlayerMovementStateMachine.ReusableData.MovementDecelerationForce = MovementData.StopData.MediumDeceleration;
            PlayerMovementStateMachine.ReusableData.CurrentJumpForce = AirborneData.PlayerJumpData.MediumForce;

        }
    }
}