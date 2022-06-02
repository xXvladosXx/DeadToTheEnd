using CameraManage;
using Data.Animations;
using Data.Combat;
using Data.ScriptableObjects;
using Entities;
using Entities.Enemies;
using StateMachine.Enemies.WarriorEnemy;
using StateMachine.WarriorEnemy.States.Combat;
using UnityEngine;

namespace StateMachine.WarriorEnemy.States.Movement
{
    public class BaseWarriorEnemyState : BaseEnemyState
    {
        protected WarriorEnemyAnimationData WarriorEnemyAnimationData;
        protected WarriorStateMachine WarriorStateMachine;
        protected BossEnemyData bossEnemyData;

        protected BossEnemy BossEnemy;
        private float _curTime;
        private float _timeToWait;
        
        protected BaseWarriorEnemyState(WarriorStateMachine warriorStateMachine) : base(warriorStateMachine)
        {
            WarriorStateMachine = warriorStateMachine;

            BossEnemy = warriorStateMachine.AliveEntity as BossEnemy;
            bossEnemyData = BossEnemy.BossEnemyData;
            WarriorEnemyAnimationData = BossEnemy.WarriorEnemyAnimationData;
        }

        public override void Exit()
        {
            base.Exit();
            _curTime = 0;
        }

        public override void Update()
        {
            base.Update();
            _curTime += Time.deltaTime;
            float viewAngle = GetViewAngle(BossEnemy.transform,
                BossEnemy.Target.transform);

            if (viewAngle is > 45 or < -45)
            {
                BossEnemy.NavMeshAgent.isStopped = true;
                WarriorStateMachine.ChangeState(WarriorStateMachine.RotateTowardsEnemyState);
            }
        }

        protected override void AddEventCallbacks()
        {
            base.AddEventCallbacks();
            WarriorStateMachine.AliveEntity.Health.OnDamageTaken += HealthOnOnAttackApplied;
        }
        protected override void RemoveEventCallbacks()
        {
            base.RemoveEventCallbacks();
            WarriorStateMachine.AliveEntity.Health.OnDamageTaken -= HealthOnOnAttackApplied;
        }
        
        private void HealthOnOnAttackApplied(AttackData attackData)
        {
            CinemachineShake.Instance.ShakeCamera(.3f, .3f);
        }

        protected bool MakeDashAttack()
        {
            if (IsEnoughDistance(bossEnemyData.EnemyAttackData.DistanceToStartDashAttack,
                    BossEnemy.transform,
                    BossEnemy.Target.transform) &&
                !IsEnoughDistance(bossEnemyData.EnemyAttackData.DistanceToStartOrdinaryAttack,
                    BossEnemy.transform,
                    BossEnemy.Target.transform) &&
                !WarriorStateMachine.StatesCooldown.ContainsKey(typeof(DashFirstAttackWarriorEnemyState)))
            {
                WarriorStateMachine.ChangeState(WarriorStateMachine.DashFirstAttackWarriorEnemyState);
                return true;
            }

            return false;
        }

        protected bool MakeSecondDashAttack()
        {
            if (IsEnoughDistance(bossEnemyData.EnemyAttackData.DistanceToStartSecondDashAttack,
                    BossEnemy.transform,
                    BossEnemy.Target.transform) &&
                !IsEnoughDistance(bossEnemyData.EnemyAttackData.DistanceToStartOrdinaryAttack,
                    BossEnemy.transform,
                    BossEnemy.Target.transform) &&
                !WarriorStateMachine.StatesCooldown.ContainsKey(typeof(DashSecondAttackWarriorEnemy)))
            {
                WarriorStateMachine.ChangeState(WarriorStateMachine.DashSecondAttackWarriorEnemy);
                return true;
            }

            return false;
        }

        protected bool MakeComboSecondAttack()
        {
            if (IsEnoughDistance(bossEnemyData.EnemyAttackData.DistanceToStartComboSecondAttack,
                    BossEnemy.transform,
                    BossEnemy.Target.transform) &&
                !WarriorStateMachine.StatesCooldown.ContainsKey(typeof(ComboSecondWarriorEnemyState))
                && !BossEnemy.EnemyStateReusableData.IsPerformingAction)
            {
                WarriorStateMachine.ChangeState(WarriorStateMachine.ComboSecondWarriorEnemyState);
                return true;
            }

            return false;
        }
        protected bool MakeOrdinaryAttack()
        {
            if (IsEnoughDistance(bossEnemyData.EnemyAttackData.DistanceToStartOrdinaryAttack,
                    BossEnemy.transform,
                    BossEnemy.Target.transform))
            {
                WarriorStateMachine.ChangeState(WarriorStateMachine.LightAttackWarriorEnemyState);
                return true;
            }

            return false;
        }

        protected bool MakeComboFirstAttack()
        {
            if (IsEnoughDistance(bossEnemyData.EnemyAttackData.DistanceToStartComboFirstAttack,
                    BossEnemy.transform,
                    BossEnemy.Target.transform) &&
                !WarriorStateMachine.StatesCooldown.ContainsKey(typeof(ComboFirstWarriorEnemyState))
                && !BossEnemy.EnemyStateReusableData.IsPerformingAction)
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