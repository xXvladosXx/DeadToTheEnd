using CameraManage;
using UnityEngine;

namespace StateMachine.Player.States.Movement.Grounded.Locked.Hit
{
    public class PlayerKnockHitState : PlayerHitState
    {
        public PlayerKnockHitState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
        {
        }
        
        public override void Enter()
        {
            LookAtHitDirection();
            MainPlayer.PlayerStateReusable.IsKnocked = true;
            base.Enter();
           
            CinemachineCameraSwitcher.Instance.ShakeCamera(1f, .3f);
            StartAnimation(PlayerAnimationData.KnockdownParameterHash);
        }

        public override void OnAnimationEnterEvent()
        {
            base.OnAnimationEnterEvent();
            StopAnimation(PlayerAnimationData.KnockdownParameterHash);
        }

        public override void OnAnimationHandleEvent()
        {
            base.OnAnimationHandleEvent();
            MainPlayer.PlayerStateReusable.IsKnocked = false;
        }
    }
}