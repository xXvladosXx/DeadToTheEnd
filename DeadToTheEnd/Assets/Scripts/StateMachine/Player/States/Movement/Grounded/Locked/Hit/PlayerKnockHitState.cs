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
            LookAt(MainPlayer.PlayerStateReusable.LastHitFromTarget);
            MainPlayer.PlayerStateReusable.IsKnocked = true;
            base.Enter();
           
            StartAnimation(PlayerAnimationData.KnockdownParameterHash);
        }

        public override void Exit()
        {
            base.Exit();
            StopAnimation(PlayerAnimationData.KnockdownParameterHash);
        }

        public override void TriggerOnStateAnimationHandleEvent()
        {
            base.TriggerOnStateAnimationHandleEvent();
            MainPlayer.PlayerStateReusable.IsKnocked = false;
        }
    }
}