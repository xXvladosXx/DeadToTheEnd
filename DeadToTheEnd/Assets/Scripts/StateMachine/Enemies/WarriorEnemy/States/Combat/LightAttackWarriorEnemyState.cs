using Data.ScriptableObjects;
using StateMachine.Enemies.WarriorEnemy;
using StateMachine.Enemies.WarriorEnemy.States.Combat;
using StateMachine.WarriorEnemy.States.Movement;
using UnityEngine;

namespace StateMachine.WarriorEnemy.States.Combat
{
    public class LightAttackWarriorEnemyState : BaseAttackEnemyState, IState
    {
        public LightAttackWarriorEnemyState(WarriorStateMachine warriorStateMachine) : base(warriorStateMachine)
        {
        }
        
        public override void Enter()
        {
            base.Enter();
            
            Enemy.NavMeshAgent.speed = bossEnemyData.EnemyComboData.ComboFirstAttackSpeed;
            Enemy.Animator.SetBool(
                BossEnemy.WarriorEnemyAnimationData.LightAttackParameterHash, true);
        }

        public override void OnAnimationEnterEvent()
        {
            Enemy.NavMeshAgent.isStopped = false;
        }
        
        public override void Update()
        {
            TargetLocked();
        }

        public override void OnAnimationExitEvent()
        {
            WarriorStateMachine.ChangeState(WarriorStateMachine.FollowWarriorEnemyState);
            Enemy.Animator.SetBool(
                BossEnemy.WarriorEnemyAnimationData.LightAttackParameterHash, false);
        }
        
        
    }
}