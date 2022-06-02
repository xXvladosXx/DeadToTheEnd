using StateMachine.Enemies.WarriorEnemy;
using StateMachine.Enemies.WarriorEnemy.States.Combat;
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
                bossEnemyData.EnemyComboData.ComboFirstAttackCooldown);
            BossEnemy.NavMeshAgent.speed = bossEnemyData.EnemyComboData.ComboFirstAttackSpeed;
            BossEnemy.Animator.SetBool(WarriorEnemyAnimationData.ComboFirstParameterHash, true);
        }

        public override void OnAnimationEnterEvent()
        {
            BossEnemy.NavMeshAgent.isStopped = false;
        }

        public override void OnAnimationExitEvent()
        {
            BossEnemy.Animator.SetBool(WarriorEnemyAnimationData.ComboFirstParameterHash, false);
            WarriorStateMachine.ChangeState(WarriorStateMachine.FollowWarriorEnemyState);
        }

    }
}