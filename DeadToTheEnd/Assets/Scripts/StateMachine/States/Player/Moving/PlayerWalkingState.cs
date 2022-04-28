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
            PlayerMovementStateMachine.ReusableData.CurrentJumpForce = AirborneData.PlayerJumpData.WeakForce;

        }

        protected override void OnMovementCanceled(InputAction.CallbackContext obj)
        {
            PlayerMovementStateMachine.ChangeState(PlayerMovementStateMachine.LightStoppingState);
        }

        protected override void OnWalkStarted(InputAction.CallbackContext obj)
        {
            base.OnWalkStarted(obj);
            PlayerMovementStateMachine.ChangeState(PlayerMovementStateMachine.RunningState);
        }
        
    }
}