namespace StateMachine.Enemies.GoblinEnemy.States.Combat
{
    public class BaseCombatGoblinEnemyState : BaseGoblinEnemyState
    {
        protected BaseCombatGoblinEnemyState(GoblinStateMachine stateMachine) : base(stateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();
            
            GoblinEnemy.NavMeshAgent.SetDestination(GoblinEnemy.Target.transform.position);
            GoblinEnemy.NavMeshAgent.isStopped = true;
            GoblinEnemy.GoblinStateReusableData.IsBlocking = false;
            
            StartAnimation(GoblinEnemyAnimationData.AttackParameterHash);
        }

        public override void Exit()
        {
            base.Exit();
            
            GoblinEnemy.NavMeshAgent.ResetPath();
            GoblinEnemy.NavMeshAgent.isStopped = true;
            
            StopAnimation(GoblinEnemyAnimationData.AttackParameterHash);
        }
    }
}