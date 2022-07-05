using UnityEngine;

namespace StateMachine.Enemies.GoblinEnemy.States.Combat
{
    public class FirstComboAttackGoblinEnemyState : BaseCombatGoblinEnemyState
    {
        public FirstComboAttackGoblinEnemyState(GoblinStateMachine stateMachine) : base(stateMachine)
        {
        }
        
        public override void Enter()
        {
            base.Enter();
            
            GoblinEnemy.NavMeshAgent.speed = GoblinEnemy.GoblinEnemyData.GoblinFirstComboAttackData.WalkSpeedModifer;
            GoblinStateMachine.StartCooldown(GoblinStateMachine.FirstComboAttackGoblinEnemyState,
                GoblinEnemy.GoblinEnemyData.GoblinFirstComboAttackData.AttackCooldown);
            
            StartAnimation(GoblinEnemyAnimationData.FirstComboAttackParameterHash);
        }

        public override void TriggerOnStateAnimationEnterEvent()
        {
            base.TriggerOnStateAnimationEnterEvent();
            
            TargetLocked();
            GoblinEnemy.NavMeshAgent.SetDestination(GoblinEnemy.Target.transform.position);
            GoblinEnemy.NavMeshAgent.isStopped = !GoblinEnemy.NavMeshAgent.isStopped;
        }

        public override void TriggerOnStateAnimationHandleEvent()
        {
            base.TriggerOnStateAnimationHandleEvent();
            
            GoblinEnemy.NavMeshAgent.isStopped = true;

            GoblinStateMachine.ChangeState(GoblinStateMachine.FollowGoblinEnemyState);
        }

        public override void TriggerOnStateAnimationExitEvent()
        {
            base.TriggerOnStateAnimationExitEvent();

            int shouldContinueCombo = Random.Range(0,2);
            if (shouldContinueCombo == 1)
            {
                StartAnimation(GoblinEnemyAnimationData.ContinueComboAttackParameterHash);
            }
            else
            {
                GoblinStateMachine.ChangeState(GoblinStateMachine.FollowGoblinEnemyState);
            }
        }

        public override void Exit()
        {
            base.Exit();
            StopAnimation(GoblinEnemyAnimationData.FirstComboAttackParameterHash);
            StopAnimation(GoblinEnemyAnimationData.ContinueComboAttackParameterHash);
        }
    }
}