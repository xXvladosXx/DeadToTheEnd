using CameraManage;

namespace StateMachine.Player.States.Movement.Grounded.Locked.Hit
{
    public class PlayerMediumHitState : PlayerHitState
    {
        public PlayerMediumHitState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();
            CinemachineCameraSwitcher.Instance.ShakeCamera(.6f, .3f);

            StartAnimation(PlayerAnimationData.MediumHitParameterHash);
        }
        
        public override void Exit()
        {
            base.Exit();
            StopAnimation(PlayerAnimationData.MediumHitParameterHash);
        }
    }
}