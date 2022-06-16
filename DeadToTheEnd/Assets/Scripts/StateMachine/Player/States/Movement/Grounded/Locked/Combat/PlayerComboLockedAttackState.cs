namespace StateMachine.Player.States.Movement.Grounded.Combat
{
    public class PlayerComboLockedAttackState : PlayerAttackLockedState
    {
        public PlayerComboLockedAttackState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();
            StartAnimation(PlayerAnimationData.ComboParameterHash);
        }

        public override void TriggerOnStateAnimationEnterEvent()
        {
            base.TriggerOnStateAnimationEnterEvent();
            StopAnimation(PlayerAnimationData.ComboParameterHash);
        }

        public override void Exit()
        {
            base.Exit();
            StopAnimation(PlayerAnimationData.ComboParameterHash);
        }
    }
}