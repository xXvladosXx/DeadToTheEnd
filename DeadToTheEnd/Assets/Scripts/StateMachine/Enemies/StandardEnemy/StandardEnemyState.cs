using System;
using Data.Animations;
using Data.ScriptableObjects;
using Entities.Enemies;
using StateMachine.Enemies.BaseStates;
using StateMachine.WarriorEnemy.States.Movement;
using UnityEngine;

namespace StateMachine.Enemies.BlueGragon
{
    public class StandardEnemyState : BaseEnemyState
    {
        protected readonly BlueDragonAnimationData BlueDragonAnimationData;
        protected readonly StandardEnemyStateMachine StandardEnemyStateMachine;
        protected readonly StandardEnemyData StandardEnemyData;

        protected readonly StandardEnemy StandardEnemy;

        public StandardEnemyState(StandardEnemyStateMachine stateMachine) : base(stateMachine)
        {
            StandardEnemyStateMachine = stateMachine;

            StandardEnemy = stateMachine.AliveEntity as StandardEnemy;
            StandardEnemyData = StandardEnemy.StandardEnemyData;
            BlueDragonAnimationData = StandardEnemy.EnemyAnimationData as BlueDragonAnimationData;
            
            CanAttackFunctions = new Func<bool>[]
            {
                CanMakeOrdinaryAttack,
                CanMakeHeavyAttack,
                CanMakeRangeAttack
            };
        }

        private bool CanMakeHeavyAttack()
        {
            if (IsEnoughDistance(EnemyData.EnemyHeavyAttackData.DistanceToStartAttack,
                    StateMachine.AliveEntity.transform,
                    Enemy.Target.transform) && 
                !StateMachine.StatesCooldown.ContainsKey(typeof(BaseHeavyAttackEnemyState)))
            {
                StandardEnemyStateMachine.ChangeState(StandardEnemyStateMachine.BaseHeavyAttackEnemyState);
                return true;
            }

            return false;
        }
        private bool CanMakeRangeAttack()
        {
            if (IsEnoughDistance(EnemyData.EnemyRangeAttackData.DistanceToStartAttack,
                    StateMachine.AliveEntity.transform,
                    Enemy.Target.transform) && 
                !StateMachine.StatesCooldown.ContainsKey(typeof(BaseRangeAttackEnemyState)))
            {
                StandardEnemyStateMachine.ChangeState(StandardEnemyStateMachine.BaseRangeAttackEnemyState);
                return true;
            }

            return false;
        }
        private bool CanMakeOrdinaryAttack()
        {
            if (IsEnoughDistance(EnemyData.EnemyOrdinaryAttackData.DistanceToStartAttack,
                    StateMachine.AliveEntity.transform,
                    Enemy.Target.transform) && 
                !StateMachine.StatesCooldown.ContainsKey(typeof(BaseOrdinaryAttackEnemyState)))
            {
                StandardEnemyStateMachine.ChangeState(StandardEnemyStateMachine.BaseOrdinaryAttackEnemyState);
                return true;
            }

            return false;
        }
       
        protected override void Rotate()
        {
            StandardEnemyStateMachine.ChangeState(StandardEnemyStateMachine.RotateTowardsTargetEnemyState);
        }

        protected override void OnDied()
        {
            base.OnDied();
            StandardEnemyStateMachine.ChangeState(StandardEnemyStateMachine.BaseDieEnemyState);
        }
    }

    
}