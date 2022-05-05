using Data.ScriptableObjects;
using UnityEngine;

namespace StateMachine.WarriorEnemy.States.Movement
{
    public class PatrolWarriorEnemyState: IState
    {
        private readonly WarriorStateMachine _warriorStateMachine;
        private readonly WarriorEnemyData _warriorEnemyData;
        
        private Vector3 _pointToMoveTo;

        public PatrolWarriorEnemyState(WarriorStateMachine warriorStateMachine)
        {
            _warriorStateMachine = warriorStateMachine;
            
            _warriorEnemyData = _warriorStateMachine.WarriorEnemy.WarriorEnemyData;
        }
        public void Enter()
        {
            _warriorStateMachine.WarriorEnemy.NavMeshAgent.speed = _warriorEnemyData.EnemyWalkData.WalkSpeedModifer;

            FindRandomPointInRadius();
            
            _warriorStateMachine.WarriorEnemy.NavMeshAgent.destination = _pointToMoveTo;
            _warriorStateMachine.WarriorEnemy.NavMeshAgent.isStopped = false;
        }
        public void Exit()
        {
           
        }

        public void HandleInput()
        {
           
        }

        public void Update()
        {
            if (Vector3.Distance(_warriorStateMachine.WarriorEnemy.MainPlayer.transform.position,
                    _warriorStateMachine.WarriorEnemy.transform.position) <
                _warriorEnemyData.EnemyPatrolData.DistanceToFindTarget)
            {
                _warriorStateMachine.ChangeState(_warriorStateMachine.FollowWarriorEnemyState);
                return;
            }
            
            if (Vector3.Distance(_pointToMoveTo, _warriorStateMachine.WarriorEnemy.transform.position) < 1)
            {
                _warriorStateMachine.ChangeState(_warriorStateMachine.IdleWarriorEnemyState);
            }
        }

        public void FixedUpdate()
        {
           
        }
        
        private void FindRandomPointInRadius()
        {
            var center = _warriorStateMachine.WarriorEnemy.EnemyStateReusableData.StartPosition;
            var radius = _warriorEnemyData.EnemyPatrolData.RadiusToPatrol;

            _pointToMoveTo = center + (radius * UnityEngine.Random.insideUnitSphere);
            _pointToMoveTo.y = 0;
        }
    }
}