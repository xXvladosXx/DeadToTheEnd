using UnityEngine;
using UnityEngine.InputSystem;

namespace StateMachine.Player.States.Movement.Grounded
{
    public class PlayerDashState: PlayerGroundedState
    {
        private float _startTime;
        private int _consecutiveDashesUsed;
        private bool _shouldKeepRotating;

        public PlayerDashState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
        {
        }

        public override void Enter()
        {
            MainPlayer.PlayerStateReusable.MovementSpeedModifier = PlayerGroundData.DashData.SpeedModifier;

            base.Enter();

            StartAnimation(PlayerAnimationData.DashParameterHash);

            MainPlayer.PlayerStateReusable.RotationData = PlayerGroundData.DashData.RotationData;
            MainPlayer.PlayerStateReusable.IsMovingAfterStop = false;
            Dash();

            _shouldKeepRotating = MainPlayer.PlayerStateReusable.MovementInputWithNormalization != Vector2.zero;

            UpdateConsecutiveDashes();

            _startTime = UnityEngine.Time.time;
        }

        public override void Exit()
        {
            base.Exit();

            StopAnimation(PlayerAnimationData.DashParameterHash);
            ResetVelocity();
            SetBaseRotationData();
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();

            if (!_shouldKeepRotating)
            {
                return;
            }

            RotateTowardsTargetRotation();
        }

        public override void OnAnimationHandleEvent()
        {
            if (MainPlayer.PlayerStateReusable.MovementInputWithNormalization == Vector2.zero)
            {
                PlayerStateMachine.ChangeState(PlayerStateMachine.HardStoppingState);

                return;
            }

            PlayerStateMachine.ChangeState(PlayerStateMachine.PlayerSprintingState);
        }

        protected override void AddInputCallbacks()
        {

            MainPlayer.InputAction.PlayerActions.Movement.performed += OnMovementPerformed;

        }

        protected override void OnLockedPerformed(InputAction.CallbackContext obj)
        {
        }

        protected override void RemoveInputCallbacks()
        {

            MainPlayer.InputAction.PlayerActions.Movement.performed -= OnMovementPerformed;
        }

        protected override void OnMovementPerformed(InputAction.CallbackContext context)
        {
            base.OnMovementPerformed(context);

            _shouldKeepRotating = true;
        }

        private void Dash()
        {
            Vector3 dashDirection = MainPlayer.transform.forward;

            dashDirection.y = 0f;

            UpdateTargetRotation(dashDirection, false);

            if (MainPlayer.PlayerStateReusable.MovementInputWithNormalization != Vector2.zero)
            {
                UpdateTargetRotation(GetMovementInputDirection());

                dashDirection = GetTargetRotationDirection(MainPlayer.PlayerStateReusable.CurrentTargetRotation.y);
            }

            MainPlayer.Rigidbody.velocity = dashDirection * GetMovementSpeed(false);
        }

        private void UpdateConsecutiveDashes()
        {
            if (!IsConsecutive())
            {
                _consecutiveDashesUsed = 0;
            }

            ++_consecutiveDashesUsed;

            if (_consecutiveDashesUsed == PlayerGroundData.DashData.DashLimitAmount)
            {
                _consecutiveDashesUsed = 0;

                MainPlayer.InputAction.DisableActionFor(MainPlayer.InputAction.PlayerActions.Dash, PlayerGroundData.DashData.DashLimitCooldown);
            }
        }

        private bool IsConsecutive()
        {
            return UnityEngine.Time.time < _startTime + PlayerGroundData.DashData.TimeToBeConsider;
        }

        protected override void OnDashStarted(InputAction.CallbackContext context)
        {
        }
        
    }
}