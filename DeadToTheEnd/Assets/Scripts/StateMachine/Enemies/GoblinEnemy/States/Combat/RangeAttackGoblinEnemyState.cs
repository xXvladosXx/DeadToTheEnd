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
            GoblinStateMachine.StartCooldown(typeof(RangeAttackGoblinEnemyState),
                GoblinEnemy.GoblinEnemyData.GoblinRangeAttackData.AttackCooldown);
            
            StartAnimation(GoblinEnemyAnimationData.RangeAttackParameterHash);
        }

        public override void OnAnimationEnterEvent()
        {
            base.OnAnimationEnterEvent();
            
            GoblinEnemy.NavMeshAgent.SetDestination(GoblinEnemy.Target.transform.position);
            GoblinEnemy.NavMeshAgent.isStopped = false;
        }

        public override void OnAnimationHandleEvent()
        {
            base.OnAnimationHandleEvent();
            TargetLocked();
            GoblinEnemy.NavMeshAgent.isStopped = true;
        }

        public override void OnAnimationExitEvent()
        {
            base.OnAnimationExitEvent();
            GoblinStateMachine.ChangeState(GoblinStateMachine.FollowGoblinEnemyState);
        }

        public override void Exit()
        {
            base.Exit();
            StopAnimation(GoblinEnemyAnimationData.RangeAttackParameterHash);
        }
    }
}