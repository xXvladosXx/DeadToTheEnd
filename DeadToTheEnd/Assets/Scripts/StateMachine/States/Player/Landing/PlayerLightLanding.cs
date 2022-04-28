using StateMachine.Player;

namespace StateMachine.States.Player.Landing
{
    public class PlayerLightLanding : PlayerLandingState
    {
        public PlayerLightLanding(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
        {
        }
    }
}