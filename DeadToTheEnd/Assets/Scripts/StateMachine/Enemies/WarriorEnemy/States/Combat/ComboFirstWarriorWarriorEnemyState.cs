using StateMachine.Core;
using StateMachine.Enemies.WarriorEnemy;
using StateMachine.Enemies.WarriorEnemy.States.Combat;
using StateMachine.WarriorEnemy.States.Movement;
using UnityEngine;
using UnityEngine.EventSystems;

namespace StateMachine.WarriorEnemy.States.Combat
{
    public class ComboFirstWarriorWarriorEnemyState : BaseAttackWarriorEnemyState
    {
        public ComboFirstWarriorWarriorEnemyState(WarriorStateMachine warriorStateMachine) : base(warriorStateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();
            
            WarriorStateMachine.StartCooldown(WarriorStateMachine.ComboFirstWarriorWarriorEnemyState,
                WarriorEnemyData.EnemyComboData.ComboFirstAttackCooldown);
            WarriorEnemy.NavMeshAgent.speed = WarriorEnemyData.EnemyComboData.ComboFirstAttackSpeed;
            WarriorEnemy.Animator.SetBool(WarriorEnemyAnimationData.ComboFirstParameterHash, true);
        }

        public override void Exit()
        {
            base.Exit();
            WarriorEnemy.Animator.SetBool(WarriorEnemyAnimationData.ComboFirstParameterHash, false);
        }

        public override void TriggerOnStateAnimationEnterEvent()
        {
            WarriorEnemy.NavMeshAgent.isStopped = false;
        }

        public override void TriggerOnStateAnimationExitEvent()
        {
            WarriorStateMachine.ChangeState(WarriorStateMachine.FollowWarriorEnemyState);
        }

    }
}