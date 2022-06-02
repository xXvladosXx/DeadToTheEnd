using StateMachine.Enemies.WarriorEnemy;
using StateMachine.Enemies.WarriorEnemy.States.Combat;

namespace StateMachine.WarriorEnemy.States.Combat
{
    public class ComboSecondWarriorEnemyState: BaseAttackEnemyState
    {
        public ComboSecondWarriorEnemyState(WarriorStateMachine warriorStateMachine) : base(warriorStateMachine)
        {
        }

        public override void Enter()
        {
            BossEnemy.EnemyStateReusableData.IsPerformingAction = true;
            Enemy.NavMeshAgent.isStopped = true;

            TargetLocked();
            Enemy.NavMeshAgent.SetDestination(WarriorStateMachine.AliveEntity
                .transform.position + WarriorStateMachine.AliveEntity.transform.forward * 10); 
            
            WarriorStateMachine.StartCooldown(typeof(ComboSecondWarriorEnemyState),
                bossEnemyData.EnemyComboData.ComboSecondAttackCooldown);
            Enemy.NavMeshAgent.speed = bossEnemyData.EnemyComboData.ComboSecondAttackSpeed;
            Enemy.Animator.SetBool(WarriorEnemyAnimationData.ComboSecondParameterHash, true);
        }

        public override void OnAnimationEnterEvent()
        {
            Enemy.NavMeshAgent.isStopped = false;
        }

        public override void OnAnimationExitEvent()
        {
            Enemy.Animator.SetBool(WarriorEnemyAnimationData.ComboSecondParameterHash, false);
            WarriorStateMachine.ChangeState(WarriorStateMachine.FollowWarriorEnemyState);
        }
    }
}