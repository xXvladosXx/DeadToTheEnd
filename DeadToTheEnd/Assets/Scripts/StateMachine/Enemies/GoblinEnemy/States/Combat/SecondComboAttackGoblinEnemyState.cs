using StateMachine.Enemies.WarriorEnemy.States.Combat;
using UnityEngine;

namespace StateMachine.Enemies.GoblinEnemy.States.Combat
{
    public class SecondComboAttackGoblinEnemyState : BaseCombatGoblinEnemyState
    {
        public SecondComboAttackGoblinEnemyState(GoblinStateMachine stateMachine) : base(stateMachine)
        {
        }
        
        public override void Enter()
        {
            base.Enter();
            
            GoblinEnemy.NavMeshAgent.speed = GoblinEnemy.GoblinEnemyData.GoblinSecondComboAttackData.WalkSpeedModifer;
            GoblinStateMachine.StartCooldown(typeof(SecondComboAttackGoblinEnemyState),
                GoblinEnemy.GoblinEnemyData.GoblinSecondComboAttackData.AttackCooldown);
            
            StartAnimation(GoblinEnemyAnimationData.SecondComboAttackParameterHash);
        }

        public override void OnAnimationEnterEvent()
        {
            base.OnAnimationEnterEvent();
            
            GoblinEnemy.NavMeshAgent.speed = 
                GoblinEnemy.GoblinEnemyData.GoblinSecondComboAttackData.WalkSpeedModifer;
            
            GoblinEnemy.NavMeshAgent.SetDestination(GoblinEnemy.Target.transform.position);

            GoblinEnemy.NavMeshAgent.isStopped = false;
        }

        public override void OnAnimationHandleEvent()
        {
            base.OnAnimationHandleEvent();
            
            GoblinStateMachine.ChangeState(GoblinStateMachine.FollowGoblinEnemyState);
        }

        public override void OnAnimationExitEvent()
        {
            base.OnAnimationExitEvent();
            GoblinEnemy.NavMeshAgent.SetDestination(GoblinEnemy.Target.transform.position);

            GoblinEnemy.NavMeshAgent.speed = 
                GoblinEnemy.GoblinEnemyData.GoblinSecondComboAttackData.WalkSpeedSecondModifer;
        }

        public override void Exit()
        {
            base.Exit();
            StopAnimation(GoblinEnemyAnimationData.SecondComboAttackParameterHash);
        }
    }
}