using StateMachine.Core;
using StateMachine.WarriorEnemy.States.Movement;

namespace StateMachine.Enemies.WarriorEnemy.States.Combat
{
    public class BaseAttackWarriorEnemyState : BaseWarriorEnemyState, ITimeable
    {
        public BaseAttackWarriorEnemyState(WarriorStateMachine warriorStateMachine) : base(warriorStateMachine)
        {
            
        }

        public override void Enter()
        {
            WarriorEnemy.WarriorStateReusableData.IsPerformingAction = true;
            WarriorEnemy.NavMeshAgent.isStopped = true;
            WarriorEnemy.NavMeshAgent.SetDestination(WarriorEnemy.Target.transform.position);
            
            TargetLocked();
        }

        public override void Exit()
        {
            Stop();
            WarriorEnemy.WarriorStateReusableData.IsPerformingAction = false;
        }


        public override void Update()
        {
           
        }
    }
}