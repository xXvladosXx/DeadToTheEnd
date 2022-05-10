using Data.ScriptableObjects;
using UnityEngine;

namespace StateMachine.WarriorEnemy.States.Movement
{
    public class PatrolWarriorEnemyState: BaseMovementEnemyState, IState
    {
        private Vector3 _pointToMoveTo;

        public PatrolWarriorEnemyState(WarriorStateMachine warriorStateMachine) : base(warriorStateMachine)
        {
            WarriorStateMachine = warriorStateMachine;
            
            WarriorEnemyData = WarriorStateMachine.WarriorEnemy.WarriorEnemyData;
        }
        public override void Enter()
        {
            WarriorStateMachine.WarriorEnemy.NavMeshAgent.speed = WarriorEnemyData.EnemyWalkData.WalkSpeedModifer;

            FindRandomPointInRadius();
          
        }
        public override void Exit()
        {
           
        }

        public override void HandleInput()
        {
           
        }

        public override void Update()
        {
            MoveTo(WarriorStateMachine.WarriorEnemy.NavMeshAgent, _pointToMoveTo);
            if(IsEnoughDistance(WarriorEnemyData.EnemyPatrolData.DistanceToFindTarget,WarriorStateMachine.WarriorEnemy.MainPlayer.transform,
                WarriorStateMachine.WarriorEnemy.transform))
            {
                WarriorStateMachine.ChangeState(WarriorStateMachine.FollowWarriorEnemyState);
                return;
            }
            
            if (Vector3.Distance(_pointToMoveTo, WarriorStateMachine.WarriorEnemy.transform.position) < 1f)
            {
                WarriorStateMachine.ChangeState(WarriorStateMachine.IdleWarriorEnemyState);
            }
        }

        public override void FixedUpdate()
        {
           
        }

        public override void OnAnimationEnterEvent()
        {
            
        }

        public override void OnAnimationExitEvent()
        {
            
        }

        private void FindRandomPointInRadius()
        {
            var center = WarriorStateMachine.WarriorEnemy.EnemyStateReusableData.StartPosition;
            var radius = WarriorEnemyData.EnemyPatrolData.RadiusToPatrol;

            _pointToMoveTo = center + (radius * UnityEngine.Random.insideUnitSphere);
        }
    }
}