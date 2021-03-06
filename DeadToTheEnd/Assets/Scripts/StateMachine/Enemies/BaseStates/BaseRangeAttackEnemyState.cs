using Data.Combat;
using StateMachine.Core;
using StateMachine.Enemies.BlueGragon;
using UnityEngine;

namespace StateMachine.Enemies.BaseStates
{
    public class BaseRangeAttackEnemyState : BaseAttackEnemyState
    {
        public BaseRangeAttackEnemyState(StandardEnemyStateMachine stateMachine) : base(stateMachine)
        {
        }
        
        public override void Enter()
        {
            base.Enter();

            StateMachine.StartCooldown(StateMachine.BaseRangeAttackEnemyState,
                EnemyData.EnemyRangeAttackData.AttackCooldown);

            StartAnimation(Enemy.EnemyAnimationData.RangeAttackParameterHash);
        }

        public override void TriggerOnStateAnimationEnterEvent()
        {
            base.TriggerOnStateAnimationEnterEvent();
            TargetLocked();
            Enemy.NavMeshAgent.SetDestination(Enemy.Target.transform.position);
            Enemy.NavMeshAgent.speed = EnemyData.EnemyRangeAttackData.WalkSpeedModifer;

            Enemy.NavMeshAgent.isStopped = false;
        }
        public override void TriggerOnStateAnimationHandleEvent()
        {
            base.TriggerOnStateAnimationHandleEvent();
            Enemy.NavMeshAgent.isStopped = true;
        }
        public override void TriggerOnStateAnimationExitEvent()
        {
            base.TriggerOnStateAnimationExitEvent();
            StateMachine.ChangeState(StateMachine.StartState());
        }
        public override void Exit()
        {
            base.Exit();

            StopAnimation(Enemy.EnemyAnimationData.RangeAttackParameterHash);
        }
    }
}