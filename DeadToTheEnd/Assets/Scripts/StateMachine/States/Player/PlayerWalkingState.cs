using StateMachine.Player;
using UnityEngine.InputSystem;

namespace StateMachine.States.Player
{
    public sealed class PlayerWalkingState : PlayerMovingState
    {
        public PlayerWalkingState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();

            PlayerMovementStateMachine.ReusableData.MovementSpeedModifier = MovementData.WalkData.SpeedModifier;
        }

        protected override void OnWalkStarted(InputAction.CallbackContext obj)
        {
            base.OnWalkStarted(obj);
            PlayerMovementStateMachine.ChangeState(PlayerMovementStateMachine.RunningState);
        }
        
    }
}