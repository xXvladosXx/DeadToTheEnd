using AnimatorStateMachine.Combat;
using StateMachine.WarriorEnemy.States.Movement;
using UnityEngine;
using UnityEngine.EventSystems;

namespace StateMachine.WarriorEnemy.States.Combat
{
    public class ComboFirstWarriorEnemyState : BaseAttackEnemyState
    {
        public ComboFirstWarriorEnemyState(WarriorStateMachine warriorStateMachine) : base(warriorStateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();
            
            WarriorStateMachine.StartCooldown(typeof(ComboFirstWarriorEnemyState),
                WarriorEnemyData.EnemyComboData.ComboFirstAttackCooldown);
            WarriorStateMachine.WarriorEnemy.NavMeshAgent.speed = WarriorEnemyData.EnemyComboData.ComboFirstAttackSpeed;
            WarriorStateMachine.WarriorEnemy.Animator.SetBool(WarriorEnemyAnimationData.ComboFirstParameterHash, true);
        }

        public override void OnAnimationEnterEvent()
        {
            WarriorStateMachine.WarriorEnemy.NavMeshAgent.isStopped = false;
        }

        public override void OnAnimationExitEvent()
        {
            WarriorStateMachine.WarriorEnemy.Animator.SetBool(WarriorEnemyAnimationData.ComboFirstParameterHash, false);
            WarriorStateMachine.ChangeState(WarriorStateMachine.FollowWarriorEnemyState);
        }

    }
}