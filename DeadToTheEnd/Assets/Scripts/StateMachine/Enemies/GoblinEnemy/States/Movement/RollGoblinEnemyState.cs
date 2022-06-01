using UnityEngine;

namespace StateMachine.Enemies.GoblinEnemy.States.Movement
{
    public class RollGoblinEnemyState : BaseGoblinEnemyState
    {
        private Vector3 direction;
        public RollGoblinEnemyState(GoblinStateMachine goblinStateMachine) : base(goblinStateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();
            GoblinEnemy.EnemyStateReusableData.CanRoll = false;
            GoblinEnemy.Animator.SetBool(GoblinEnemyAnimationData.RollParameterHash, true);

            GoblinEnemy.NavMeshAgent.updateRotation = false;
            GoblinEnemy.NavMeshAgent.speed = GoblinEnemy.GoblinEnemyData.EnemyRollData.RollSpeedModifer;
        }

        public override void Update()
        {
            GoblinEnemy.NavMeshAgent.speed -= Time.deltaTime*5;
            Vector3 direction = GoblinEnemy.transform.position -
                                GoblinEnemy.MainPlayer.transform.position; 
            
            GoblinEnemy.transform.LookAt(GoblinEnemy.MainPlayer.transform.position);
            GoblinEnemy.NavMeshAgent.SetDestination(GoblinEnemy.transform.position + direction);
        }

        public override void Exit()
        {
            base.Exit();
            GoblinEnemy.Animator.SetBool(GoblinEnemyAnimationData.RollParameterHash, false);
        }

        public override void OnAnimationEnterEvent()
        {
            Debug.Log("enter");
            GoblinEnemy.NavMeshAgent.isStopped = false;
        }

        public override void OnAnimationExitEvent()
        {
            GoblinEnemy.Animator.SetBool(GoblinEnemyAnimationData.RollParameterHash, false);
            GoblinStateMachine.ChangeState(GoblinStateMachine.FollowGoblinEnemyState);
        }
    }
}