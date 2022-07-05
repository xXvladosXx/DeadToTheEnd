using StateMachine.Core;
using StateMachine.Enemies.BlueGragon;
using StateMachine.WarriorEnemy.States.Movement;
using UnityEngine;

namespace StateMachine.Enemies.GoblinEnemy.States.Movement
{
    public class BaseRollEnemyState : BaseEnemyState
    {
        private Vector3 direction;
        public BaseRollEnemyState(StandardEnemyStateMachine goblinStateMachine) : base(goblinStateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();
            StartAnimation(Enemy.EnemyAnimationData.RollParameterHash);

            Enemy.NavMeshAgent.updateRotation = false;
            Enemy.NavMeshAgent.speed = EnemyData.EnemyRollData.RollSpeedModifer;
        }

        public override void Update()
        {
            Enemy.NavMeshAgent.speed -= Time.deltaTime*5;
            Vector3 direction = Enemy.transform.position -
                                Enemy.Target.transform.position; 
            
            Enemy.transform.LookAt(Enemy.Target.transform.position);
            Enemy.NavMeshAgent.SetDestination(Enemy.transform.position + direction);
        }

        public override void Exit()
        {
            base.Exit();
            StopAnimation(Enemy.EnemyAnimationData.RollParameterHash);
        }

        protected override void AddEventCallbacks()
        {
        }

        protected override void RemoveEventCallbacks()
        {
        }

        public override void TriggerOnStateAnimationEnterEvent()
        {
            Enemy.NavMeshAgent.isStopped = false;
        }

        public override void TriggerOnStateAnimationExitEvent()
        {
            StateMachine.ChangeState(StateMachine.StartState());
        }
    }
}