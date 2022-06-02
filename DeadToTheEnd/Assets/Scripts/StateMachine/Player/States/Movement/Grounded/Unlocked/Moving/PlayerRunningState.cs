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
            MainPlayer.PlayerStateReusable.MovementSpeedModifier = PlayerGroundData.PlayerRunData.SpeedModifier;

            CalculateTime = 0;
            base.Enter();

            StartAnimation(PlayerAnimationData.RunParameterHash);

            _startTime = UnityEngine.Time.time;
        }

        public override void Exit()
        {
            base.Exit();

            StopAnimation(PlayerAnimationData.RunParameterHash);
        }

        public override void Update()
        {
            base.Update();

            if (!MainPlayer.PlayerStateReusable.ShouldWalk)
            {
                return;
            }

            if (UnityEngine.Time.time < _startTime + PlayerGroundData.SprintData.RunToWalkTime)
            {
                return;
            }

            StopRunning();
        }

        private void StopRunning()
        {
            if (MainPlayer.PlayerStateReusable.MovementInputWithNormalization == Vector2.zero)
            {
                PlayerStateMachine.ChangeState(PlayerStateMachine.PlayerIdleState);

                return;
            }

            PlayerStateMachine.ChangeState(PlayerStateMachine.PlayerWalkingState);
        }

        protected override void OnMovementCanceled(InputAction.CallbackContext context)
        {
            PlayerStateMachine.ChangeState(PlayerStateMachine.MediumStoppingState);

            base.OnMovementCanceled(context);
        }
    }
}