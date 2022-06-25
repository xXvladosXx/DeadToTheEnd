using System;
using System.Linq;
using CameraManage;
using Data.Animations;
using Data.Combat;
using Data.ScriptableObjects;
using Data.States.StateData;
using Entities;
using Entities.Enemies;
using StateMachine.Core;
using StateMachine.Enemies.WarriorEnemy;
using StateMachine.WarriorEnemy.States.Combat;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace StateMachine.WarriorEnemy.States.Movement
{
    public abstract class BaseEnemyState : IState
    {
        protected readonly Enemy Enemy;
        protected readonly EnemyData EnemyData;
        
        protected readonly EntityStateMachine StateMachine;
        protected Func<bool>[] CanAttackFunctions;

        protected BaseEnemyState(EntityStateMachine stateMachine)
        {
            Enemy = stateMachine.AliveEntity as Enemy;
            EnemyData = Enemy.EntityData as EnemyData;

            StateMachine = stateMachine;
            
        }

        public virtual void Enter()
        {
            Stop();
            AddEventCallbacks();
        }

        public virtual void Exit()
        {
            Stop();
            RemoveEventCallbacks();
        }

        public virtual void Update()
        {
            float viewAngle = GetViewAngle(Enemy.transform,
                Enemy.Target.transform);

            if (viewAngle is > 45 or < -45)
            {
                Rotate();
            }
        }

        public virtual void FixedUpdate()
        {
        }

        public virtual void TriggerOnStateAnimationEnterEvent()
        {
        }

        public virtual void TriggerOnStateAnimationExitEvent()
        {
        }

        public virtual void TriggerOnStateAnimationHandleEvent()
        {
        }

        public void HandleInput()
        {
            
        }
        
        protected virtual void AddEventCallbacks()
        {
            Enemy.AttackCalculator.OnDamageTaken += HealthOnAttackApplied;
            Enemy.Health.OnDied += OnDied;
        }

        

        protected virtual void RemoveEventCallbacks()
        {
            Enemy.AttackCalculator.OnDamageTaken -= HealthOnAttackApplied;
        }
        
        protected virtual void HealthOnAttackApplied(AttackData obj)
        {
        }
        protected virtual void OnDied()
        {
            
        }
        protected virtual void Rotate()
        {
            
        }

        protected void StartAnimation(int animation)
        {
            Enemy.Animator.SetBool(animation, true);
        }

        protected void StopAnimation(int animation)
        {
            Enemy.Animator.SetBool(animation, false);
        }
      
        protected void Stop()
        {
            Enemy.NavMeshAgent.speed = 0;
            Enemy.NavMeshAgent.ResetPath();
            Enemy.NavMeshAgent.isStopped = true;
        }

        protected float GetViewAngle(Transform user, Transform target)
        {
            Vector3 targetDirection = target.position - user.position;

            float viewableAngle = Vector3.SignedAngle(targetDirection, user.forward, Vector3.up);
            return viewableAngle;
        }

        protected bool IsEnoughDistance(float distance, Transform user, Transform target) =>
            distance > Vector3.Distance(user.position, target.position);


        protected void TargetLocked()
        {
            Transform transform;
            (transform = Enemy.transform).LookAt(Enemy.Target.transform);
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
        }

        protected void HandleMoveToTarget()
        {
           
            if (IsEnoughDistance(EnemyData.EnemyWalkData.StoppingDistance,
                    Enemy.transform,
                    Enemy.Target.transform))
            {
                Stop();
                if(Enemy.Animator.GetFloat(Enemy.EnemyAnimationData.VerticalParameterHash) <= .01)
                    StateMachine.ChangeState(StateMachine.StartState());
                
                Enemy.Animator.SetFloat(Enemy.EnemyAnimationData.VerticalParameterHash, 0, .1f, Time.deltaTime);
                return;
            }
            
            if (!IsEnoughDistance(EnemyData.EnemyWalkData.StoppingDistance,
                    Enemy.transform,
                    Enemy.Target.transform))
            {
                Enemy.Animator.SetFloat(Enemy.EnemyAnimationData.VerticalParameterHash, 1, .1f, Time.deltaTime);
            }

            HandleRotateTowardsTarget();
        }
        
        protected float DecideTime(float minTime, float maxTime) => Random.Range(minTime, maxTime);

        protected virtual void DecideAttackToDo()
        {
            while (true)
            {
                var any = CanAttackFunctions.Any(f => f());

                if (!any)
                {
                    break;
                }
            }
        }
        
        private void HandleRotateTowardsTarget()
        {
            if (Enemy.Reusable.IsPerformingAction)
            {
                Vector3 direction = Enemy.Target.transform.position -
                                    Enemy.transform.position;

                direction.y = 0;
                direction.Normalize();

                if (direction == Vector3.zero)
                {
                    direction = Enemy.transform.forward;
                }

                Quaternion targetRotation = Quaternion.LookRotation(direction);
                Enemy.transform.rotation = Quaternion.Slerp(
                    Enemy.transform.rotation, targetRotation,
                    EnemyData.EnemyIdleData.RotationSpeedModifer / Time.deltaTime);
            }
            else
            {
                Vector3 targetVelocity = Enemy.Rigidbody.velocity;

                Enemy.NavMeshAgent.isStopped = false;
                Enemy.NavMeshAgent.SetDestination(Enemy
                    .Target.transform.position);
                Enemy.Rigidbody.velocity = targetVelocity;
            }
        }
    }
}