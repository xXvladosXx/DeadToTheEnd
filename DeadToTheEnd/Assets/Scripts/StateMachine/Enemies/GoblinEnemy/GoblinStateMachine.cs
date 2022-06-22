using Entities;
using StateMachine.Enemies.BaseStates;
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
        public BaseRollEnemyState BaseRollEnemyState { get; }
        public DefenseHitGoblinEnemyState DefenseHitGoblinEnemyState { get; }
        
        public MediumHitGoblinEnemyState MediumHitGoblinEnemyState { get; }
        public HitGoblinEnemyState HitGoblinEnemyState { get; }
        
        public LightAttackGoblinEnemyState LightAttackGoblinEnemyState { get; }
        public BaseOrdinaryAttackEnemyState OrdinaryAttackGoblinEnemyState { get; }
        public BaseHeavyAttackEnemyState HeavyAttackGoblinEnemyState { get; }
        public RangeAttackGoblinEnemyState RangeAttackGoblinEnemyState { get; }
        public FirstComboAttackGoblinEnemyState FirstComboAttackGoblinEnemyState { get; }
        public SecondComboAttackGoblinEnemyState SecondComboAttackGoblinEnemyState { get; }

        public BaseDieEnemyState BaseDieEnemyState { get; }
        
        public GoblinStateMachine(Entities.Enemies.GoblinEnemy goblinEnemy)
        {
            AliveEntity = goblinEnemy;

            FollowGoblinEnemyState = new FollowGoblinEnemyState(this);
            ForwardMoveGoblinEnemyState = new ForwardMoveGoblinEnemyState(this);
            BaseRollEnemyState = new BaseRollEnemyState(this);
            DefenseHitGoblinEnemyState = new DefenseHitGoblinEnemyState(this);
            
            HitGoblinEnemyState = new HitGoblinEnemyState(this);
            MediumHitGoblinEnemyState = new MediumHitGoblinEnemyState(this);

            LightAttackGoblinEnemyState = new LightAttackGoblinEnemyState(this);
            OrdinaryAttackGoblinEnemyState = new BaseOrdinaryAttackEnemyState(this);
            HeavyAttackGoblinEnemyState = new BaseHeavyAttackEnemyState(this);
            RangeAttackGoblinEnemyState = new RangeAttackGoblinEnemyState(this);
            FirstComboAttackGoblinEnemyState = new FirstComboAttackGoblinEnemyState(this);
            SecondComboAttackGoblinEnemyState = new SecondComboAttackGoblinEnemyState(this);

            BaseDieEnemyState = new BaseDieEnemyState(this);
        }

        public override IState StartState() => FollowGoblinEnemyState;
    }
}