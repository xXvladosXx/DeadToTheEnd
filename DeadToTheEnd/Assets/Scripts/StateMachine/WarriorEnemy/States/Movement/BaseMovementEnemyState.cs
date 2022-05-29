using CameraManage;
using Data.Animations;
using Data.ScriptableObjects;
using StateMachine.WarriorEnemy.States.Combat;
using UnityEngine;
using UnityEngine.AI;

namespace StateMachine.WarriorEnemy.States.Movement
{
    public class BaseMovementEnemyState : IState
    {
        protected WarriorEnemyAnimationData WarriorEnemyAnimationData;
        protected WarriorStateMachine WarriorStateMachine;
        protected WarriorEnemyData WarriorEnemyData;

        private float _curTime;
        private float _timeToWait;

        protected BaseMovementEnemyState(WarriorStateMachine warriorStateMachine)
        {
            WarriorStateMachine = warriorStateMachine;

            WarriorEnemyData = WarriorStateMachine.WarriorEnemy.WarriorEnemyData;
            WarriorEnemyAnimationData = WarriorStateMachine.WarriorEnemy.WarriorEnemyAnimationData;
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
            _curTime = 0;
        }

        protected void AddEventCallbacks()
        {
            WarriorStateMachine.WarriorEnemy.Health.OnDamageTaken += HealthOnOnAttackApplied;
        }
        protected void RemoveEventCallbacks()
        {
            WarriorStateMachine.WarriorEnemy.Health.OnDamageTaken -= HealthOnOnAttackApplied;
        }
        private void HealthOnOnAttackApplied()
        {
            CinemachineShake.Instance.ShakeCamera(.3f, .3f);
        }

        public virtual void Update()
        {
            _curTime += Time.deltaTime;
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
        }

        public virtual void OnAnimationHandleEvent()
        {
        }

        public void HandleInput()
        {
            
        }

        protected void MoveTo(NavMeshAgent enemy, Vector3 target, float speed = 1f)
        {
            enemy.destination = target;
            enemy.isStopped = false;
        }

        protected void Stop()
        {
            WarriorStateMachine.WarriorEnemy.NavMeshAgent.ResetPath();
            WarriorStateMachine.WarriorEnemy.NavMeshAgent.isStopped = true;
        }

        protected float GetViewAngle(Transform enemy, Transform target)
        {
            Vector3 targetDirection = target.position - enemy.position;

            float viewableAngle = Vector3.SignedAngle(targetDirection, enemy.forward, Vector3.up);
            return viewableAngle;
        }

        protected bool IsEnoughDistance(float distance, Transform enemy, Transform target) =>
            distance > Vector3.Distance(enemy.position, target.position);


        protected void TargetLocked()
        {
            Transform transform;
            (transform = WarriorStateMachine.WarriorEnemy.transform).LookAt(WarriorStateMachine.WarriorEnemy.MainPlayer
                .transform);
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
        }

        protected bool MakeOrdinaryAttack()
        {
            if (IsEnoughDistance(WarriorEnemyData.EnemyAttackData.DistanceToStartOrdinaryAttack,
                    WarriorStateMachine.WarriorEnemy.transform,
                    WarriorStateMachine.WarriorEnemy.MainPlayer.transform))
            {
                WarriorStateMachine.ChangeState(WarriorStateMachine.LightAttackWarriorEnemyState);
                return true;
            }

            return false;
        }

        protected float DecideTimeOfMoving(float minTime, float maxTime) => Random.Range(maxTime, maxTime);

        protected bool MakeDashAttack()
        {
            if (IsEnoughDistance(WarriorEnemyData.EnemyAttackData.DistanceToStartDashAttack,
                    WarriorStateMachine.WarriorEnemy.transform,
                    WarriorStateMachine.WarriorEnemy.MainPlayer.transform) &&
                !IsEnoughDistance(WarriorEnemyData.EnemyAttackData.DistanceToStartOrdinaryAttack,
                    WarriorStateMachine.WarriorEnemy.transform,
                    WarriorStateMachine.WarriorEnemy.MainPlayer.transform) &&
                !WarriorStateMachine.StatesCooldown.ContainsKey(typeof(DashFirstAttackWarriorEnemyState)))
            {
                WarriorStateMachine.ChangeState(WarriorStateMachine.DashFirstAttackWarriorEnemyState);
                return true;
            }

            return false;
        }

        protected bool MakeSecondDashAttack()
        {
            if (IsEnoughDistance(WarriorEnemyData.EnemyAttackData.DistanceToStartSecondDashAttack,
                    WarriorStateMachine.WarriorEnemy.transform,
                    WarriorStateMachine.WarriorEnemy.MainPlayer.transform) &&
                !IsEnoughDistance(WarriorEnemyData.EnemyAttackData.DistanceToStartOrdinaryAttack,
                    WarriorStateMachine.WarriorEnemy.transform,
                    WarriorStateMachine.WarriorEnemy.MainPlayer.transform) &&
                !WarriorStateMachine.StatesCooldown.ContainsKey(typeof(DashSecondAttackWarriorEnemy)))
            {
                WarriorStateMachine.ChangeState(WarriorStateMachine.DashSecondAttackWarriorEnemy);
                return true;
            }

            return false;
        }

        protected bool MakeComboSecondAttack()
        {
            if (IsEnoughDistance(WarriorEnemyData.EnemyAttackData.DistanceToStartComboSecondAttack,
                    WarriorStateMachine.WarriorEnemy.transform,
                    WarriorStateMachine.WarriorEnemy.MainPlayer.transform) &&
                !WarriorStateMachine.StatesCooldown.ContainsKey(typeof(ComboSecondWarriorEnemyState))
                && !WarriorStateMachine.WarriorEnemy.EnemyStateReusableData.IsPerformingAction)
            {
                WarriorStateMachine.ChangeState(WarriorStateMachine.ComboSecondWarriorEnemyState);
                return true;
            }

            return false;
        }
        
        protected bool MakeComboFirstAttack()
        {
            if (IsEnoughDistance(WarriorEnemyData.EnemyAttackData.DistanceToStartComboFirstAttack,
                    WarriorStateMachine.WarriorEnemy.transform,
                    WarriorStateMachine.WarriorEnemy.MainPlayer.transform) &&
                !WarriorStateMachine.StatesCooldown.ContainsKey(typeof(ComboFirstWarriorEnemyState))
                && !WarriorStateMachine.WarriorEnemy.EnemyStateReusableData.IsPerformingAction)
            {
                WarriorStateMachine.ChangeState(WarriorStateMachine.ComboFirstWarriorEnemyState);
                return true;
            }

            return false;
        }
        
        protected void DecideAttackToDo()
        {
            var result = false;
            while (true)
            {
                int attack = Random.Range(0, 6);
                switch (attack)
                {
                    case 0:
                        result = MakeOrdinaryAttack();
                        if (result)
                            break;

                        continue;
                    case 1:
                        result = MakeDashAttack();
                        if (result)
                            break;
                        
                        continue;
                    case 2:
                        result = MakeComboFirstAttack();
                        if (result)
                            break;
                        
                        continue;
                    case 3:
                        result = MakeSecondDashAttack();
                        if (result)
                            break;
                        
                        continue;
                    case 4:
                        result = MakeComboSecondAttack();
                        if (result)
                            break;
                        
                        continue;
                    case 5:
                        break;
                }
                
                break;
            }
        }
    }
}