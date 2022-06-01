using UnityEngine;

namespace StateMachine.Player.States.Movement.Grounded.Stopping
{
    public class PlayerLockedStoppingState : PlayerStoppingState
    {
        public PlayerLockedStoppingState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
        {
        }
        
        public override void Enter()
        {
            base.Enter();
            ResetVelocity();
            
            Debug.Log("Enter");

            MainPlayer.ReusableData.MovementDecelerationForce = 0;
            MainPlayer.ReusableData.MovementSpeedModifier = 0;
            PlayerStateMachine.ChangeState(PlayerStateMachine.PlayerLockedMovementState);
        }

        public override void Update()
        {
        }

        public override void FixedUpdate()
        {
            ResetVelocity();
        }

        public override void Exit()
        {
            ResetVelocity();

            base.Exit();
        }

    }
}