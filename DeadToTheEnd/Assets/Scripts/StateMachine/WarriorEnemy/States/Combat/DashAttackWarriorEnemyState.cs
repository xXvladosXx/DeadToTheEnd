using Data.ScriptableObjects;
using StateMachine.WarriorEnemy.States.Movement;
using UnityEngine;

namespace StateMachine.WarriorEnemy.States.Combat
{
    public class DashAttackWarriorEnemyState: BaseMovementEnemyState, IState
    {
        private readonly WarriorStateMachine _warriorStateMachine;
        private readonly WarriorEnemyData _warriorEnemyData;
        private bool _hasStopped;

        public DashAttackWarriorEnemyState(WarriorStateMachine warriorStateMachine) : base(warriorStateMachine)
        {
            _warriorStateMachine = warriorStateMachine;
            
            _warriorEnemyData = _warriorStateMachine.WarriorEnemy.WarriorEnemyData;
        }
        public override void Enter()
        {
            if (_warriorStateMachine.StatesCooldown.ContainsKey(this))
            {
                _warriorStateMachine.ChangeState(_warriorStateMachine.FollowWarriorEnemyState);
                return;
            }

            _hasStopped = false;
            _warriorStateMachine.StartCooldown(this, _warriorStateMachine.WarriorEnemy.EnemyStateReusableData.DashAttackCooldown);
            _warriorStateMachine.WarriorEnemy.NavMeshAgent.isStopped = false;
            _warriorStateMachine.WarriorEnemy.Animator.SetBool(
                WarriorEnemyAnimationData.DashAttackParameterHash, true); 
        }

        public override void Exit()
        {
            _warriorStateMachine.WarriorEnemy.Animator.SetBool(
                WarriorEnemyAnimationData.DashAttackParameterHash, false);
        }

      

        public override void Update()
        {
            if(!_hasStopped)           
                _warriorStateMachine.WarriorEnemy.NavMeshAgent.isStopped = false;
        }

        public override void OnAnimationEnterEvent()
        {
            _hasStopped = true;
            _warriorStateMachine.WarriorEnemy.NavMeshAgent.isStopped = true;
        }

        public override void OnAnimationExitEvent()
        {
            _warriorStateMachine.WarriorEnemy.Animator.SetBool(
                WarriorEnemyAnimationData.DashAttackParameterHash, false);
            _warriorStateMachine.ChangeState(_warriorStateMachine.FollowWarriorEnemyState);
        }
        
    }
}