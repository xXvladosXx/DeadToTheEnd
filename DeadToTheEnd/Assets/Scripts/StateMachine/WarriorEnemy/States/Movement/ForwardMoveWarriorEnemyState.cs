using Data.ScriptableObjects;
using UnityEngine;

namespace StateMachine.WarriorEnemy.States.Movement
{
    public class ForwardMoveWarriorEnemyState : BaseMovementEnemyState, IState
    {

        public ForwardMoveWarriorEnemyState(WarriorStateMachine warriorStateMachine) : base(warriorStateMachine)
        {
           
        }

        public override void Enter()
        {
            WarriorStateMachine.WarriorEnemy.NavMeshAgent.isStopped = false;
        }

        public override void Exit()
        {
            WarriorStateMachine.WarriorEnemy.Animator.SetFloat(WarriorEnemyAnimationData.VerticalParameterHash, 0);
        }

        public override void HandleInput()
        {
        }

        public override void Update()
        {
            base.Update();
            HandleMoveToTarget();
            CheckCombatConditions();
        }

        private void CheckCombatConditions()
        {
        }

        public override void FixedUpdate()
        {
        }

        public override void OnAnimationEnterEvent()
        {
        }

        public override void OnAnimationExitEvent()
        {
        }

        private void HandleMoveToTarget()
        {
            WarriorStateMachine.ChangeState(WarriorStateMachine.RollBackWarriorEnemyState);
            
            if (IsEnoughDistance(3,
                    WarriorStateMachine.WarriorEnemy.transform,
                    WarriorStateMachine.WarriorEnemy.MainPlayer.transform))
            {
                WarriorStateMachine.WarriorEnemy.Animator.SetFloat(WarriorEnemyAnimationData.VerticalParameterHash, 0, .1f, Time.deltaTime);
                WarriorStateMachine.WarriorEnemy.NavMeshAgent.isStopped = true;
                //WarriorStateMachine.ChangeState(WarriorStateMachine.FollowWarriorEnemyState);
            }
            else if (!IsEnoughDistance(3,
                         WarriorStateMachine.WarriorEnemy.transform,
                         WarriorStateMachine.WarriorEnemy.MainPlayer.transform))
            {
                WarriorStateMachine.WarriorEnemy.Animator.SetFloat(WarriorEnemyAnimationData.VerticalParameterHash, 1, .1f, Time.deltaTime);
            }

            HandleRotateTowardsTarget();
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