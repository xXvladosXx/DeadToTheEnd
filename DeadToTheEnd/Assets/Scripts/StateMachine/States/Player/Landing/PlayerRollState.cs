using StateMachine.Player;

namespace StateMachine.States.Player.Landing
{
    public class PlayerRollState : PlayerLandingState
    {
        public PlayerRollState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
        {
        }
    }
}