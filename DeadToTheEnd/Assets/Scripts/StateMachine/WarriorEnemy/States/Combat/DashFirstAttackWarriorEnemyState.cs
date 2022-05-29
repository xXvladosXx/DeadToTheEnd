using Data.Combat;
using Data.ScriptableObjects;
using Entities;
using StateMachine.WarriorEnemy.States.Movement;
using UnityEngine;

namespace StateMachine.WarriorEnemy.States.Combat
{
    public class DashFirstAttackWarriorEnemyState : BaseAttackEnemyState
    {
        private bool _hasStopped;
        private bool _startAttack;

        public DashFirstAttackWarriorEnemyState(WarriorStateMachine warriorStateMachine) : base(warriorStateMachine)
        {
           
        }

        public override void Enter()
        {
            base.Enter();
            WarriorStateMachine.WarriorEnemy.LongSwordColliderActivator.OnTargetHit += Stop;
            
            WarriorStateMachine.StartCooldown(typeof(DashFirstAttackWarriorEnemyState),
                WarriorEnemyData.EnemyDashData.DashAttackCooldown);
            WarriorStateMachine.WarriorEnemy.NavMeshAgent.speed = WarriorEnemyData.EnemyDashData.DashSpeedModifier;
            WarriorStateMachine.WarriorEnemy.Animator.SetBool(
                WarriorEnemyAnimationData.DashAttackParameterHash, true);

            WarriorStateMachine.WarriorEnemy.NavMeshAgent.SetDestination(WarriorStateMachine.WarriorEnemy
                .transform.position + WarriorStateMachine.WarriorEnemy.transform.forward*10);
            _hasStopped = false;
            _startAttack = false;
        }
        
        public override void Update()
        {
            switch (_hasStopped)
            {
                case false when _startAttack:
                    WarriorStateMachine.WarriorEnemy.NavMeshAgent.isStopped = false;
                    break;
                case true:
                    Stop();
                    break;
            }
        }

        public override void Exit()
        {
            base.Exit();
            WarriorStateMachine.WarriorEnemy.LongSwordColliderActivator.OnTargetHit -= Stop;
        }

        public override void OnAnimationEnterEvent()
        {
            _hasStopped = true;
        }
        
        public override void OnAnimationHandleEvent()
        {
            _startAttack = true;
        }
        
        public override void OnAnimationExitEvent()
        {
            WarriorStateMachine.WarriorEnemy.Animator.SetBool(
                WarriorEnemyAnimationData.DashAttackParameterHash, false);
            WarriorStateMachine.ChangeState(WarriorStateMachine.FollowWarriorEnemyState);
        }
        
        private void Stop(AttackData attackData)
        {
            Stop();
            WarriorStateMachine.WarriorEnemy.LongSwordColliderActivator.DeactivateCollider();
        }
    }
}