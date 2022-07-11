using UnityEngine;
using UnityEngine.InputSystem;

namespace StateMachine.Player.States.Movement.Grounded.Stopping
{
    public abstract class PlayerStoppingState: PlayerGroundedState
    {
        public PlayerStoppingState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();
            MainPlayer.PlayerStateReusable.IsMovingAfterStop = true;
            
            if (MainPlayer.PlayerStateReusable.ShouldAttack)
            {
                PlayerStateMachine.ChangeState(PlayerStateMachine.PlayerAttackState);
                return;
            }

            SetBaseCameraRecenteringData();

            StartAnimation(PlayerAnimationData.StoppingParameterHash);
        }

        public override void Exit()
        {
            base.Exit();
            
            StopAnimation(PlayerAnimationData.WasMovingParameterHash);
            StopAnimation(PlayerAnimationData.StoppingParameterHash);
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();

            RotateTowardsTargetRotation();

            if (!IsMovingHorizontally())
            {
                return;
            }

            DecelerateHorizontally();
        }

        public override void TriggerOnStateAnimationEnterEvent()
        {
            if(MainPlayer.PlayerStateReusable.ShouldAttack) return;
            PlayerStateMachine.ChangeState(PlayerStateMachine.PlayerIdleState);
        }
        
        protected override void AddInputCallbacks()
        {
            base.AddInputCallbacks();

            MainPlayer.InputAction.PlayerActions.Movement.started += OnMovementStarted;
        }

        protected override void RemoveInputCallbacks()
        {
            base.RemoveInputCallbacks();

            MainPlayer.InputAction.PlayerActions.Movement.started -= OnMovementStarted;
        }
        protected override void OnAttackPerformed(InputAction.CallbackContext obj)
        {
            base.OnAttackPerformed(obj);
            PlayerStateMachine.ChangeState(PlayerStateMachine.PlayerAttackState);
        }
        protected override void OnLockedPerformed(InputAction.CallbackContext obj)
        {
        }
        private void OnMovementStarted(InputAction.CallbackContext context)
        {
            OnMove();
        }
    }
}