using Data.States;
using StateMachine.Player;
using UnityEngine;
using UnityEngine.InputSystem;

namespace StateMachine.States.Player
{
    public sealed class PlayerSprintingState : PlayerMovingState
    {
        private PlayerSprintData _playerSprintData;
        private float _startTime;
        private bool _keepSprinting;
        private bool _shouldResetSprint;
        public PlayerSprintingState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
        {
            _playerSprintData = MovementData.SprintData;
        }

        public override void Enter()
        {
            base.Enter();
            PlayerMovementStateMachine.ReusableData.MovementSpeedModifier = _playerSprintData.SpeedModifier;
            PlayerMovementStateMachine.ReusableData.CurrentJumpForce = AirborneData.PlayerJumpData.StrongForce;

            _shouldResetSprint = true;

            _startTime = Time.time;
        }

        public override void Exit()
        {
            base.Exit();
            _keepSprinting = false;

            if (_shouldResetSprint)
            {
                _keepSprinting = false;
                PlayerMovementStateMachine.ReusableData.ShouldSprint = false;   
            }
        }

        public override void Update()
        {
            base.Update();
            if(_keepSprinting)
                return;

            if (Time.time < _startTime + _playerSprintData.SprintToRunTime)
                return;

            StopSprinting();
        }

        private void StopSprinting()
        {
            if (PlayerMovementStateMachine.ReusableData.MovementInput == Vector2.zero)
            {
                PlayerMovementStateMachine.ChangeState(PlayerMovementStateMachine.IdleState);
                return;
            }
            
            PlayerMovementStateMachine.ChangeState(PlayerMovementStateMachine.RunningState);
        }

        protected override void OnMovementCanceled(InputAction.CallbackContext obj)
        {
            PlayerMovementStateMachine.ChangeState(PlayerMovementStateMachine.HardStoppingState);
        }

        protected override void OnJumpStarted(InputAction.CallbackContext obj)
        {
            _shouldResetSprint = false;
            base.OnJumpStarted(obj);
        }

        protected override void OnFall()
        {
            _shouldResetSprint = false;
            
            base.OnFall();
        }

        protected override void AddInputActionsCallbacks()
        {
            base.AddInputActionsCallbacks();
            PlayerMovementStateMachine.Player.InputAction.PlayerActions.Sprint.performed += OnSprintPerform;

        }

        protected override void RemoveInputActionsCallbacks()
        {
            base.RemoveInputActionsCallbacks();
            PlayerMovementStateMachine.Player.InputAction.PlayerActions.Sprint.performed -= OnSprintPerform;
        }
        
        private void OnSprintPerform(InputAction.CallbackContext obj)
        {
            _keepSprinting= true;
            PlayerMovementStateMachine.ReusableData.ShouldSprint = true;
        }

    }
}