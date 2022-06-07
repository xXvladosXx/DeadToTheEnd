using Entities.Enemies;
using StateMachine.Enemies.BaseStates;
using StateMachine.Enemies.BlueGragon.Movement;
using StateMachine.WarriorEnemy.States.Movement;

namespace StateMachine.Enemies.BlueGragon
{
    public class BlueDragonStateMachine : StateMachine
    {
        public FollowBlueDragonEnemyState FollowBlueDragonEnemyState { get; }
        public RotateTowardsTargetEnemyState RotateTowardsTargetEnemyState { get; }
        public ForwardBlueDragonEnemyState ForwardBlueDragonEnemyState { get; }
        public BaseOrdinaryAttackEnemyState OrdinaryAttackBlueDragonEnemyState { get; }
        public BlueDragonStateMachine(BlueDragonEnemy blueDragonEnemy)
        {
            AliveEntity = blueDragonEnemy;
            
            FollowBlueDragonEnemyState = new FollowBlueDragonEnemyState(this);
            RotateTowardsTargetEnemyState = new RotateTowardsTargetEnemyState(this);
            ForwardBlueDragonEnemyState = new ForwardBlueDragonEnemyState(this);

            OrdinaryAttackBlueDragonEnemyState = new BaseOrdinaryAttackEnemyState(this);
        }

        public override IState StartState() => FollowBlueDragonEnemyState;
    }
}