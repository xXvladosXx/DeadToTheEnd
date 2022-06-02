using Entities;
using StateMachine.Enemies.GoblinEnemy.States.Combat;
using StateMachine.Enemies.GoblinEnemy.States.Defense;
using StateMachine.Enemies.GoblinEnemy.States.Movement;
using StateMachine.Enemies.GoblinEnemy.States.Movement.Hit;

namespace StateMachine.Enemies.GoblinEnemy
{
    public class GoblinStateMachine : StateMachine
    {
        public FollowGoblinEnemyState FollowGoblinEnemyState { get; }
        public ForwardMoveGoblinEnemyState ForwardMoveGoblinEnemyState { get; }
        public RotateGoblinEnemyState RotateGoblinEnemyState { get; }
        public RollGoblinEnemyState RollGoblinEnemyState { get; }
        public DefenseHitGoblinEnemyState DefenseHitGoblinEnemyState { get; }
        
        public MediumHitGoblinEnemyState MediumHitGoblinEnemyState { get; }
        public HitGoblinEnemyState HitGoblinEnemyState { get; }
        
        public LightAttackGoblinEnemyState LightAttackGoblinEnemyState { get; }
        public GoblinStateMachine(Entities.Enemies.GoblinEnemy goblinEnemy)
        {
            AliveEntity = goblinEnemy;

            FollowGoblinEnemyState = new FollowGoblinEnemyState(this);
            ForwardMoveGoblinEnemyState = new ForwardMoveGoblinEnemyState(this);
            RotateGoblinEnemyState = new RotateGoblinEnemyState(this);
            RollGoblinEnemyState = new RollGoblinEnemyState(this);
            DefenseHitGoblinEnemyState = new DefenseHitGoblinEnemyState(this);
            
            HitGoblinEnemyState = new HitGoblinEnemyState(this);
            MediumHitGoblinEnemyState = new MediumHitGoblinEnemyState(this);

            LightAttackGoblinEnemyState = new LightAttackGoblinEnemyState(this);
        }

        public override IState StartState()
        {
            return FollowGoblinEnemyState;
        }
    }
}