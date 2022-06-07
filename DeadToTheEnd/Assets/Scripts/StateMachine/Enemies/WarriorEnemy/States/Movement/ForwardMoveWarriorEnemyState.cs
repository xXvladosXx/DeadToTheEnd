using StateMachine.Enemies.WarriorEnemy;

namespace StateMachine.WarriorEnemy.States.Movement
{
    public class ForwardMoveWarriorEnemyState : BaseWarriorEnemyState
    {
        public ForwardMoveWarriorEnemyState(WarriorStateMachine warriorStateMachine) : base(warriorStateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();
            
            WarriorEnemy.NavMeshAgent.isStopped = false;
            WarriorEnemy.NavMeshAgent.speed = Enemy.EnemyData.EnemyWalkData.WalkSpeedModifer;
        }

        public override void Update()
        {
            base.Update();
            HandleMoveToTarget();
            
            DecideAttackToDo();
        }
    }
}