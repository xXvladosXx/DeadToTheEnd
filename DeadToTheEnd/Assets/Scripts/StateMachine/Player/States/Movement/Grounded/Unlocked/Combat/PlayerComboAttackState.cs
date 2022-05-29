namespace StateMachine.Player.States.Movement.Grounded.Combat
{
    public class PlayerComboAttackState : PlayerAttackState
    {
        public PlayerComboAttackState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
        {
        }
        
        public override void Enter()
        {
            base.Enter();
            StartAnimation(PlayerAnimationData.ComboParameterHash);
        }

        public override void OnAnimationEnterEvent()
        {
            base.OnAnimationEnterEvent();
            StopAnimation(PlayerAnimationData.ComboParameterHash);
        }

        public override void Exit()
        {
            base.Exit();
            StopAnimation(PlayerAnimationData.ComboParameterHash);
        }
    }
}