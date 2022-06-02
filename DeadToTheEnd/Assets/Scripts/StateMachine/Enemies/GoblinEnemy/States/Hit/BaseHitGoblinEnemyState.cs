using StateMachine.Enemies.GoblinEnemy.States.Movement;

namespace StateMachine.Enemies.GoblinEnemy.States.Hit
{
    public class BaseHitGoblinEnemyState : BaseGoblinEnemyState
    {
        protected BaseHitGoblinEnemyState(GoblinStateMachine stateMachine) : base(stateMachine)
        {
        }
        public override void OnAnimationExitEvent()
        {
            base.OnAnimationExitEvent();
            GoblinStateMachine.ChangeState(GoblinStateMachine.FollowGoblinEnemyState);
        }

    }
}