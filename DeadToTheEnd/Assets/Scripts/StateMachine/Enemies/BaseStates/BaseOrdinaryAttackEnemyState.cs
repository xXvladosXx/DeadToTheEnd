namespace StateMachine.Enemies.BaseStates
{
    public class BaseOrdinaryAttackEnemyState : BaseAttackEnemyState
    {
        public BaseOrdinaryAttackEnemyState(StateMachine stateMachine) : base(stateMachine)
        {
        }
        
        public override void Enter()
        {
            base.Enter();
            
            Enemy.NavMeshAgent.speed = Enemy.EnemyData.EnemyOrdinaryAttackData.WalkSpeedModifer;
            StateMachine.StartCooldown(typeof(BaseOrdinaryAttackEnemyState),
                Enemy.EnemyData.EnemyOrdinaryAttackData.AttackCooldown);
        }

        public override void TriggerOnStateAnimationEnterEvent()
        {
            base.TriggerOnStateAnimationEnterEvent();
            
            TargetLocked();
            
            Enemy.NavMeshAgent.speed = Enemy.EnemyData.EnemyOrdinaryAttackData.WalkSpeedModifer;
            Enemy.NavMeshAgent.SetDestination(Enemy.Target.transform.position);
            Enemy.NavMeshAgent.isStopped = false;
        }

        public override void TriggerOnStateAnimationExitEvent()
        {
            base.TriggerOnStateAnimationExitEvent();

            StateMachine.ChangeState(StateMachine.StartState());
        }

    }
}