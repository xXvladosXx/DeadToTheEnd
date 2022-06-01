using Data.ScriptableObjects;
using StateMachine.Enemies.WarriorEnemy;
using UnityEngine;

namespace StateMachine.WarriorEnemy.States.Movement
{
    public class ForwardMoveWarriorEnemyState : BaseWarriorEnemyState, IState
    {
        private float _curTime;
        private float _timeToMoveForward;
        
        public ForwardMoveWarriorEnemyState(WarriorStateMachine warriorStateMachine) : base(warriorStateMachine)
        {
           
        }

        public override void Enter()
        {
            BossEnemy.NavMeshAgent.isStopped = false;
            BossEnemy.NavMeshAgent.speed = bossEnemyData.EnemyWalkData.WalkSpeedModifer;
            _timeToMoveForward = DecideTimeOfMoving(bossEnemyData.EnemyWalkData.WalkMinTime, bossEnemyData.EnemyWalkData.WalkMaxTime);
        }

        public override void Exit()
        {
            base.Exit();

            BossEnemy.Animator.SetFloat(WarriorEnemyAnimationData.VerticalParameterHash, 0);
        }

        public override void Update()
        {
            if(BossEnemy.EnemyStateReusableData.IsPerformingAction) return;
            _curTime += Time.deltaTime;
            base.Update();
            if(_curTime > _timeToMoveForward)
                DecideAttackToDo();
            
            HandleMoveToTarget();
        }
        
        private void HandleMoveToTarget()
        {
            if (IsEnoughDistance(bossEnemyData.EnemyAttackData.DistanceToStartOrdinaryAttack,
                    BossEnemy.transform,
                    BossEnemy.MainPlayer.transform))
            {
                DecideAttackToDo();
                BossEnemy.Animator.SetFloat(WarriorEnemyAnimationData.VerticalParameterHash, 0, .1f, Time.deltaTime);
                return;
            }
            
            if (!IsEnoughDistance(bossEnemyData.EnemyAttackData.DistanceToStartOrdinaryAttack,
                         BossEnemy.transform,
                         BossEnemy.MainPlayer.transform))
            {
                BossEnemy.Animator.SetFloat(WarriorEnemyAnimationData.VerticalParameterHash, 1, .1f, Time.deltaTime);
            }

            HandleRotateTowardsTarget();
            TargetLocked();
        }

        private void HandleRotateTowardsTarget()
        {
            if (BossEnemy.EnemyStateReusableData.IsPerformingAction)
            {
                Vector3 direction = BossEnemy.MainPlayer.transform.position -
                                    BossEnemy.transform.position;

                direction.y = 0;
                direction.Normalize();

                if (direction == Vector3.zero)
                {
                    direction = WarriorStateMachine.AliveEntity.transform.forward;
                }

                Quaternion targetRotation = Quaternion.LookRotation(direction);
                WarriorStateMachine.AliveEntity.transform.rotation = Quaternion.Slerp(
                    WarriorStateMachine.AliveEntity.transform.rotation, targetRotation,
                    bossEnemyData.EnemyIdleData.RotationSpeedModifer / Time.deltaTime);
            }
            else
            {
                Vector3 targetVelocity = BossEnemy.Rigidbody.velocity;

                BossEnemy.NavMeshAgent.enabled = true;
                BossEnemy.NavMeshAgent.SetDestination(BossEnemy
                    .MainPlayer.transform.position);
                BossEnemy.Rigidbody.velocity = targetVelocity;

                WarriorStateMachine.AliveEntity.transform.rotation = Quaternion.Slerp(
                    BossEnemy.transform.rotation,
                    BossEnemy.NavMeshAgent.transform.rotation,
                    bossEnemyData.EnemyIdleData.RotationSpeedModifer / Time.deltaTime);
            }
        }
    }
}