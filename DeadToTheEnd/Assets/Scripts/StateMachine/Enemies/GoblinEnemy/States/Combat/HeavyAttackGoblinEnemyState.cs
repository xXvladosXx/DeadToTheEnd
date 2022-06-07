using StateMachine.Enemies.BaseStates;

namespace StateMachine.Enemies.GoblinEnemy.States.Combat
{
    public class HeavyAttackGoblinEnemyState : BaseHeavyAttackEnemyState
    {
        public HeavyAttackGoblinEnemyState(GoblinStateMachine stateMachine) : base(stateMachine)
        {
        }
        
        public override void OnAnimationEnterEvent()
        {
            base.OnAnimationEnterEvent();
            TargetLocked();
            Enemy.NavMeshAgent.SetDestination(Enemy.Target.transform.position);
            Enemy.NavMeshAgent.speed = Enemy.EnemyData.EnemyHeavyAttackData.WalkSpeedModifer;

            Enemy.NavMeshAgent.isStopped = false;
        }

        public override void OnAnimationExitEvent()
        {
            base.OnAnimationExitEvent();
            StateMachine.ChangeState(StateMachine.StartState());
        }
    }
}