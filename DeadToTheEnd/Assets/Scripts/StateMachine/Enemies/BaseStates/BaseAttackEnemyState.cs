using Data.Animations;
using Data.Combat;
using Entities.Enemies;
using StateMachine.Core;
using StateMachine.Enemies.BlueGragon;
using StateMachine.WarriorEnemy.States.Movement;

namespace StateMachine.Enemies.BaseStates
{
    public class BaseAttackEnemyState : BaseEnemyState, ITimeable
    {
        public BaseAttackEnemyState(StandardEnemyStateMachine stateMachine) : base(stateMachine)
        {
        }
        
        public override void Enter()
        {
            base.Enter();
            
            Enemy.NavMeshAgent.isStopped = true;
            Enemy.NavMeshAgent.updateRotation = true;
            Enemy.NavMeshAgent.speed = 0;

            Enemy.NavMeshAgent.ResetPath();
            StartAnimation(Enemy.EnemyAnimationData.AttackParameterHash);
        }

        public override void Update()
        {
        }


        protected override void HealthOnAttackApplied(AttackData attackData)
        {
            
        }

        public override void Exit()
        {
            base.Exit();
            
            Enemy.NavMeshAgent.ResetPath();
            Enemy.NavMeshAgent.isStopped = true;
            
            StopAnimation(Enemy.EnemyAnimationData.AttackParameterHash);
        }

        public float GetTime()
        {
            return 0;
        }
    }
}