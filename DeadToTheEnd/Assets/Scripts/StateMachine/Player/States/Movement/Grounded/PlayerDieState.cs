using UnityEngine;

namespace StateMachine.Player.States.Movement.Grounded
{
    public class PlayerDieState : PlayerGroundedState
    {
        public PlayerDieState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
        {
        }

        public override void Enter()
        {
            ResetVelocity();
            PlayerStateMachine.MainPlayer.Animator.Play("Death");
            MainPlayer.InputAction.enabled = false;
        }

        public override void Update()
        {
        }

        public override void FixedUpdate()
        {
            Float();
        }

        public override void HandleInput()
        {
        }

        protected override void AddInputCallbacks()
        {
        }

        protected override void RemoveInputCallbacks()
        {
        }
    }
}