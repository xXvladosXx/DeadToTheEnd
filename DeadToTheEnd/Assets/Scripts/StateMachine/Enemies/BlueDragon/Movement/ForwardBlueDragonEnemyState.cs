using UnityEngine;

namespace StateMachine.Enemies.BlueGragon.Movement
{
    public class ForwardBlueDragonEnemyState : BaseBlueDragonEnemyState
    {
        public ForwardBlueDragonEnemyState(BlueDragonStateMachine stateMachine) : base(stateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();
            BlueDragonEnemy.NavMeshAgent.speed = BlueDragonEnemyData.EnemyWalkData.WalkSpeedModifer;
        }

        public override void Exit()
        {
            base.Exit();
            BlueDragonEnemy.Animator.SetFloat(BlueDragonAnimationData.VerticalParameterHash, 0);
        }

        public override void Update()
        {
            base.Update();
            
            HandleMoveToTarget();
            DecideAttackToDo();
        }
    }
}