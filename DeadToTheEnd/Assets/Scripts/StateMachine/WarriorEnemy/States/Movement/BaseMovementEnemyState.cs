using Data.Animations;
using Data.ScriptableObjects;
using UnityEngine;
using UnityEngine.AI;

namespace StateMachine.WarriorEnemy.States.Movement
{
    public class BaseMovementEnemyState : IState
    {
        protected WarriorEnemyAnimationData WarriorEnemyAnimationData;
        protected WarriorStateMachine WarriorStateMachine;
        protected WarriorEnemyData WarriorEnemyData;

        protected BaseMovementEnemyState(WarriorStateMachine warriorStateMachine)
        {
            WarriorStateMachine = warriorStateMachine;
            
            WarriorEnemyData = WarriorStateMachine.WarriorEnemy.WarriorEnemyData;
            WarriorEnemyAnimationData = WarriorStateMachine.WarriorEnemy.WarriorEnemyAnimationData;
        }
        
        public virtual void Enter()
        {
            
        }

        public virtual void Exit()
        {
            
        }

        public virtual void HandleInput()
        {
            
        }

        public virtual void Update()
        {
            float viewAngle = GetViewAngle(WarriorStateMachine.WarriorEnemy.transform,
                WarriorStateMachine.WarriorEnemy.MainPlayer.transform);

            if (viewAngle is > 45 or < -45)
            {
                WarriorStateMachine.WarriorEnemy.NavMeshAgent.isStopped = true;
                WarriorStateMachine.ChangeState(WarriorStateMachine.RotateTowardsEnemyState);
            }
        }

        public virtual void FixedUpdate()
        {
            
        }

        public virtual void OnAnimationEnterEvent()
        {
           
        }

        public virtual void OnAnimationExitEvent()
        {
            
        }   protected void MoveTo(NavMeshAgent enemy, Vector3 target, float speed = 1f)
        {
            enemy.destination = target;
            enemy.isStopped = false;
        }

        protected void Stop(NavMeshAgent enemy)
        {
            enemy.ResetPath();
            enemy.isStopped = true;
        }

        protected float GetViewAngle(Transform enemy, Transform target)
        {
            Vector3 targetDirection = target.position - enemy.position;

            float viewableAngle = Vector3.SignedAngle(targetDirection, enemy.forward, Vector3.up);
            return viewableAngle;
        }

        protected bool IsEnoughDistance(float distance, Transform enemy, Transform target) => 
            distance > Vector3.Distance(enemy.position, target.position);

        protected void MakeOrdinaryAttack()
        {
            if (IsEnoughDistance(WarriorEnemyData.EnemyFollowData.DistanceToStartOrdinaryAttack,
                    WarriorStateMachine.WarriorEnemy.transform,
                    WarriorStateMachine.WarriorEnemy.MainPlayer.transform))
            {
                WarriorStateMachine.ChangeState(WarriorStateMachine.LightAttackWarriorEnemyState);
            }
        }

        protected void MakeDashAttack()
        {
            if (IsEnoughDistance(WarriorEnemyData.EnemyFollowData.DistanceToStartDashAttack,
                    WarriorStateMachine.WarriorEnemy.transform,
                    WarriorStateMachine.WarriorEnemy.MainPlayer.transform) &&
                !IsEnoughDistance(WarriorEnemyData.EnemyFollowData.DistanceToStartOrdinaryAttack,
                    WarriorStateMachine.WarriorEnemy.transform,
                    WarriorStateMachine.WarriorEnemy.MainPlayer.transform))
            {
                WarriorStateMachine.ChangeState(WarriorStateMachine.DashAttackWarriorEnemyState);
            }
        }
    }
}