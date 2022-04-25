using Data.States;
using StateMachine.States.Player;

namespace StateMachine.Player
{
    public sealed class PlayerMovementStateMachine : StateMachine
    {
        public MainPlayer MainPlayer { get; }
        public PlayerStateReusableData ReusableData { get; }
        public PlayerIdleState IdleState { get; }
        public PlayerRunningState RunningState { get; }
        public PlayerWalkingState WalkingState { get; }
        public PlayerSprintingState SprintingState { get; }


        public PlayerMovementStateMachine(MainPlayer mainPlayer)
        {
            MainPlayer = mainPlayer;

            ReusableData = new PlayerStateReusableData();
            IdleState = new PlayerIdleState(this);
            RunningState = new PlayerRunningState(this);
            WalkingState = new PlayerWalkingState(this);
            SprintingState = new PlayerSprintingState(this);
        }
    }
}