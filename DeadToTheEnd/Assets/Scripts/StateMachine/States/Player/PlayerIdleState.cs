using StateMachine.Player;
using UnityEngine;

namespace StateMachine.States.Player
{
    public class PlayerIdleState : PlayerGroundedState
    {
        public PlayerIdleState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();

            PlayerMovementStateMachine.ReusableData.MovementSpeedModifier = 0;
            
            ResetVelocity();
        }

        public override void Update()
        {
            base.Update();
            
            if(PlayerMovementStateMachine.ReusableData.MovementInput == Vector2.zero)
                return;

            OnMove();
        }

       
    }
}