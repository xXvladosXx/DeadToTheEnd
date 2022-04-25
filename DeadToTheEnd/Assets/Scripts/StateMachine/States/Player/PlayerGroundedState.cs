using StateMachine.Player;
using UnityEngine.InputSystem;

namespace StateMachine.States.Player
{
    public class PlayerGroundedState : PlayerMovementState
    {
        public PlayerGroundedState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
        {
        }
        
        protected virtual void OnMove()
        {
            if (PlayerMovementStateMachine.ReusableData.ShouldWalk)
            {
                PlayerMovementStateMachine.ChangeState(PlayerMovementStateMachine.WalkingState);
            }
            
            PlayerMovementStateMachine.ChangeState(PlayerMovementStateMachine.RunningState);
        }
        protected override void AddInputActionsCallbacks()
        {
            base.AddInputActionsCallbacks();
            PlayerMovementStateMachine.MainPlayer.InputAction.PlayerActions.Movement.canceled += OnMovementCanceled;
        }

        protected override void RemoveInputActionsCallbacks()
        {
            base.RemoveInputActionsCallbacks();
            PlayerMovementStateMachine.MainPlayer.InputAction.PlayerActions.Movement.canceled -= OnMovementCanceled;
        }

        protected virtual void OnMovementCanceled(InputAction.CallbackContext obj)
        {
            PlayerMovementStateMachine.ChangeState(PlayerMovementStateMachine.IdleState);
        }
    }
}