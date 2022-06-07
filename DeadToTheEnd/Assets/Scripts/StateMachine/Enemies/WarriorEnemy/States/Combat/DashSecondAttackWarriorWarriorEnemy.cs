using StateMachine.Enemies.WarriorEnemy;
using StateMachine.Enemies.WarriorEnemy.States.Combat;
using UnityEngine;

namespace StateMachine.WarriorEnemy.States.Combat
{
    public class DashSecondAttackWarriorWarriorEnemy: BaseAttackWarriorEnemyState
    {
        private bool _hasStopped;
        private bool _startAttack;
        
        public DashSecondAttackWarriorWarriorEnemy(WarriorStateMachine warriorStateMachine) : base(warriorStateMachine)
        {
        }
        
        public override void Enter()
        {
            base.Enter();
            
            WarriorStateMachine.StartCooldown(typeof(DashSecondAttackWarriorWarriorEnemy),
                WarriorEnemyData.EnemyDashData.DashSecondAttackCooldown);
            Enemy.NavMeshAgent.speed = WarriorEnemyData.EnemyDashData.DashSecondSpeedModifierFirstPhase;
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

        public override void Exit()
        {
            base.Exit();
            Enemy.Animator.SetBool(
                WarriorEnemyAnimationData.DashSecondAttackParameterHash, false);
        }

        public override void OnAnimationEnterEvent()
        {
            TargetLocked();
            
            Enemy.NavMeshAgent.speed = WarriorEnemyData.EnemyDashData.DashSecondSpeedModifierSecondPhase;
            Enemy.NavMeshAgent.SetDestination(WarriorStateMachine.AliveEntity
                .transform.position + WarriorStateMachine.AliveEntity.transform.forward*2);
        }
        
        public override void OnAnimationHandleEvent()
        {
            _startAttack = true;
        }
        
        public override void OnAnimationExitEvent()
        {
           
            WarriorStateMachine.ChangeState(WarriorStateMachine.FollowWarriorEnemyState);
        }
    }
}