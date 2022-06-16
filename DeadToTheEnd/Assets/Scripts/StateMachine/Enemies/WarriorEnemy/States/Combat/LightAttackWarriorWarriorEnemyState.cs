using Data.ScriptableObjects;
using StateMachine.Enemies.WarriorEnemy;
using StateMachine.Enemies.WarriorEnemy.States.Combat;
using StateMachine.WarriorEnemy.States.Movement;
using UnityEngine;

namespace StateMachine.WarriorEnemy.States.Combat
{
    public class LightAttackWarriorWarriorEnemyState : BaseAttackWarriorEnemyState, IState
    {
        public LightAttackWarriorWarriorEnemyState(WarriorStateMachine warriorStateMachine) : base(warriorStateMachine)
        {
        }
        
        public override void Enter()
        {
            base.Enter();

            WarriorStateMachine.StartCooldown(typeof(LightAttackWarriorWarriorEnemyState), 
                WarriorEnemyData.EnemyDashData.DashAttackCooldown);

            Enemy.NavMeshAgent.speed = WarriorEnemyData.EnemyComboData.ComboFirstAttackSpeed;
            Enemy.Animator.SetBool(WarriorEnemyAnimationData.LightAttackParameterHash, true);
        }

        public override void Exit()
        {
            base.Exit();
            Enemy.Animator.SetBool(WarriorEnemyAnimationData.LightAttackParameterHash, false);
        }

        public override void TriggerOnStateAnimationEnterEvent()
        {
            Enemy.NavMeshAgent.isStopped = false;
        }
        
        public override void Update()
        {
            TargetLocked();
        }

        public override void TriggerOnStateAnimationExitEvent()
        {
            WarriorStateMachine.ChangeState(WarriorStateMachine.FollowWarriorEnemyState);
        }
        
        
    }
}