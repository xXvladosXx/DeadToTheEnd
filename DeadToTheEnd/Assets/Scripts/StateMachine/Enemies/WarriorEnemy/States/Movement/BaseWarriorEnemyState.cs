using CameraManage;
using Data.Animations;
using Data.Combat;
using Data.ScriptableObjects;
using Entities;
using Entities.Enemies;
using StateMachine.Enemies;
using StateMachine.Enemies.WarriorEnemy;
using StateMachine.WarriorEnemy.States.Combat;
using UnityEngine;

namespace StateMachine.WarriorEnemy.States.Movement
{
    public class BaseWarriorEnemyState : BaseEnemyState
    {
        protected readonly WarriorEnemyAnimationData WarriorEnemyAnimationData;
        protected readonly WarriorStateMachine WarriorStateMachine;
        protected readonly WarriorEnemyData WarriorEnemyData;

        protected readonly Entities.Enemies.WarriorEnemy WarriorEnemy;
        private float _timeToWait;
        
        protected BaseWarriorEnemyState(WarriorStateMachine warriorStateMachine) : base(warriorStateMachine)
        {
            WarriorStateMachine = warriorStateMachine;

            WarriorEnemy = warriorStateMachine.AliveEntity as Entities.Enemies.WarriorEnemy;
            WarriorEnemyData = WarriorEnemy.WarriorEnemyData;
            WarriorEnemyAnimationData = WarriorEnemy.EnemyAnimationData as WarriorEnemyAnimationData;
        }

        protected override void Rotate()
        {
            float viewAngle = GetViewAngle(WarriorEnemy.transform,
                WarriorEnemy.Target.transform);

            if (viewAngle is > 45 or < -45)
            {
                WarriorEnemy.NavMeshAgent.isStopped = true;
                WarriorStateMachine.ChangeState(WarriorStateMachine.RotateTowardsEnemyState);
            }
        }

        protected override void HealthOnAttackApplied(AttackData attackData)
        {
            CinemachineShake.Instance.ShakeCamera(.3f, .3f);
        }

        protected bool MakeDashAttack()
        {
            if (IsEnoughDistance(WarriorEnemyData.EnemyAttackData.DistanceToStartDashAttack,
                    WarriorEnemy.transform,
                    WarriorEnemy.Target.transform) &&
                !IsEnoughDistance(WarriorEnemyData.EnemyAttackData.DistanceToStartOrdinaryAttack,
                    WarriorEnemy.transform,
                    WarriorEnemy.Target.transform) &&
                !WarriorStateMachine.StatesCooldown.ContainsKey(typeof(DashFirstAttackWarriorWarriorEnemyState)))
            {
                WarriorStateMachine.ChangeState(WarriorStateMachine.DashFirstAttackWarriorWarriorEnemyState);
                return true;
            }

            return false;
        }

        protected bool MakeSecondDashAttack()
        {
            if (IsEnoughDistance(WarriorEnemyData.EnemyAttackData.DistanceToStartSecondDashAttack,
                    WarriorEnemy.transform,
                    WarriorEnemy.Target.transform) &&
                !IsEnoughDistance(WarriorEnemyData.EnemyAttackData.DistanceToStartOrdinaryAttack,
                    WarriorEnemy.transform,
                    WarriorEnemy.Target.transform) &&
                !WarriorStateMachine.StatesCooldown.ContainsKey(typeof(DashSecondAttackWarriorWarriorEnemy)))
            {
                WarriorStateMachine.ChangeState(WarriorStateMachine.DashSecondAttackWarriorWarriorEnemy);
                return true;
            }

            return false;
        }

        protected bool MakeComboSecondAttack()
        {
            if (IsEnoughDistance(WarriorEnemyData.EnemyAttackData.DistanceToStartComboSecondAttack,
                    WarriorEnemy.transform,
                    WarriorEnemy.Target.transform) &&
                !WarriorStateMachine.StatesCooldown.ContainsKey(typeof(ComboSecondWarriorWarriorEnemyState))
                && !WarriorEnemy.WarriorStateReusableData.IsPerformingAction)
            {
                WarriorStateMachine.ChangeState(WarriorStateMachine.ComboSecondWarriorWarriorEnemyState);
                return true;
            }

            return false;
        }
        protected bool MakeOrdinaryAttack()
        {
            if (IsEnoughDistance(WarriorEnemyData.EnemyAttackData.DistanceToStartComboSecondAttack,
                    WarriorEnemy.transform,
                    WarriorEnemy.Target.transform) &&
                !WarriorStateMachine.StatesCooldown.ContainsKey(typeof(LightAttackWarriorWarriorEnemyState))
                && !WarriorEnemy.WarriorStateReusableData.IsPerformingAction)
            {
                WarriorStateMachine.ChangeState(WarriorStateMachine.LightAttackWarriorWarriorEnemyState);
                return true;
            }

            return false;
        }

        protected bool MakeComboFirstAttack()
        {
            if (IsEnoughDistance(WarriorEnemyData.EnemyAttackData.DistanceToStartComboFirstAttack,
                    WarriorEnemy.transform,
                    WarriorEnemy.Target.transform) &&
                !WarriorStateMachine.StatesCooldown.ContainsKey(typeof(ComboFirstWarriorWarriorEnemyState))
                && !WarriorEnemy.WarriorStateReusableData.IsPerformingAction)
            {
                WarriorStateMachine.ChangeState(WarriorStateMachine.ComboFirstWarriorWarriorEnemyState);
                return true;
            }

            return false;
        }
        
        protected override void DecideAttackToDo()
        {
            while (true)
            {
                int attack = Random.Range(0, 6);
                switch (attack)
                {
                    case 0:
                        if (MakeOrdinaryAttack())
                            break;

                        continue;
                    case 1:
                        if (MakeDashAttack())
                            break;
                        
                        continue;
                    case 2:
                        if (MakeComboFirstAttack())
                            break;
                        
                        continue;
                    case 3:
                        if (MakeSecondDashAttack())
                            break;
                        
                        continue;
                    case 4:
                        if (MakeComboSecondAttack())
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