using Data.ScriptableObjects;
using StateMachine.WarriorEnemy.States.Movement;

namespace StateMachine.WarriorEnemy.States.Combat
{
    public class BaseAttackEnemyState : BaseMovementEnemyState, IState
    {
        public BaseAttackEnemyState(WarriorStateMachine warriorStateMachine) : base(warriorStateMachine)
        {
            
        }

        public override void Enter()
        {
            WarriorStateMachine.WarriorEnemy.EnemyStateReusableData.IsPerformingAction = true;
            WarriorStateMachine.WarriorEnemy.NavMeshAgent.isStopped = true;
            WarriorStateMachine.WarriorEnemy.NavMeshAgent.SetDestination(WarriorStateMachine.WarriorEnemy.MainPlayer
                .transform.position);
            
            TargetLocked();
        }

        public override void Exit()
        {
            Stop();
            WarriorStateMachine.WarriorEnemy.EnemyStateReusableData.IsPerformingAction = false;
        }


        public override void Update()
        {
           
        }
    }
}