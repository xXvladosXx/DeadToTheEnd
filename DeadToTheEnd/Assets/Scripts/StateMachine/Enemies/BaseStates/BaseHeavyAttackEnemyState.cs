namespace StateMachine.Enemies.BaseStates
{
    public class BaseHeavyAttackEnemyState : BaseAttackEnemyState
    {
        public BaseHeavyAttackEnemyState(StateMachine stateMachine) : base(stateMachine)
        {
        }
        
        public override void Enter()
        {
            base.Enter();
            
            StateMachine.StartCooldown(typeof(BaseHeavyAttackEnemyState),
                EnemyData.EnemyHeavyAttackData.AttackCooldown);
            
            StartAnimation(Enemy.EnemyAnimationData.HeavyAttackParameterHash);
        }
        public override void TriggerOnStateAnimationEnterEvent()
        {
            base.TriggerOnStateAnimationEnterEvent();
            TargetLocked();
        }
        
        public override void TriggerOnStateAnimationExitEvent()
        {
            base.TriggerOnStateAnimationExitEvent();
            StateMachine.ChangeState(StateMachine.StartState());
        }
        public override void Exit()
        {
            base.Exit();
            StopAnimation(Enemy.EnemyAnimationData.HeavyAttackParameterHash);
        }
    }
}