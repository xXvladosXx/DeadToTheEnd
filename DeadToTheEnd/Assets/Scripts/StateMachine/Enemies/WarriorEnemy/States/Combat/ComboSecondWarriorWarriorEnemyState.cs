using StateMachine.Enemies.WarriorEnemy;
using StateMachine.Enemies.WarriorEnemy.States.Combat;

namespace StateMachine.WarriorEnemy.States.Combat
{
    public class ComboSecondWarriorWarriorEnemyState: BaseAttackWarriorEnemyState
    {
        public ComboSecondWarriorWarriorEnemyState(WarriorStateMachine warriorStateMachine) : base(warriorStateMachine)
        {
        }

        public override void Enter()
        {
            WarriorEnemy.WarriorStateReusableData.IsPerformingAction = true;
            Enemy.NavMeshAgent.isStopped = true;

            TargetLocked();
            Enemy.NavMeshAgent.SetDestination(WarriorStateMachine.AliveEntity
                .transform.position + WarriorStateMachine.AliveEntity.transform.forward * 10); 
            
            WarriorStateMachine.StartCooldown(typeof(ComboSecondWarriorWarriorEnemyState),
                WarriorEnemyData.EnemyComboData.ComboSecondAttackCooldown);
            Enemy.NavMeshAgent.speed = WarriorEnemyData.EnemyComboData.ComboSecondAttackSpeed;
            Enemy.Animator.SetBool(WarriorEnemyAnimationData.ComboSecondParameterHash, true);
        }

        public override void Exit()
        {
            base.Exit();
            Enemy.Animator.SetBool(WarriorEnemyAnimationData.ComboSecondParameterHash, false);
        }
        public override void OnAnimationEnterEvent()
        {
            Enemy.NavMeshAgent.isStopped = false;
        }

        public override void OnAnimationExitEvent()
        {
            WarriorStateMachine.ChangeState(WarriorStateMachine.FollowWarriorEnemyState);
        }
    }
}