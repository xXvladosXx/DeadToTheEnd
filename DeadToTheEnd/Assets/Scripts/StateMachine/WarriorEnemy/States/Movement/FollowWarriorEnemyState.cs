using Data.ScriptableObjects;
using Entities;
using UnityEngine;

namespace StateMachine.WarriorEnemy.States.Movement
{
    public class FollowWarriorEnemyState : IState
    {
        private readonly WarriorStateMachine _warriorStateMachine;
        private readonly WarriorEnemyData _warriorEnemyData;
        
        private Vector3 _pointToMoveTo;

        public FollowWarriorEnemyState(WarriorStateMachine warriorStateMachine)
        {
            _warriorStateMachine = warriorStateMachine;
            
            _warriorEnemyData = _warriorStateMachine.WarriorEnemy.WarriorEnemyData;
        }
        
        public void Enter()
        {
        }

        public void Exit()
        {
           
        }

        public void HandleInput()
        {
           
        }

        public void Update()
        {
            Debug.Log("Following");
        }

        public void FixedUpdate()
        {
           
        }

    }
}