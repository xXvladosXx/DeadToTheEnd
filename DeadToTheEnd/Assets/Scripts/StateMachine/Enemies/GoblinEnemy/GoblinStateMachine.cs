using Entities;
using StateMachine.Enemies.GoblinEnemy.States.Movement;

namespace StateMachine.Enemies.GoblinEnemy
{
    public class GoblinStateMachine : StateMachine
    {
        public FollowGoblinEnemyState FollowGoblinEnemyState { get; }
        public ForwardMoveGoblinEnemyState ForwardMoveGoblinEnemyState { get; }
        public RotateGoblinEnemyState RotateGoblinEnemyState { get; }
        public RollGoblinEnemyState RollGoblinEnemyState { get; }
        public GoblinStateMachine(Entities.Enemies.GoblinEnemy goblinEnemy)
        {
            AliveEntity = goblinEnemy;

            FollowGoblinEnemyState = new FollowGoblinEnemyState(this);
            ForwardMoveGoblinEnemyState = new ForwardMoveGoblinEnemyState(this);
            RotateGoblinEnemyState = new RotateGoblinEnemyState(this);
            RollGoblinEnemyState = new RollGoblinEnemyState(this);
        }

        public override IState StartState()
        {
            return FollowGoblinEnemyState;
        }
    }
}