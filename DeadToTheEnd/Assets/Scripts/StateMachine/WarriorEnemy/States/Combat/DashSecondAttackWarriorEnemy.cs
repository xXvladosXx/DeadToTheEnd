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
                WarriorEnemyData.EnemyDashData.DashSecondAttackCooldown);
            WarriorStateMachine.WarriorEnemy.NavMeshAgent.speed = WarriorEnemyData.EnemyDashData.DashSecondSpeedModifierFirstPhase;
            WarriorStateMachine.WarriorEnemy.Animator.SetBool(
                WarriorEnemyAnimationData.DashSecondAttackParameterHash, true);

            WarriorStateMachine.WarriorEnemy.NavMeshAgent.SetDestination(WarriorStateMachine.WarriorEnemy
                .MainPlayer.transform.position - WarriorStateMachine.WarriorEnemy.transform.forward/2);
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

        public override void OnAnimationEnterEvent()
        {
            TargetLocked();
            
            WarriorStateMachine.WarriorEnemy.NavMeshAgent.speed = WarriorEnemyData.EnemyDashData.DashSecondSpeedModifierSecondPhase;
            WarriorStateMachine.WarriorEnemy.NavMeshAgent.SetDestination(WarriorStateMachine.WarriorEnemy
                .transform.position + WarriorStateMachine.WarriorEnemy.transform.forward*2);
        }
        
        public override void OnAnimationHandleEvent()
        {
            _startAttack = true;
        }
        
        public override void OnAnimationExitEvent()
        {
            WarriorStateMachine.WarriorEnemy.Animator.SetBool(
                WarriorEnemyAnimationData.DashSecondAttackParameterHash, false);
            WarriorStateMachine.ChangeState(WarriorStateMachine.FollowWarriorEnemyState);
        }
    }
}