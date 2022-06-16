using System;
using Data.Combat;
using StateMachine.Core;
using Random = UnityEngine.Random;

namespace StateMachine.Enemies.GoblinEnemy.States.Combat
{
    public class BaseCombatGoblinEnemyState : BaseGoblinEnemyState, ITimeable
    {
        private bool _timeToWait;
        
        protected BaseCombatGoblinEnemyState(GoblinStateMachine stateMachine) : base(stateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();
            
            GoblinEnemy.NavMeshAgent.isStopped = true;
            GoblinEnemy.NavMeshAgent.updateRotation = true;
            GoblinEnemy.NavMeshAgent.speed = 0;

            GoblinEnemy.NavMeshAgent.SetDestination(GoblinEnemy.Target.transform.position);

            GoblinEnemy.GoblinStateReusableData.IsBlocking = false;
            
            StartAnimation(GoblinEnemyAnimationData.AttackParameterHash);
        }

        public override void Update()
        {
        }

        protected override void OnDefenseImpact()
        {
        }

        protected override void HealthOnAttackApplied(AttackData attackData)
        {
            
        }

        public override void Exit()
        {
            base.Exit();
            
            GoblinEnemy.NavMeshAgent.ResetPath();
            GoblinEnemy.NavMeshAgent.isStopped = true;
            
            StopAnimation(GoblinEnemyAnimationData.AttackParameterHash);
        }
    }
}