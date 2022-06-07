using UnityEngine;

namespace StateMachine.Enemies.GoblinEnemy.States.Movement
{
    public class ForwardMoveGoblinEnemyState : BaseGoblinEnemyState
    {

        public ForwardMoveGoblinEnemyState(GoblinStateMachine stateMachine) : base(stateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();
            
            GoblinEnemy.NavMeshAgent.isStopped = false;
            GoblinEnemy.NavMeshAgent.speed = GoblinEnemy.GoblinEnemyData.GoblinForwardMoveData.WalkSpeedModifer;
        }

        public override void Exit()
        {
            base.Exit();
            GoblinEnemy.Animator.SetFloat(GoblinEnemyAnimationData.VerticalParameterHash, 0);
        }

        public override void Update()
        {
            base.Update();
            
            DecideAttackToDo();
            HandleMoveToTarget();
        }
    }
}