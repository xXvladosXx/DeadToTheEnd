using UnityEngine;

namespace StateMachine.Player.States.Movement.Grounded
{
    public class PlayerIdleState : PlayerGroundedState
    {
        public PlayerIdleState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
        {
        }

        public override void Enter()
        {
            MainPlayer.PlayerStateReusable.MovementSpeedModifier = 0f;

            MainPlayer.PlayerStateReusable.BackCameraRecenteringDatas = PlayerGroundData.IdleData.BackCameraRecenteringDatas;

            base.Enter();
            
            StartAnimation(PlayerAnimationData.IdleParameterHash);

            ResetVelocity();
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