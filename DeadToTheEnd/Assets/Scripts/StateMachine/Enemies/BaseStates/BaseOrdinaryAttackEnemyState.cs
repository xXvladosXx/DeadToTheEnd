using StateMachine.Core;

namespace StateMachine.Enemies.BaseStates
{
    public class BaseOrdinaryAttackEnemyState : BaseAttackEnemyState
    {
        public BaseOrdinaryAttackEnemyState(EntityStateMachine stateMachine) : base(stateMachine)
        {
        }
        
        public override void Enter()
        {
            base.Enter();
            
            Enemy.NavMeshAgent.speed = EnemyData.EnemyOrdinaryAttackData.WalkSpeedModifer;
            StateMachine.StartCooldown(typeof(BaseOrdinaryAttackEnemyState),
                EnemyData.EnemyOrdinaryAttackData.AttackCooldown);
        }

        public override void TriggerOnStateAnimationEnterEvent()
        {
            base.TriggerOnStateAnimationEnterEvent();
            
            TargetLocked();
            
            Enemy.NavMeshAgent.speed = EnemyData.EnemyOrdinaryAttackData.WalkSpeedModifer;
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