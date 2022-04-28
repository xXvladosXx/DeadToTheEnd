using Data.States;
using StateMachine.Player;
using Unity.VisualScripting;
using UnityEngine;

namespace StateMachine.States.Player.Airborne
{
    public class PlayerFallingState : PlayerAirborneState
    {
        private PlayerFallData _fallData;
        public PlayerFallingState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();
            PlayerMovementStateMachine.ReusableData.MovementSpeedModifier = 0f;
            ResetVerticalVelocity();
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();
            LimitVerticalVelocity();
        }

        protected override void ResetSprintState()
        {
        }

        private void LimitVerticalVelocity()
        {
            Vector3 playerVel = GetPlayerVerticalVelocity;
            
            if(PlayerMovementStateMachine.Player.Rigidbody.velocity.y >= -_fallData.SpeedLimit) 
                return;

            Vector3 limitedVel = new Vector3(0f,
                _fallData.SpeedLimit - playerVel.y, 0f);
            
            PlayerMovementStateMachine.Player.Rigidbody.AddForce(limitedVel, ForceMode.VelocityChange);
        }
    }
}