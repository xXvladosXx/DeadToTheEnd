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
            StartAnimation(PlayerAnimationData.MediumHitParameterHash);
        }
        
        public override void OnAnimationEnterEvent()
        {
            base.OnAnimationEnterEvent();
            StopAnimation(PlayerAnimationData.MediumHitParameterHash);
        }


        public override void Exit()
        {
            base.Exit();
            StopAnimation(PlayerAnimationData.MediumHitParameterHash);
        }
    }
}