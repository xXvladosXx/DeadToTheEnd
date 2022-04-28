using Data.States;
using StateMachine.Player;
using UnityEngine;
using UnityEngine.InputSystem;

namespace StateMachine.States.Player
{
    public sealed class PlayerRunningState : PlayerMovingState
    {
        private PlayerSprintData _playerSprintData;
        private float _startTime;
        public PlayerRunningState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
        {
            _playerSprintData = MovementData.SprintData;
        }

        public override void Enter()
        {
            base.Enter();
            PlayerMovementStateMachine.ReusableData.MovementSpeedModifier = MovementData.RunData.SpeedModifier;
            PlayerMovementStateMachine.ReusableData.CurrentJumpForce = AirborneData.PlayerJumpData.MediumForce;

            _startTime = Time.time;
        }

        public override void Update()
        {
            base.Update();
            if(!PlayerMovementStateMachine.ReusableData.ShouldWalk)
                return;
            
            if(Time.time < _startTime + _playerSprintData.RunToWalkTime)
                return;

            StopRunning();
        }

        private void StopRunning()
        {
            if (PlayerMovementStateMachine.ReusableData.MovementInput == Vector2.zero)
            {
                PlayerMovementStateMachine.ChangeState(PlayerMovementStateMachine.IdleState);
                return;
            }
            
            PlayerMovementStateMachine.ChangeState(PlayerMovementStateMachine.WalkingState);
        }

        protected override void OnMovementCanceled(InputAction.CallbackContext obj)
        {
            PlayerMovementStateMachine.ChangeState(PlayerMovementStateMachine.MediumStoppingState);
        }

        protected override void OnWalkStarted(InputAction.CallbackContext obj)
        {
            base.OnWalkStarted(obj);
            PlayerMovementStateMachine.ChangeState(PlayerMovementStateMachine.WalkingState);
        }
        
    }
}