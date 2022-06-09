using Data.Combat;
using UnityEngine;

namespace StateMachine.Enemies.BaseStates
{
    public class BaseRangeAttackEnemyState : BaseAttackEnemyState
    {
        public BaseRangeAttackEnemyState(StateMachine stateMachine) : base(stateMachine)
        {
        }
        
        public override void Enter()
        {
            base.Enter();

            StateMachine.StartCooldown(typeof(BaseRangeAttackEnemyState),
                Enemy.EnemyData.EnemyRangeAttackData.AttackCooldown);

            StartAnimation(Enemy.EnemyAnimationData.RangeAttackParameterHash);
        }

        public override void OnAnimationEnterEvent()
        {
            base.OnAnimationEnterEvent();
            TargetLocked();
            Enemy.NavMeshAgent.SetDestination(Enemy.Target.transform.position);
            Enemy.NavMeshAgent.speed = Enemy.EnemyData.EnemyRangeAttackData.WalkSpeedModifer;

            Enemy.NavMeshAgent.isStopped = false;
        }
        public override void OnAnimationHandleEvent()
        {
            base.OnAnimationHandleEvent();
            Enemy.NavMeshAgent.isStopped = true;
        }
        public override void OnAnimationExitEvent()
        {
            base.OnAnimationExitEvent();
            StateMachine.ChangeState(StateMachine.StartState());
        }
        public override void Exit()
        {
            base.Exit();

            StopAnimation(Enemy.EnemyAnimationData.RangeAttackParameterHash);
        }
    }
}