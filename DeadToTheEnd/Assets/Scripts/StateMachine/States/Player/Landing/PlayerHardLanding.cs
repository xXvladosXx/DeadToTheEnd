using StateMachine.Player;

namespace StateMachine.States.Player.Landing
{
    public class PlayerHardLanding : PlayerLandingState
    {
        public PlayerHardLanding(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
        {
        }
    }
}