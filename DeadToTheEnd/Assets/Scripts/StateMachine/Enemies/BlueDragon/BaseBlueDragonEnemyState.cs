using Data.Animations;
using Data.ScriptableObjects;
using Entities.Enemies;
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
        }

       
        protected override void Rotate()
        {
            BlueDragonStateMachine.ChangeState(BlueDragonStateMachine.RotateTowardsTargetEnemyState);
        }
    }

    
}