using StateMachine.Player;
using UnityEngine;

namespace StateMachine.States.Player.Airborne
{
    public class PlayerAirborneState : PlayerMovementState
    {
        public PlayerAirborneState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();
            
            ResetSprintState();
        }

        protected override void OnContactWithGround(Collider collider)
        {
            PlayerMovementStateMachine.ChangeState(PlayerMovementStateMachine.IdleState);
        }

        protected virtual void ResetSprintState()
        {
            PlayerMovementStateMachine.ReusableData.ShouldSprint = false;
        }
    }
}