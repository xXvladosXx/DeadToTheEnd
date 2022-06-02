using UnityEngine;
using UnityEngine.InputSystem;

namespace StateMachine.Player.States.Movement.Grounded.Moving
{
    public class PlayerSprintingState: PlayerMovingState
    {
        private float _startTime;
        private bool _keepSprinting;
        private bool _shouldResetSprintState;

        public PlayerSprintingState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
        {
        }

        public override void Enter()
        {
            MainPlayer.PlayerStateReusable.MovementSpeedModifier = PlayerGroundData.SprintData.SpeedModifier;
            MainPlayer.PlayerStateReusable.TimeToReachTargetRotation =
                MainPlayer.PlayerStateReusable.RotationData.TargetRotationReachTimeSprint;

            base.Enter();

            StartAnimation(PlayerAnimationData.SprintParameterHash);

            _startTime = UnityEngine.Time.time;
            _shouldResetSprintState = true;

            if (!MainPlayer.PlayerStateReusable.ShouldSprint)
            {
                _keepSprinting = false;
            }
        }

        public override void Exit()
        {
            base.Exit();
            
            MainPlayer.PlayerStateReusable.TimeToReachTargetRotation =
                MainPlayer.PlayerStateReusable.RotationData.TargetRotationReachTimeDefault;

            StopAnimation(PlayerAnimationData.SprintParameterHash);

            if (_shouldResetSprintState)
            {
                _keepSprinting = false;

                MainPlayer.PlayerStateReusable.ShouldSprint = false;
            }
        }

        public override void Update()
        {
            base.Update();

            if (_keepSprinting)
            {
                return;
            }

            if (UnityEngine.Time.time < _startTime + PlayerGroundData.SprintData.SprintToRunTime)
            {
                return;
            }

            StopSprinting();
        }

        private void StopSprinting()
        {
            if (MainPlayer.PlayerStateReusable.MovementInputWithNormalization == Vector2.zero)
            {
                PlayerStateMachine.ChangeState(PlayerStateMachine.PlayerIdleState);

                return;
            }

            PlayerStateMachine.ChangeState(PlayerStateMachine.PlayerRunningState);
        }

        protected override void AddInputCallbacks()
        {
            base.AddInputCallbacks();

            MainPlayer.InputAction.PlayerActions.Sprint.performed += OnSprintPerformed;
        }

        protected override void RemoveInputCallbacks()
        {
            base.RemoveInputCallbacks();

            MainPlayer.InputAction.PlayerActions.Sprint.performed -= OnSprintPerformed;
        }

        private void OnSprintPerformed(InputAction.CallbackContext context)
        {
            _keepSprinting = true;

            MainPlayer.PlayerStateReusable.ShouldSprint = true;
        }

        protected override void OnMovementCanceled(InputAction.CallbackContext context)
        {
            PlayerStateMachine.ChangeState(PlayerStateMachine.HardStoppingState);

            base.OnMovementCanceled(context);
        }
    }
}