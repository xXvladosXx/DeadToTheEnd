using UnityEngine.InputSystem;

namespace StateMachine.Player.States.Movement.Grounded.Moving
{
    public class PlayerWalkingState : PlayerMovingState
    {
        public PlayerWalkingState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
        {
        }

        public override void Enter()
        {
            MainPlayer.ReusableData.MovementSpeedModifier = PlayerGroundData.WalkData.SpeedModifier;

            MainPlayer.ReusableData.BackCameraRecenteringDatas = PlayerGroundData.WalkData.BackCameraRecenteringDatas;

            base.Enter();

            StartAnimation(PlayerAnimationData.WalkParameterHash);
        }

        public override void Exit()
        {
            base.Exit();

            StopAnimation(PlayerAnimationData.WalkParameterHash);

            SetBaseCameraRecenteringData();
        }

        protected override void OnMovementCanceled(InputAction.CallbackContext context)
        {
            PlayerStateMachine.ChangeState(PlayerStateMachine.PlayerIdleState);

            base.OnMovementCanceled(context);
        }
    }
}