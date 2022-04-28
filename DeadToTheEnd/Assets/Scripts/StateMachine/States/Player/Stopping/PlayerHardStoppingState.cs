using StateMachine.Player;

namespace StateMachine.States.Player.Stopping
{
    public class PlayerHardStoppingState : PlayerStoppingState
    {
        public PlayerHardStoppingState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
        {
        }
        
        public override void Enter()
        {
            base.Enter();
            PlayerMovementStateMachine.ReusableData.MovementDecelerationForce = MovementData.StopData.HardDeceleration;
            PlayerMovementStateMachine.ReusableData.CurrentJumpForce = AirborneData.PlayerJumpData.StrongForce;

        }

        protected override void OnMove()
        {
            if(PlayerMovementStateMachine.ReusableData.ShouldWalk)
                return;
            
            PlayerMovementStateMachine.ChangeState(PlayerMovementStateMachine.RunningState);
        }
    }
}