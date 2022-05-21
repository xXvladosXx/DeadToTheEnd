namespace StateMachine.WarriorEnemy.States.Combat
{
    public class ComboSecondWarriorEnemyState: BaseAttackEnemyState
    {
        public ComboSecondWarriorEnemyState(WarriorStateMachine warriorStateMachine) : base(warriorStateMachine)
        {
        }

        public override void Enter()
        {
            WarriorStateMachine.WarriorEnemy.EnemyStateReusableData.IsPerformingAction = true;
            WarriorStateMachine.WarriorEnemy.NavMeshAgent.isStopped = true;

            TargetLocked();
            WarriorStateMachine.WarriorEnemy.NavMeshAgent.SetDestination(WarriorStateMachine.WarriorEnemy
                .transform.position + WarriorStateMachine.WarriorEnemy.transform.forward * 10); 
            
            WarriorStateMachine.StartCooldown(typeof(ComboSecondWarriorEnemyState),
                WarriorEnemyData.EnemyComboData.ComboSecondAttackCooldown);
            WarriorStateMachine.WarriorEnemy.NavMeshAgent.speed = WarriorEnemyData.EnemyComboData.ComboSecondAttackSpeed;
            WarriorStateMachine.WarriorEnemy.Animator.SetBool(WarriorEnemyAnimationData.ComboSecondParameterHash, true);
        }

        public override void OnAnimationEnterEvent()
        {
            WarriorStateMachine.WarriorEnemy.NavMeshAgent.isStopped = false;
        }

        public override void OnAnimationExitEvent()
        {
            WarriorStateMachine.WarriorEnemy.Animator.SetBool(WarriorEnemyAnimationData.ComboSecondParameterHash, false);
            WarriorStateMachine.ChangeState(WarriorStateMachine.FollowWarriorEnemyState);
        }
    }
}