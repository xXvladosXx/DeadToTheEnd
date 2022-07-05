using UnityEngine;

namespace StateMachine.Enemies.GoblinEnemy.States.Combat
{
    public class RangeAttackGoblinEnemyState : BaseCombatGoblinEnemyState
    {
        public RangeAttackGoblinEnemyState(GoblinStateMachine stateMachine) : base(stateMachine)
        {
        }
        
        public override void Enter()
        {
            base.Enter();

            GoblinEnemy.NavMeshAgent.speed = GoblinEnemy.GoblinEnemyData.GoblinRangeAttackData.WalkSpeedModifer;
            GoblinStateMachine.StartCooldown(GoblinStateMachine.RangeAttackGoblinEnemyState,
                GoblinEnemy.GoblinEnemyData.GoblinRangeAttackData.AttackCooldown);
            
            StartAnimation(GoblinEnemyAnimationData.RangeAttackParameterHash);
        }

        public override void TriggerOnStateAnimationEnterEvent()
        {
            base.TriggerOnStateAnimationEnterEvent();
            
            GoblinEnemy.NavMeshAgent.SetDestination(GoblinEnemy.Target.transform.position);
            GoblinEnemy.NavMeshAgent.isStopped = false;
        }

        public override void TriggerOnStateAnimationHandleEvent()
        {
            base.TriggerOnStateAnimationHandleEvent();
            TargetLocked();
            GoblinEnemy.NavMeshAgent.isStopped = true;
        }

        public override void TriggerOnStateAnimationExitEvent()
        {
            base.TriggerOnStateAnimationExitEvent();
            GoblinStateMachine.ChangeState(GoblinStateMachine.FollowGoblinEnemyState);
        }

        public override void Exit()
        {
            base.Exit();
            StopAnimation(GoblinEnemyAnimationData.RangeAttackParameterHash);
        }
    }
}