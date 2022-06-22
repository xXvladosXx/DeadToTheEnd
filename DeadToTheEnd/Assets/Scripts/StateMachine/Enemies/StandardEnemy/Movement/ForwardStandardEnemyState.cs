using UnityEngine;

namespace StateMachine.Enemies.BlueGragon.Movement
{
    public class ForwardStandardEnemyState : StandardEnemyState
    {
        public ForwardStandardEnemyState(StandardEnemyStateMachine stateMachine) : base(stateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();
            StandardEnemy.NavMeshAgent.speed = StandardEnemyData.EnemyWalkData.WalkSpeedModifer;
        }

        public override void Exit()
        {
            base.Exit();
            StandardEnemy.Animator.SetFloat(BlueDragonAnimationData.VerticalParameterHash, 0);
        }

        public override void Update()
        {
            base.Update();
            
            HandleMoveToTarget();
            DecideAttackToDo();
        }
    }
}