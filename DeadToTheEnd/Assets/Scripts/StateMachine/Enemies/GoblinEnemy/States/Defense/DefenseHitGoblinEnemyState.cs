using StateMachine.Enemies.GoblinEnemy.States.Movement;

namespace StateMachine.Enemies.GoblinEnemy.States.Defense
{
    public class DefenseHitGoblinEnemyState : BaseGoblinEnemyState
    {
        public DefenseHitGoblinEnemyState(GoblinStateMachine stateMachine) : base(stateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();
            StartAnimation(GoblinEnemyAnimationData.BlockHitParameterHash);
        }

        public override void Exit()
        {
            base.Exit();
            StopAnimation(GoblinEnemyAnimationData.BlockHitParameterHash);
        }

        public override void OnAnimationExitEvent()
        {
            base.OnAnimationExitEvent();
            GoblinStateMachine.ChangeState(GoblinStateMachine.FollowGoblinEnemyState);
        }
    }
}