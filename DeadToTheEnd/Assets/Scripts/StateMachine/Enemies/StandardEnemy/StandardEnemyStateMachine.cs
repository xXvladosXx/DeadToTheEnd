using Entities.Enemies;
using StateMachine.Core;
using StateMachine.Enemies.BaseStates;
using StateMachine.Enemies.BlueGragon.Movement;
using StateMachine.WarriorEnemy.States.Movement;

namespace StateMachine.Enemies.BlueGragon
{
    public class StandardEnemyStateMachine : EntityStateMachine
    {
        public FollowStandardEnemyState FollowStandardEnemyState { get; }
        public RotateTowardsTargetEnemyState RotateTowardsTargetEnemyState { get; }
        public ForwardStandardEnemyState ForwardStandardEnemyState { get; }
        public BaseOrdinaryAttackEnemyState BaseOrdinaryAttackEnemyState { get; }
        public BaseHeavyAttackEnemyState BaseHeavyAttackEnemyState { get; }
        public BaseRangeAttackEnemyState BaseRangeAttackEnemyState { get; }
        public BaseDieEnemyState BaseDieEnemyState { get; }
        
        public StandardEnemyStateMachine(Enemy blueDragonEnemy)
        {
            AliveEntity = blueDragonEnemy;
            
            FollowStandardEnemyState = new FollowStandardEnemyState(this);
            RotateTowardsTargetEnemyState = new RotateTowardsTargetEnemyState(this);
            ForwardStandardEnemyState = new ForwardStandardEnemyState(this);

            BaseOrdinaryAttackEnemyState = new BaseOrdinaryAttackEnemyState(this);
            BaseHeavyAttackEnemyState = new BaseHeavyAttackEnemyState(this);
            BaseRangeAttackEnemyState = new BaseRangeAttackEnemyState(this);

            BaseDieEnemyState = new BaseDieEnemyState(this);
        }

        public override IState StartState() => FollowStandardEnemyState;
    }
}