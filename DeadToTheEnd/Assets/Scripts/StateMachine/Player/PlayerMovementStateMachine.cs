using Data.States;
using StateMachine.States.Player;
using StateMachine.States.Player.Airborne;
using StateMachine.States.Player.Stopping;

namespace StateMachine.Player
{
    public sealed class PlayerMovementStateMachine : StateMachine
    {
        public MainPlayer Player { get; }
        public PlayerStateReusableData ReusableData { get; }
        public PlayerIdleState IdleState { get; }
        public PlayerDashingState DashState { get; }
        public PlayerRunningState RunningState { get; }
        public PlayerWalkingState WalkingState { get; }
        public PlayerSprintingState SprintingState { get; }
        public PlayerMediumStoppingState MediumStoppingState { get; }
        public PlayerHardStoppingState HardStoppingState { get; }
        public PlayerLightStoppingState LightStoppingState { get; }
        public PlayerJumpingState PlayerJumpingState { get; }
        public PlayerFallingState PlayerFallingState { get; }


        public PlayerMovementStateMachine(MainPlayer player)
        {
            Player = player;

            ReusableData = new PlayerStateReusableData();
            IdleState = new PlayerIdleState(this);
            DashState = new PlayerDashingState(this);
            RunningState = new PlayerRunningState(this);
            WalkingState = new PlayerWalkingState(this);
            SprintingState = new PlayerSprintingState(this);
            HardStoppingState = new PlayerHardStoppingState(this);
            LightStoppingState = new PlayerLightStoppingState(this);
            MediumStoppingState = new PlayerMediumStoppingState(this);
            PlayerJumpingState = new PlayerJumpingState(this);
            PlayerFallingState = new PlayerFallingState(this);
        }
    }
}