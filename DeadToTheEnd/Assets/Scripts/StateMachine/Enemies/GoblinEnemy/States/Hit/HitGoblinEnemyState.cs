using StateMachine.Enemies.GoblinEnemy.States.Hit;

namespace StateMachine.Enemies.GoblinEnemy.States.Movement.Hit
{
    public class HitGoblinEnemyState : BaseHitGoblinEnemyState
    {
        public HitGoblinEnemyState(GoblinStateMachine stateMachine) : base(stateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();
            
            StartAnimation(GoblinEnemyAnimationData.HitParameterHash);
        }

        public override void Exit()
        {
            base.Exit();
            StopAnimation(GoblinEnemyAnimationData.HitParameterHash);
        }
    }
}