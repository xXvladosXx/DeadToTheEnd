using Data.ScriptableObjects;
using UnityEngine;

namespace StateMachine.WarriorEnemy.States.Movement
{
    public class ForwardMoveWarriorEnemyState : BaseMovementEnemyState, IState
    {
        private float _curTime;
        private float _timeToMoveForward;
        
        public ForwardMoveWarriorEnemyState(WarriorStateMachine warriorStateMachine) : base(warriorStateMachine)
        {
           
        }

        public override void Enter()
        {
            WarriorStateMachine.WarriorEnemy.NavMeshAgent.isStopped = false;
            WarriorStateMachine.WarriorEnemy.NavMeshAgent.speed = WarriorEnemyData.EnemyWalkData.WalkSpeedModifer;
            _timeToMoveForward = DecideTimeOfMoving(WarriorEnemyData.EnemyWalkData.WalkMinTime, WarriorEnemyData.EnemyWalkData.WalkMaxTime);
        }

        public override void Exit()
        {
            base.Exit();

            WarriorStateMachine.WarriorEnemy.Animator.SetFloat(WarriorEnemyAnimationData.VerticalParameterHash, 0);
        }

        public override void Update()
        {
            if(WarriorStateMachine.WarriorEnemy.EnemyStateReusableData.IsPerformingAction) return;
            _curTime += Time.deltaTime;
            base.Update();
            if(_curTime > _timeToMoveForward)
                DecideAttackToDo();
            
            HandleMoveToTarget();
        }
        
        private void HandleMoveToTarget()
        {
            if (IsEnoughDistance(WarriorEnemyData.EnemyAttackData.DistanceToStartOrdinaryAttack,
                    WarriorStateMachine.WarriorEnemy.transform,
                    WarriorStateMachine.WarriorEnemy.MainPlayer.transform))
            {
                DecideAttackToDo();
                WarriorStateMachine.WarriorEnemy.Animator.SetFloat(WarriorEnemyAnimationData.VerticalParameterHash, 0, .1f, Time.deltaTime);
                return;
            }
            else if (!IsEnoughDistance(WarriorEnemyData.EnemyAttackData.DistanceToStartOrdinaryAttack,
                         WarriorStateMachine.WarriorEnemy.transform,
                         WarriorStateMachine.WarriorEnemy.MainPlayer.transform))
            {
                WarriorStateMachine.WarriorEnemy.Animator.SetFloat(WarriorEnemyAnimationData.VerticalParameterHash, 1, .1f, Time.deltaTime);
            }

            HandleRotateTowardsTarget();
            TargetLocked();
        }

        private void HandleRotateTowardsTarget()
        {
            if (WarriorStateMachine.WarriorEnemy.EnemyStateReusableData.IsPerformingAction)
            {
                Vector3 direction = WarriorStateMachine.WarriorEnemy.MainPlayer.transform.position -
                                    WarriorStateMachine.WarriorEnemy.transform.position;

                direction.y = 0;
                direction.Normalize();

                if (direction == Vector3.zero)
                {
                    direction = WarriorStateMachine.WarriorEnemy.transform.forward;
                }

                Quaternion targetRotation = Quaternion.LookRotation(direction);
                WarriorStateMachine.WarriorEnemy.transform.rotation = Quaternion.Slerp(
                    WarriorStateMachine.WarriorEnemy.transform.rotation, targetRotation,
                    WarriorEnemyData.EnemyIdleData.RotationSpeedModifer / Time.deltaTime);
            }
            else
            {
                Vector3 targetVelocity = WarriorStateMachine.WarriorEnemy.Rigidbody.velocity;

                WarriorStateMachine.WarriorEnemy.NavMeshAgent.enabled = true;
                WarriorStateMachine.WarriorEnemy.NavMeshAgent.SetDestination(WarriorStateMachine.WarriorEnemy
                    .MainPlayer.transform.position);
                WarriorStateMachine.WarriorEnemy.Rigidbody.velocity = targetVelocity;

                WarriorStateMachine.WarriorEnemy.transform.rotation = Quaternion.Slerp(
                    WarriorStateMachine.WarriorEnemy.transform.rotation,
                    WarriorStateMachine.WarriorEnemy.NavMeshAgent.transform.rotation,
                    WarriorEnemyData.EnemyIdleData.RotationSpeedModifer / Time.deltaTime);
            }
        }
    }
}