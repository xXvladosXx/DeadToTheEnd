using Data.ScriptableObjects;
using StateMachine.Enemies.WarriorEnemy;
using StateMachine.WarriorEnemy.States.Movement;

namespace StateMachine.WarriorEnemy.States.Combat
{
    public class BaseAttackEnemyState : BaseWarriorEnemyState, IState
    {
        public BaseAttackEnemyState(WarriorStateMachine warriorStateMachine) : base(warriorStateMachine)
        {
            
        }

        public override void Enter()
        {
            BossEnemy.EnemyStateReusableData.IsPerformingAction = true;
            BossEnemy.NavMeshAgent.isStopped = true;
            BossEnemy.NavMeshAgent.SetDestination(BossEnemy.MainPlayer.transform.position);
            
            TargetLocked();
        }

        public override void Exit()
        {
            Stop();
            BossEnemy.EnemyStateReusableData.IsPerformingAction = false;
        }


        public override void Update()
        {
           
        }
    }
}