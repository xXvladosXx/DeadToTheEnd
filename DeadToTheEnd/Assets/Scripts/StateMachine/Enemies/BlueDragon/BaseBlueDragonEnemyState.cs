using System;
using Data.Animations;
using Data.ScriptableObjects;
using Entities.Enemies;
using StateMachine.Enemies.BaseStates;
using StateMachine.WarriorEnemy.States.Movement;
using UnityEngine;

namespace StateMachine.Enemies.BlueGragon
{
    public class BaseBlueDragonEnemyState : BaseEnemyState
    {
        protected readonly BlueDragonAnimationData BlueDragonAnimationData;
        protected readonly BlueDragonStateMachine BlueDragonStateMachine;
        protected readonly BlueDragonEnemyData BlueDragonEnemyData;

        protected readonly BlueDragonEnemy BlueDragonEnemy;

        public BaseBlueDragonEnemyState(BlueDragonStateMachine stateMachine) : base(stateMachine)
        {
            BlueDragonStateMachine = stateMachine;

            BlueDragonEnemy = stateMachine.AliveEntity as BlueDragonEnemy;
            BlueDragonEnemyData = BlueDragonEnemy.BlueDragonEnemyData;
            BlueDragonAnimationData = BlueDragonEnemy.EnemyAnimationData as BlueDragonAnimationData;
            
            CanAttackFunctions = new Func<bool>[]
            {
                CanMakeOrdinaryAttack,
                CanMakeHeavyAttack,
                CanMakeRangeAttack
            };
        }

        private bool CanMakeHeavyAttack()
        {
            if (IsEnoughDistance(Enemy.EnemyData.EnemyHeavyAttackData.DistanceToStartAttack,
                    StateMachine.AliveEntity.transform,
                    Enemy.Target.transform) && 
                !StateMachine.StatesCooldown.ContainsKey(typeof(BaseHeavyAttackEnemyState)))
            {
                BlueDragonStateMachine.ChangeState(BlueDragonStateMachine.BaseHeavyAttackEnemyState);
                return true;
            }

            return false;
        }
        private bool CanMakeRangeAttack()
        {
            if (IsEnoughDistance(Enemy.EnemyData.EnemyRangeAttackData.DistanceToStartAttack,
                    StateMachine.AliveEntity.transform,
                    Enemy.Target.transform) && 
                !StateMachine.StatesCooldown.ContainsKey(typeof(BaseRangeAttackEnemyState)))
            {
                BlueDragonStateMachine.ChangeState(BlueDragonStateMachine.BaseRangeAttackEnemyState);
                return true;
            }

            return false;
        }
        private bool CanMakeOrdinaryAttack()
        {
            if (IsEnoughDistance(Enemy.EnemyData.EnemyOrdinaryAttackData.DistanceToStartAttack,
                    StateMachine.AliveEntity.transform,
                    Enemy.Target.transform) && 
                !StateMachine.StatesCooldown.ContainsKey(typeof(BaseOrdinaryAttackEnemyState)))
            {
                BlueDragonStateMachine.ChangeState(BlueDragonStateMachine.BaseOrdinaryAttackEnemyState);
                return true;
            }

            return false;
        }
       
        protected override void Rotate()
        {
            BlueDragonStateMachine.ChangeState(BlueDragonStateMachine.RotateTowardsTargetEnemyState);
        }
    }

    
}