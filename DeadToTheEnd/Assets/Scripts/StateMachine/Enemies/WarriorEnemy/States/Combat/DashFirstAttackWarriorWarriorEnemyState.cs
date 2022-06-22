using Data.Combat;
using Data.ScriptableObjects;
using Entities;
using StateMachine.Enemies.WarriorEnemy;
using StateMachine.Enemies.WarriorEnemy.States.Combat;
using StateMachine.WarriorEnemy.States.Movement;
using UnityEngine;

namespace StateMachine.WarriorEnemy.States.Combat
{
    public class DashFirstAttackWarriorWarriorEnemyState : BaseAttackWarriorEnemyState
    {
        private bool _hasStopped;
        private bool _startAttack;

        public DashFirstAttackWarriorWarriorEnemyState(WarriorStateMachine warriorStateMachine) : base(warriorStateMachine)
        {
           
        }

        public override void Enter()
        {
            base.Enter();
            WarriorStateMachine.AliveEntity.OrdinaryAttackColliderActivator.OnTargetHit += Stop;
            
            WarriorStateMachine.StartCooldown(typeof(DashFirstAttackWarriorWarriorEnemyState),
                WarriorEnemyData.EnemyDashData.DashAttackCooldown);
            WarriorEnemy.NavMeshAgent.speed = WarriorEnemyData.EnemyDashData.DashSpeedModifier;
            WarriorEnemy.Animator.SetBool(
                WarriorEnemyAnimationData.DashAttackParameterHash, true);

            WarriorEnemy.NavMeshAgent.SetDestination(WarriorStateMachine.AliveEntity
                .transform.position + WarriorStateMachine.AliveEntity.transform.forward*10);
            _hasStopped = false;
            _startAttack = false;
        }
        
        public override void Update()
        {
            switch (_hasStopped)
            {
                case false when _startAttack:
                    WarriorEnemy.NavMeshAgent.isStopped = false;
                    break;
                case true:
                    Stop();
                    break;
            }
        }

        public override void Exit()
        {
            base.Exit();
            WarriorEnemy.Animator.SetBool(
                WarriorEnemyAnimationData.DashAttackParameterHash, false);
            WarriorStateMachine.AliveEntity.OrdinaryAttackColliderActivator.OnTargetHit -= Stop;
        }

        public override void TriggerOnStateAnimationEnterEvent()
        {
            _hasStopped = true;
        }
        
        public override void TriggerOnStateAnimationHandleEvent()
        {
            _startAttack = true;
        }
        
        public override void TriggerOnStateAnimationExitEvent()
        {
           
            WarriorStateMachine.ChangeState(WarriorStateMachine.FollowWarriorEnemyState);
        }
        
        private void Stop(AttackData attackData)
        {
            Stop();
            WarriorStateMachine.AliveEntity.OrdinaryAttackColliderActivator.DeactivateCollider();
        }
    }
}