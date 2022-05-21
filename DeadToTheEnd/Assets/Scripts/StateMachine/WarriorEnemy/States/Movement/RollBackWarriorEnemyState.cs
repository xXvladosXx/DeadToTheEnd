using UnityEngine;

namespace StateMachine.WarriorEnemy.States.Movement
{
    public class RollBackWarriorEnemyState : BaseMovementEnemyState
    {
        private Vector3 direction;
        public RollBackWarriorEnemyState(WarriorStateMachine warriorStateMachine) : base(warriorStateMachine)
        {
        }

        public override void Enter()
        {
            Stop();
            WarriorStateMachine.WarriorEnemy.EnemyStateReusableData.CanRoll = false;
            WarriorStateMachine.WarriorEnemy.Animator.SetBool(WarriorEnemyAnimationData.RollParameterHash, true);

            WarriorStateMachine.WarriorEnemy.NavMeshAgent.updateRotation = false;
            WarriorStateMachine.WarriorEnemy.NavMeshAgent.speed = WarriorEnemyData.EnemyRollData.RollSpeedModifer;
        }

        public override void Update()
        {
            WarriorStateMachine.WarriorEnemy.NavMeshAgent.speed -= Time.deltaTime*5;
            Vector3 direction = WarriorStateMachine.WarriorEnemy.transform.position -
                                WarriorStateMachine.WarriorEnemy.MainPlayer.transform.position; 
            
            WarriorStateMachine.WarriorEnemy.transform.LookAt(WarriorStateMachine.WarriorEnemy.MainPlayer.transform.position);
            WarriorStateMachine.WarriorEnemy.NavMeshAgent.SetDestination(WarriorStateMachine.WarriorEnemy.transform.position + direction);
        }

        public override void Exit()
        {
            base.Exit();
            WarriorStateMachine.WarriorEnemy.Animator.SetBool(WarriorEnemyAnimationData.RollParameterHash, false);
        }

        public override void OnAnimationEnterEvent()
        {
            WarriorStateMachine.WarriorEnemy.NavMeshAgent.isStopped = false;
        }

        public override void OnAnimationExitEvent()
        {
            WarriorStateMachine.WarriorEnemy.Animator.SetBool(WarriorEnemyAnimationData.RollParameterHash, false);
            WarriorStateMachine.ChangeState(WarriorStateMachine.FollowWarriorEnemyState);
        }
      
    }
}