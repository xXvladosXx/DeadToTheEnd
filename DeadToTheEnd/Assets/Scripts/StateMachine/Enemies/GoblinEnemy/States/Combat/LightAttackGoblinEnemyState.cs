using Data.Combat;

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
            
            GoblinEnemy.NavMeshAgent.speed = GoblinEnemy.GoblinEnemyData.GoblinLightAttackData.WalkSpeedModifer;
            GoblinStateMachine.StartCooldown(typeof(LightAttackGoblinEnemyState),
                GoblinEnemy.GoblinEnemyData.GoblinLightAttackData.AttackCooldown);
            
            StartAnimation(GoblinEnemyAnimationData.LightAttackParameterHash);
        }

        public override void TriggerOnStateAnimationEnterEvent()
        {
            base.TriggerOnStateAnimationEnterEvent();
            
            GoblinEnemy.NavMeshAgent.isStopped = false;
        }

        public override void TriggerOnStateAnimationHandleEvent()
        {
            base.TriggerOnStateAnimationHandleEvent();
            
            GoblinEnemy.NavMeshAgent.speed = GoblinEnemy.GoblinEnemyData.GoblinLightAttackData.WalkSpeedModiferSecond;
        }

        public override void TriggerOnStateAnimationExitEvent()
        {
            base.TriggerOnStateAnimationExitEvent();
            GoblinStateMachine.ChangeState(GoblinStateMachine.FollowGoblinEnemyState);
        }

        public override void Exit()
        {
            base.Exit();
            StopAnimation(GoblinEnemyAnimationData.LightAttackParameterHash);
        }
    }
}