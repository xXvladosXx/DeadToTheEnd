namespace StateMachine.Player.States.Movement.Grounded.Locked.Hit
{
    public class PlayerKnockHitState : PlayerHitState
    {
        public PlayerKnockHitState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
        {
        }
        
        public override void Enter()
        {
            MainPlayer.ReusableData.IsKnocked = true;
            base.Enter();
           
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
            MainPlayer.ReusableData.IsKnocked = false;
        }
    }
}