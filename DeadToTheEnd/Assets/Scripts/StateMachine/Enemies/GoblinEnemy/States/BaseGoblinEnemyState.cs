using System;
using CameraManage;
using Data.Animations;
using Data.Combat;
using Data.ScriptableObjects;
using StateMachine.WarriorEnemy.States.Movement;
using UnityEngine;

namespace StateMachine.Enemies.GoblinEnemy.States
{
    public class BaseGoblinEnemyState : BaseEnemyState
    {
        protected GoblinEnemyAnimationData GoblinEnemyAnimationData;
        protected GoblinStateMachine GoblinStateMachine;
        protected GoblinEnemyData BossEnemyData;

        protected Entities.Enemies.GoblinEnemy GoblinEnemy;
        private float _curTime;
        private float _timeToWait;
        protected BaseGoblinEnemyState(GoblinStateMachine stateMachine) : base(stateMachine)
        {
            GoblinStateMachine = stateMachine;

            GoblinEnemy = stateMachine.AliveEntity as Entities.Enemies.GoblinEnemy;
            BossEnemyData = GoblinEnemy.GoblinEnemyData;
            GoblinEnemyAnimationData = GoblinEnemy.GoblinEnemyAnimationData;
        }

        public override void Enter()
        {
            base.Enter();
            GoblinEnemy.GoblinStateReusableData.IsBlocking = true;
            GoblinEnemy.DefenseColliderActivator.ActivateCollider();
        }

        public override void Exit()
        {
            base.Exit();
            _curTime = 0;
            
            GoblinEnemy.GoblinStateReusableData.IsBlocking = false;
            GoblinEnemy.DefenseColliderActivator.DeactivateCollider();
        }

        public override void Update()
        {
            base.Update();
            _curTime += Time.deltaTime;
            
            float viewableAngle = GetViewAngle(GoblinEnemy.transform,
                GoblinEnemy.Target.transform);

            switch (viewableAngle)
            {
                case >= 100 and <= 180 :
                    GoblinEnemy.Reusable.IsTargetBehind = true;
                    break;
                case <= -101 and >= -180:
                    GoblinEnemy.Reusable.IsTargetBehind = true;
                    break;
                case <= -45 and >= -100 :
                    GoblinEnemy.Reusable.IsTargetBehind = true;
                    break;
                case >= 45 and <= 100:
                    GoblinEnemy.Reusable.IsTargetBehind = true;
                    break;
                default:
                    GoblinEnemy.Reusable.IsTargetBehind = false;
                    Debug.Log("Front");

                    break;
            }
            /*if (viewAngle is > 45 or < -45)
            {
                GoblinEnemy.NavMeshAgent.isStopped = true;
                GoblinStateMachine.ChangeState(GoblinStateMachine.RotateGoblinEnemyState);
            }*/
        }
        
        protected override void AddEventCallbacks()
        {
            base.AddEventCallbacks();
            GoblinStateMachine.AliveEntity.Health.OnDamageTaken += HealthOnOnAttackApplied;
            Enemy.Health.OnAttackApplied += OnDefenseImpact;
        }
        protected override void RemoveEventCallbacks()
        {
            base.RemoveEventCallbacks();
            GoblinStateMachine.AliveEntity.Health.OnDamageTaken -= HealthOnOnAttackApplied;
            Enemy.Health.OnAttackApplied -= OnDefenseImpact;
        }
        
        private void HealthOnOnAttackApplied(AttackData attackData)
        {
            CinemachineShake.Instance.ShakeCamera(.3f, .3f);
            
            Debug.Log(attackData.AttackType);
            
            switch (attackData.AttackType)
            {
                case AttackType.Knock:
                    GoblinStateMachine.ChangeState(GoblinStateMachine.MediumHitGoblinEnemyState);
                    break;
                case AttackType.Medium:
                    GoblinStateMachine.ChangeState(GoblinStateMachine.MediumHitGoblinEnemyState);
                    break;
                case AttackType.Easy:
                    GoblinStateMachine.ChangeState(GoblinStateMachine.HitGoblinEnemyState);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        
        private void OnDefenseImpact()
        {
            GoblinStateMachine.ChangeState(GoblinStateMachine.DefenseHitGoblinEnemyState);
        }
    }
}