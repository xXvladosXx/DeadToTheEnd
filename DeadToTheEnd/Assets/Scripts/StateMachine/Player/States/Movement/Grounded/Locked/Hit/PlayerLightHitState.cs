using CameraManage;

namespace StateMachine.Player.States.Movement.Grounded.Locked.Hit
{
    public class PlayerLightHitState : PlayerHitState
    {
        public PlayerLightHitState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
        {
        }
        
        public override void Enter()
        {
            base.Enter();
            CinemachineCameraSwitcher.Instance.ShakeCamera(.3f, .3f);

            StartAnimation(PlayerAnimationData.EasyHitParameterHash);
        }

        public override void Exit()
        {
            base.Exit();
            StopAnimation(PlayerAnimationData.EasyHitParameterHash);
        }
    }
}