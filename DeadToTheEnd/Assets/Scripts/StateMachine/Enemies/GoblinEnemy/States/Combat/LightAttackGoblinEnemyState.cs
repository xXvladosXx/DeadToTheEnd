namespace StateMachine.Enemies.GoblinEnemy.States.Combat
{
    public class LightAttackGoblinEnemyState : BaseCombatGoblinEnemyState
    {
        public LightAttackGoblinEnemyState(GoblinStateMachine stateMachine) : base(stateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();
            
            GoblinEnemy.NavMeshAgent.speed = GoblinEnemy.GoblinEnemyData.GoblinLightAttackData.WalkSpeedModiferFirstAttack;

            StartAnimation(GoblinEnemyAnimationData.LightAttackParameterHash);
        }

        public override void OnAnimationEnterEvent()
        {
            base.OnAnimationEnterEvent();
            
            GoblinEnemy.NavMeshAgent.isStopped = false;
        }

        public override void OnAnimationHandleEvent()
        {
            base.OnAnimationHandleEvent();
            
            GoblinEnemy.NavMeshAgent.speed = GoblinEnemy.GoblinEnemyData.GoblinLightAttackData.WalkSpeedModiferSecondAttack;
        }

        public override void OnAnimationExitEvent()
        {
            base.OnAnimationExitEvent();
            GoblinStateMachine.ChangeState(GoblinStateMachine.FollowGoblinEnemyState);
        }

        public override void Exit()
        {
            base.Exit();
            StopAnimation(GoblinEnemyAnimationData.LightAttackParameterHash);
        }
    }
}