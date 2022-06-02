using StateMachine.Enemies.WarriorEnemy;
using UnityEngine;

namespace StateMachine.WarriorEnemy.States.Movement
{
    public class RollBackWarriorEnemyState : BaseWarriorEnemyState
    {
        private Vector3 direction;
        public RollBackWarriorEnemyState(WarriorStateMachine warriorStateMachine) : base(warriorStateMachine)
        {
        }

        public override void Enter()
        {
            Stop();
            BossEnemy.EnemyStateReusableData.CanRoll = false;
            BossEnemy.Animator.SetBool(WarriorEnemyAnimationData.RollParameterHash, true);

            BossEnemy.NavMeshAgent.updateRotation = false;
            BossEnemy.NavMeshAgent.speed = bossEnemyData.EnemyRollData.RollSpeedModifer;
        }

        public override void Update()
        {
            BossEnemy.NavMeshAgent.speed -= Time.deltaTime*5;
            Vector3 direction = BossEnemy.transform.position -
                                BossEnemy.Target.transform.position; 
            
            BossEnemy.transform.LookAt(BossEnemy.Target.transform.position);
            BossEnemy.NavMeshAgent.SetDestination(BossEnemy.transform.position + direction);
        }

        public override void Exit()
        {
            base.Exit();
            BossEnemy.Animator.SetBool(WarriorEnemyAnimationData.RollParameterHash, false);
        }

        public override void OnAnimationEnterEvent()
        {
            BossEnemy.NavMeshAgent.isStopped = false;
        }

        public override void OnAnimationExitEvent()
        {
            BossEnemy.Animator.SetBool(WarriorEnemyAnimationData.RollParameterHash, false);
            WarriorStateMachine.ChangeState(WarriorStateMachine.FollowWarriorEnemyState);
        }
      
    }
}