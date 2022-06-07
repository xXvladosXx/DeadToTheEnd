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
                Enemy.EnemyData.EnemyHeavyAttackData.AttackCooldown);
            
            StartAnimation(Enemy.EnemyAnimationData.HeavyAttackParameterHash);
        }
        
        public override void Exit()
        {
            base.Exit();
            StopAnimation(Enemy.EnemyAnimationData.HeavyAttackParameterHash);
        }
    }
}