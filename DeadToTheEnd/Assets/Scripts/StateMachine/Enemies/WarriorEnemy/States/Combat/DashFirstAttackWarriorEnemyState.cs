using Data.Combat;
using Data.ScriptableObjects;
using Entities;
using StateMachine.Enemies.WarriorEnemy;
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
            WarriorStateMachine.AliveEntity.SwordColliderActivator.OnTargetHit += Stop;
            
            WarriorStateMachine.StartCooldown(typeof(DashFirstAttackWarriorEnemyState),
                bossEnemyData.EnemyDashData.DashAttackCooldown);
            BossEnemy.NavMeshAgent.speed = bossEnemyData.EnemyDashData.DashSpeedModifier;
            BossEnemy.Animator.SetBool(
                WarriorEnemyAnimationData.DashAttackParameterHash, true);

            BossEnemy.NavMeshAgent.SetDestination(WarriorStateMachine.AliveEntity
                .transform.position + WarriorStateMachine.AliveEntity.transform.forward*10);
            _hasStopped = false;
            _startAttack = false;
        }
        
        public override void Update()
        {
            switch (_hasStopped)
            {
                case false when _startAttack:
                    BossEnemy.NavMeshAgent.isStopped = false;
                    break;
                case true:
                    Stop();
                    break;
            }
        }

        public override void Exit()
        {
            base.Exit();
            WarriorStateMachine.AliveEntity.SwordColliderActivator.OnTargetHit -= Stop;
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
            BossEnemy.Animator.SetBool(
                WarriorEnemyAnimationData.DashAttackParameterHash, false);
            WarriorStateMachine.ChangeState(WarriorStateMachine.FollowWarriorEnemyState);
        }
        
        private void Stop(AttackData attackData)
        {
            Stop();
            WarriorStateMachine.AliveEntity.SwordColliderActivator.DeactivateCollider();
        }
    }
}