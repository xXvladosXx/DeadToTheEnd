using Data.Animations;
using Data.ScriptableObjects;
using StateMachine.Enemies.WarriorEnemy;
using UnityEngine;

namespace StateMachine.WarriorEnemy.States.Movement
{
    public class IdleWarriorEnemyState : BaseWarriorEnemyState
    {
        private float _curTime;
        private bool _randomDestinationSet;

        public IdleWarriorEnemyState(WarriorStateMachine warriorStateMachine) : base(warriorStateMachine)
        {
            
        }

        public override void Enter()
        {
            BossEnemy.NavMeshAgent.speed = 1;
            _curTime = 0f;
        }


        public override void Update()
        {
            _curTime += Time.deltaTime;
            if(_curTime < bossEnemyData.EnemyIdleData.TimeOfIdlePositioning) return;

            if (IsEnoughDistance(bossEnemyData.EnemyIdleData.DistanceToFindTarget,
                    BossEnemy.transform,
                    BossEnemy.Target.transform))
            {
                base.Update();
            }
        }
    }
}