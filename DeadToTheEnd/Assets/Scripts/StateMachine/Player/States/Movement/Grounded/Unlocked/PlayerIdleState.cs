using UnityEngine;
using UnityEngine.InputSystem;

namespace StateMachine.Player.States.Movement.Grounded
{
    public class PlayerIdleState : PlayerGroundedState
    {
        public PlayerIdleState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();
            
            ResetAnimatorSpeed();

            MainPlayer.PlayerStateReusable.BackCameraRecenteringDatas = PlayerGroundData.IdleData.BackCameraRecenteringDatas;

            ResetVelocity();
            StartAnimation(PlayerAnimationData.IdleParameterHash);
        }
        
        public override void Exit()
        {
            base.Exit();

            StopAnimation(PlayerAnimationData.IdleParameterHash);
        }

        public override void Update()
        {
            base.Update();
            if (MainPlayer.PlayerStateReusable.MovementInputWithNormalization == Vector2.zero)
            {
                return;
            }

            OnMove();
        }
        
        protected override void OnAttackPerformed(InputAction.CallbackContext obj)
        {
            base.OnAttackPerformed(obj);
            PlayerStateMachine.ChangeState(PlayerStateMachine.PlayerAttackState);
        }
        
        public override void FixedUpdate()
        {
            base.FixedUpdate();

            if (!IsMovingHorizontally())
            {
                return;
            }

            ResetVelocity();
        }
    }
}