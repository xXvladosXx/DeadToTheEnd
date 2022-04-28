using Data.States;
using StateMachine.Player;
using UnityEngine;
using UnityEngine.InputSystem;

namespace StateMachine.States.Player
{
    public class PlayerDashingState : PlayerGroundedState
    {
        private PlayerDashData _playerDashData;
        private float _startTime;
        private int _consecutiveDashesUsed;
        private bool _shouldKeepRotating;
        
        private float _dashToSprintTime = .3f;
        public PlayerDashingState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
        {
            _playerDashData = MovementData.DashData;
        }

        public override void Enter()
        {
            base.Enter();

            PlayerMovementStateMachine.ReusableData.MovementSpeedModifier = _playerDashData.SpeedModifier;
            PlayerMovementStateMachine.ReusableData.RotationData = _playerDashData.RotationData;
            
            AddForceOnTransitionFromIdleState();
            
            _shouldKeepRotating = PlayerMovementStateMachine.ReusableData.MovementInput != Vector2.zero;

            UpdateConsecutiveDashed();
            
            _startTime = Time.time;
        }

        public override void Update()
        {
            base.Update();
            
            if(Time.time < _startTime + _dashToSprintTime)
                return;
            
            PlayerMovementStateMachine.ChangeState(PlayerMovementStateMachine.SprintingState);
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();
            
            if(!_shouldKeepRotating)
                return;
            
            RotateTowardsTargetRotation();

            if (IsMovingUp())
            {
                
            }
        }

        public override void Exit()
        {
            base.Exit();
            SetBaseRotationData();
        }

        private void UpdateConsecutiveDashed()
        {
            if (!IsConsecutive())
            {
                _consecutiveDashesUsed = 0;
            }

            _consecutiveDashesUsed++;

            if (_consecutiveDashesUsed == _playerDashData.DashLimitAmount)
            {
                _consecutiveDashesUsed = 0;
                PlayerMovementStateMachine.Player.InputAction.DisableActionFor(
                    PlayerMovementStateMachine.Player.InputAction.PlayerActions.Dash, _playerDashData.DashLimitCooldown);
            }
        }

        private bool IsConsecutive() => Time.time < _startTime + _playerDashData.TimeToBeConsider;

        private void AddForceOnTransitionFromIdleState()
        {
            if(PlayerMovementStateMachine.ReusableData.MovementInput != Vector2.zero)
                return;

            Vector3 characterRotationDirection = PlayerMovementStateMachine.Player.transform.forward;
            characterRotationDirection.y = 0f;
            UpdateTargetRotation(characterRotationDirection, false);
            PlayerMovementStateMachine.Player.Rigidbody.velocity = characterRotationDirection * GetMovementSpeed();
        }

        public override void OnAnimationTransitionEvent()
        {
            if (PlayerMovementStateMachine.ReusableData.MovementInput == Vector2.zero)
            {
                PlayerMovementStateMachine.ChangeState(PlayerMovementStateMachine.HardStoppingState);
                return;
            }
            
            PlayerMovementStateMachine.ChangeState(PlayerMovementStateMachine.SprintingState);
        }

        protected override void AddInputActionsCallbacks()
        {
            base.AddInputActionsCallbacks();
            PlayerMovementStateMachine.Player.InputAction.PlayerActions.Movement.performed += OnMovementPerformed;
        }

        protected override void RemoveInputActionsCallbacks()
        {
            base.RemoveInputActionsCallbacks();

            PlayerMovementStateMachine.Player.InputAction.PlayerActions.Movement.performed -= OnMovementPerformed;
        }

        private void OnMovementPerformed(InputAction.CallbackContext obj)
        {
            _shouldKeepRotating = true;
        }

        protected override void OnDashStarted(InputAction.CallbackContext obj)
        {
        }

        protected override void OnMovementCanceled(InputAction.CallbackContext obj)
        {
        }
    }
}