using StateMachine.Player;
using UnityEngine.InputSystem;

namespace StateMachine.States.Player.Stopping
{
    public class PlayerStoppingState : PlayerGroundedState
    {
        public PlayerStoppingState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();
            PlayerMovementStateMachine.ReusableData.MovementSpeedModifier = 0f;
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();

            RotateTowardsTargetRotation();
            if(!IsMovingHorizontally())
                return;
            
            DecelerateHorizontally();
        }

        public override void OnAnimationTransitionEvent()
        {
            PlayerMovementStateMachine.ChangeState(PlayerMovementStateMachine.IdleState);
        }

        protected override void AddInputActionsCallbacks()
        {
            base.AddInputActionsCallbacks();
            PlayerMovementStateMachine.Player.InputAction.PlayerActions.Movement.started += OnMovementStarted;
        }
        
        protected override void RemoveInputActionsCallbacks()
        {
            base.RemoveInputActionsCallbacks();
            PlayerMovementStateMachine.Player.InputAction.PlayerActions.Movement.started -= OnMovementStarted;
        }
        
        private void OnMovementStarted(InputAction.CallbackContext obj)
        {
            OnMove();
        }

        protected override void OnMovementCanceled(InputAction.CallbackContext obj)
        {
        }
    }
}