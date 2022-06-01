using Data.ScriptableObjects;
using StateMachine.Enemies.WarriorEnemy;
using UnityEngine;

namespace StateMachine.WarriorEnemy.States.Movement
{
    public class PatrolWarriorEnemyState: BaseEnemyState, IState
    {
        private Vector3 _pointToMoveTo;

        public PatrolWarriorEnemyState(WarriorStateMachine warriorStateMachine) : base(warriorStateMachine)
        {
            }
       
    }
}