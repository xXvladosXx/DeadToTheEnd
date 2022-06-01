using CameraManage;
using Data.Animations;
using Data.Combat;
using Data.ScriptableObjects;
using Entities;
using Entities.Enemies;
using StateMachine.Enemies.WarriorEnemy;
using StateMachine.WarriorEnemy.States.Combat;
using UnityEngine;
using UnityEngine.AI;

namespace StateMachine.WarriorEnemy.States.Movement
{
    public class BaseEnemyState : IState
    {
        protected readonly Enemy Enemy; 
        
        protected BaseEnemyState(StateMachine stateMachine)
        {
            Enemy = stateMachine.AliveEntity as Enemy;
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

        protected virtual void AddEventCallbacks()
        {
        }
        protected virtual void RemoveEventCallbacks()
        {
        }

        public virtual void Update()
        {
            
        }

        public virtual void FixedUpdate()
        {
        }

        public virtual void OnAnimationEnterEvent()
        {
        }

        public virtual void OnAnimationExitEvent()
        {
        }

        public virtual void OnAnimationHandleEvent()
        {
        }

        public void HandleInput()
        {
            
        }

        protected void MoveTo(NavMeshAgent user, Vector3 target, float speed = 1f)
        {
            user.destination = target;
            user.isStopped = false;
        }

        protected void Stop()
        {
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
            (transform = Enemy.transform).LookAt(Enemy.MainPlayer.transform);
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
        }

        
        protected float DecideTimeOfMoving(float minTime, float maxTime) => Random.Range(minTime, maxTime);

        
    }
}