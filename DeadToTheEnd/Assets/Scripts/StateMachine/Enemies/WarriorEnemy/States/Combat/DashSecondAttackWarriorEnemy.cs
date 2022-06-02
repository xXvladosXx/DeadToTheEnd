using StateMachine.Enemies.WarriorEnemy;
using StateMachine.Enemies.WarriorEnemy.States.Combat;
using UnityEngine;

namespace StateMachine.WarriorEnemy.States.Combat
{
    public class DashSecondAttackWarriorEnemy: BaseAttackEnemyState
    {
        private bool _hasStopped;
        private bool _startAttack;
        
        public DashSecondAttackWarriorEnemy(WarriorStateMachine warriorStateMachine) : base(warriorStateMachine)
        {
        }
        
        public override void Enter()
        {
            base.Enter();
            
            WarriorStateMachine.StartCooldown(typeof(DashSecondAttackWarriorEnemy),
                bossEnemyData.EnemyDashData.DashSecondAttackCooldown);
            Enemy.NavMeshAgent.speed = bossEnemyData.EnemyDashData.DashSecondSpeedModifierFirstPhase;
            Enemy.Animator.SetBool(
                WarriorEnemyAnimationData.DashSecondAttackParameterHash, true);

            Enemy.NavMeshAgent.SetDestination(Enemy
                .Target.transform.position - Enemy.transform.forward/2);
            _hasStopped = false;
            _startAttack = false;
        }
        
        public override void Update()
        {
            switch (_hasStopped)
            {
                case false when _startAttack:
                    Enemy.NavMeshAgent.isStopped = false;
                    break;
                case true:
                    Stop();
                    break;
            }
        }

        public override void OnAnimationEnterEvent()
        {
            TargetLocked();
            
            Enemy.NavMeshAgent.speed = bossEnemyData.EnemyDashData.DashSecondSpeedModifierSecondPhase;
            Enemy.NavMeshAgent.SetDestination(WarriorStateMachine.AliveEntity
                .transform.position + WarriorStateMachine.AliveEntity.transform.forward*2);
        }
        
        public override void OnAnimationHandleEvent()
        {
            _startAttack = true;
        }
        
        public override void OnAnimationExitEvent()
        {
            Enemy.Animator.SetBool(
                WarriorEnemyAnimationData.DashSecondAttackParameterHash, false);
            WarriorStateMachine.ChangeState(WarriorStateMachine.FollowWarriorEnemyState);
        }
    }
}