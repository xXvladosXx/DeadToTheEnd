using System;
using System.Linq;
using CameraManage;
using Data.Animations;
using Data.Combat;
using Data.ScriptableObjects;
using StateMachine.Enemies.BaseStates;
using StateMachine.Enemies.GoblinEnemy.States.Combat;
using StateMachine.WarriorEnemy.Components;
using StateMachine.WarriorEnemy.States.Movement;
using UnityEngine;
using Random = UnityEngine.Random;

namespace StateMachine.Enemies.GoblinEnemy.States
{
    public class BaseGoblinEnemyState : BaseEnemyState
    {
        protected GoblinEnemyAnimationData GoblinEnemyAnimationData;
        protected GoblinStateMachine GoblinStateMachine;

        protected Entities.Enemies.GoblinEnemy GoblinEnemy;
        private float _curTime;
        private float _timeToWait;
        
        
        protected BaseGoblinEnemyState(GoblinStateMachine stateMachine) : base(stateMachine)
        {
            GoblinStateMachine = stateMachine;

            GoblinEnemy = stateMachine.AliveEntity as Entities.Enemies.GoblinEnemy;
            GoblinEnemyAnimationData = GoblinEnemy.EnemyAnimationData as GoblinEnemyAnimationData;
            //GoblinEnemyAnimationData = GoblinEnemy.GoblinEnemyAnimationData;

            CanAttackFunctions = new Func<bool>[]
            {
                CanMakeHeavyAttack,
                CanMakeLightAttack,
                CanMakeRangeAttack,
                CanMakeFirstComboAttack,
                CanMakeSecondComboAttack,
            };
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
                    break;
            }
        }

        protected override void Rotate()
        {
            
        }

        protected override void AddEventCallbacks()
        {
            base.AddEventCallbacks();
            GoblinStateMachine.AliveEntity.AttackCalculator.OnDamageTaken += HealthOnAttackApplied;
            Enemy.AttackCalculator.OnAttackApplied += OnDefenseImpact;
        }
        protected override void RemoveEventCallbacks()
        {
            base.RemoveEventCallbacks();
            GoblinStateMachine.AliveEntity.AttackCalculator.OnDamageTaken -= HealthOnAttackApplied;
            Enemy.AttackCalculator.OnAttackApplied -= OnDefenseImpact;
        }

        protected override void OnDied()
        {
            base.OnDied();
            GoblinStateMachine.ChangeState(GoblinStateMachine.BaseDieEnemyState);
        }

        protected override void HealthOnAttackApplied(AttackData attackData)
        {
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
        
        protected virtual void OnDefenseImpact()
        {
            GoblinStateMachine.ChangeState(GoblinStateMachine.DefenseHitGoblinEnemyState);
        }

        protected override void DecideAttackToDo()
        {
            base.DecideAttackToDo();
            int shouldWaitBeforeAttack = Random.Range(0, 1);
            if (shouldWaitBeforeAttack == 1)
            {
                _timeToWait = DecideTime(1f, 2.5f);
            }

            if (shouldWaitBeforeAttack == 1)
            {
                if (_curTime > _timeToWait)
                {
                    TryToAttack();
                }
            }
            else
            {
                TryToAttack();
            }
        }

        private void TryToAttack()
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

        protected bool CanMakeLightAttack()
        {
            if (IsEnoughDistance(GoblinEnemy.GoblinEnemyData.GoblinLightAttackData.DistanceToStartAttack,
                    GoblinStateMachine.AliveEntity.transform,
                    GoblinEnemy.Target.transform) && 
                !GoblinStateMachine.StatesCooldown.ContainsKey(GoblinStateMachine.LightAttackGoblinEnemyState))
            {
                GoblinStateMachine.ChangeState(GoblinStateMachine.LightAttackGoblinEnemyState);
                return true;
            }

            return false;
        }
        
        protected bool CanMakeRangeAttack()
        {
            if (IsEnoughDistance(GoblinEnemy.GoblinEnemyData.GoblinRangeAttackData.DistanceToStartAttack,
                    GoblinStateMachine.AliveEntity.transform,
                    GoblinEnemy.Target.transform) && 
                !GoblinStateMachine.StatesCooldown.ContainsKey(GoblinStateMachine.RangeAttackGoblinEnemyState))
            {
                GoblinStateMachine.ChangeState(GoblinStateMachine.RangeAttackGoblinEnemyState);
                return true;
            }

            return false;
        }
        
        protected bool CanMakeHeavyAttack()
        {
            if (IsEnoughDistance(GoblinEnemy.GoblinEnemyData.EnemyHeavyAttackData.DistanceToStartAttack,
                    GoblinStateMachine.AliveEntity.transform,
                    GoblinEnemy.Target.transform) && 
                !StateMachine.StatesCooldown.ContainsKey(GoblinStateMachine.HeavyAttackGoblinEnemyState))
            {
                GoblinStateMachine.ChangeState(GoblinStateMachine.HeavyAttackGoblinEnemyState);
                return true;
            }

            return false;
        }
        
        protected bool CanMakeFirstComboAttack()
        {
            if (IsEnoughDistance(GoblinEnemy.GoblinEnemyData.GoblinFirstComboAttackData.DistanceToStartAttack,
                    GoblinStateMachine.AliveEntity.transform,
                    GoblinEnemy.Target.transform) && 
                !GoblinStateMachine.StatesCooldown.ContainsKey(GoblinStateMachine.FirstComboAttackGoblinEnemyState))
            {
                GoblinStateMachine.ChangeState(GoblinStateMachine.FirstComboAttackGoblinEnemyState);
                return true;
            }

            return false;
        }
        
        protected bool CanMakeSecondComboAttack()
        {
            if (IsEnoughDistance(GoblinEnemy.GoblinEnemyData.GoblinSecondComboAttackData.DistanceToStartAttack,
                    GoblinStateMachine.AliveEntity.transform,
                    GoblinEnemy.Target.transform) && 
                !GoblinStateMachine.StatesCooldown.ContainsKey(GoblinStateMachine.SecondComboAttackGoblinEnemyState))
            {
                GoblinStateMachine.ChangeState(GoblinStateMachine.SecondComboAttackGoblinEnemyState);
                return true;
            }

            return false;
        }
    }
}