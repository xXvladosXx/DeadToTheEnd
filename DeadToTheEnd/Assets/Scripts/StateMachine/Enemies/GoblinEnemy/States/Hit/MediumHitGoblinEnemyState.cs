namespace StateMachine.Enemies.GoblinEnemy.States.Movement.Hit
{
    public class MediumHitGoblinEnemyState : HitGoblinEnemyState
    {
        public MediumHitGoblinEnemyState(GoblinStateMachine stateMachine) : base(stateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();
            StartAnimation(GoblinEnemyAnimationData.MediumHitParameterHash);
        }

        public override void Exit()
        {
            base.Exit();
            StopAnimation(GoblinEnemyAnimationData.MediumHitParameterHash);
        }
    }
}