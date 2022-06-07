using UnityEngine.InputSystem;

namespace StateMachine.Player.States.Movement.Grounded.Moving
{
    public abstract class PlayerMovingState: PlayerGroundedState
    {
        public PlayerMovingState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();

            StartAnimation(PlayerAnimationData.WasMovingParameterHash);
            StartAnimation(PlayerAnimationData.MovingParameterHash);
        }

        protected override void OnAttackPerformed(InputAction.CallbackContext obj)
        {
            base.OnAttackPerformed(obj);
            PlayerStateMachine.ChangeState(PlayerStateMachine.PlayerAttackState);
        }

        public override void Exit()
        {
            base.Exit();

            StopAnimation(PlayerAnimationData.MovingParameterHash);
        }
        
    }
}