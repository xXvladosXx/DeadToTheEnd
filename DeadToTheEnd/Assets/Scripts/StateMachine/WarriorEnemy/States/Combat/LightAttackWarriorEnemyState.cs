using Data.ScriptableObjects;
using StateMachine.WarriorEnemy.States.Movement;
using UnityEngine;

namespace StateMachine.WarriorEnemy.States.Combat
{
    public class LightAttackWarriorEnemyState : BaseMovementEnemyState, IState
    {
        private readonly WarriorStateMachine _warriorStateMachine;
        private readonly WarriorEnemyData _warriorEnemyData;
        
        public LightAttackWarriorEnemyState(WarriorStateMachine warriorStateMachine) : base(warriorStateMachine)
        {
            _warriorStateMachine = warriorStateMachine;
            
            _warriorEnemyData = _warriorStateMachine.WarriorEnemy.WarriorEnemyData;
        }
        
        public override void Enter()
        {
            Stop(_warriorStateMachine.WarriorEnemy.NavMeshAgent);
            
            _warriorStateMachine.WarriorEnemy.Animator.SetBool(
                _warriorStateMachine.WarriorEnemy.WarriorEnemyAnimationData.LightAttackParameterHash, true);
            
            Debug.Log("Made light Attack");
        }

        public override void Exit()
        {
           
        }

        public override void HandleInput()
        {
           
        }

        public override void Update()
        {
            TargetLocked();
        }

        public override void FixedUpdate()
        {
           
        }

        public override void OnAnimationEnterEvent()
        {
            
        }

        public override void OnAnimationExitEvent()
        {
            _warriorStateMachine.ChangeState(_warriorStateMachine.FollowWarriorEnemyState);
            _warriorStateMachine.WarriorEnemy.Animator.SetBool(
                _warriorStateMachine.WarriorEnemy.WarriorEnemyAnimationData.LightAttackParameterHash, false);
        }
        
        private void TargetLocked()
        {
            Transform transform;
            (transform = _warriorStateMachine.WarriorEnemy.transform).LookAt(_warriorStateMachine.WarriorEnemy.MainPlayer.transform);
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
        }
    }
}