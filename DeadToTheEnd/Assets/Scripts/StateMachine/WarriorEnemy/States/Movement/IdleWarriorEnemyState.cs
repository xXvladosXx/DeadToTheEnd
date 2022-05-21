using Data.Animations;
using Data.ScriptableObjects;
using UnityEngine;

namespace StateMachine.WarriorEnemy.States.Movement
{
    public class IdleWarriorEnemyState : BaseMovementEnemyState
    {
        private float _curTime;
        private bool _randomDestinationSet;

        public IdleWarriorEnemyState(WarriorStateMachine warriorStateMachine) : base(warriorStateMachine)
        {
            
        }

        public override void Enter()
        {
            WarriorStateMachine.WarriorEnemy.NavMeshAgent.speed = 1;
            _curTime = 0f;
        }


        public override void Update()
        {
            _curTime += Time.deltaTime;
            if(_curTime < WarriorEnemyData.EnemyIdleData.TimeOfIdlePositioning) return;

            if (IsEnoughDistance(WarriorEnemyData.EnemyIdleData.DistanceToFindTarget,
                    WarriorStateMachine.WarriorEnemy.transform,
                    WarriorStateMachine.WarriorEnemy.MainPlayer.transform))
            {
                base.Update();
            }
        }
    }
}