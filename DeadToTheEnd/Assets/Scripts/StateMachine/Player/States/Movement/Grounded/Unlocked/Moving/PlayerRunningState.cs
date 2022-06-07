using UnityEngine;
using UnityEngine.InputSystem;

namespace StateMachine.Player.States.Movement.Grounded.Moving
{
    public class PlayerRunningState : PlayerMovingState
    {
        private float _startTime;

        public PlayerRunningState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();
            ResetAnimatorSpeed();

            MainPlayer.PlayerStateReusable.MovementSpeedModifier = PlayerGroundData.PlayerRunData.SpeedModifier;
            
            StartAnimation(PlayerAnimationData.RunParameterHash);
        }

        public override void Exit()
        {
            base.Exit();

            StopAnimation(PlayerAnimationData.RunParameterHash);
        }
       
        protected override void OnMovementCanceled(InputAction.CallbackContext context)
        {
            PlayerStateMachine.ChangeState(PlayerStateMachine.MediumStoppingState);

            base.OnMovementCanceled(context);
        }
    }
}