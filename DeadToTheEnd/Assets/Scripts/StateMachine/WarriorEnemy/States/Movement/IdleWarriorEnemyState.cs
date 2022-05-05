using Data.ScriptableObjects;
using UnityEngine;

namespace StateMachine.WarriorEnemy.States.Movement
{
    public class IdleWarriorEnemyState : IState
    {
        private readonly WarriorEnemyData _warriorEnemyData;
        private readonly WarriorStateMachine _warriorStateMachine;

        private float _curTime;

        public IdleWarriorEnemyState(WarriorStateMachine warriorStateMachine)
        {
            _warriorStateMachine = warriorStateMachine;

            _warriorEnemyData = _warriorStateMachine.WarriorEnemy.WarriorEnemyData;
        }

        public void Enter()
        {
            _warriorStateMachine.WarriorEnemy.NavMeshAgent.speed = _warriorEnemyData.EnemyIdleData.IdleSpeedModifer;
            _curTime = 0f;
        }

        public void Exit()
        {
           
        }

        public void HandleInput()
        {
           
        }

        public void Update()
        {
            Debug.Log("waiting..." + _curTime);
            _curTime += Time.deltaTime;
            if (_curTime > _warriorEnemyData.EnemyIdleData.TimeOfIdlePositioning)
            {
                _warriorStateMachine.ChangeState(_warriorStateMachine.PatrolWarriorEnemyState);
            }
        }

        public void FixedUpdate()
        {
           
        }

    }
}