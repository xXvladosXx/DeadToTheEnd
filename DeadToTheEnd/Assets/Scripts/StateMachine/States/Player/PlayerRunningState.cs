using StateMachine.Player;
using UnityEngine.InputSystem;

namespace StateMachine.States.Player
{
    public sealed class PlayerRunningState : PlayerMovingState
    {
        public PlayerRunningState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();
            PlayerMovementStateMachine.ReusableData.MovementSpeedModifier = MovementData.RunData.SpeedModifier;
        }
        
        protected override void OnWalkStarted(InputAction.CallbackContext obj)
        {
            base.OnWalkStarted(obj);
            PlayerMovementStateMachine.ChangeState(PlayerMovementStateMachine.WalkingState);
        }
        
    }
}