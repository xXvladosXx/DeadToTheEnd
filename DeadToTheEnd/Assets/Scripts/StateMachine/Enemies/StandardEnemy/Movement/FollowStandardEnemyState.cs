using System;
using System.Linq;
using StateMachine.Enemies.BaseStates;
using UnityEngine;
using Random = UnityEngine.Random;

namespace StateMachine.Enemies.BlueGragon.Movement
{
    public class FollowStandardEnemyState : StandardEnemyState
    {
        public FollowStandardEnemyState(StandardEnemyStateMachine stateMachine) : base(stateMachine)
        {
            
        }
        
        private float _curTime;
        private float _timeToWait;

        public override void Enter()
        {
            base.Enter();

            Enemy.NavMeshAgent.isStopped = true;
            _timeToWait = DecideTime(StandardEnemyData.EnemyWalkData.WalkMinTime,
                StandardEnemyData.EnemyWalkData.WalkMaxTime);
            
            _curTime = 0;
        }

        public override void Update()
        {
            base.Update();
            
            _curTime += Time.deltaTime;

            if (_curTime > _timeToWait)
            {
                DecideWhatToDo();
            }
        }

        private void DecideWhatToDo()
        {
            while (true)
            {
                int choice = Random.Range(0, 2);
                switch (choice)
                {
                    case 0:
                        StandardEnemyStateMachine.ChangeState(StandardEnemyStateMachine.ForwardStandardEnemyState);
                        break;
                    case 1:
                        DecideAttackToDo();
                        break;
                }
                
                break;
            }
        }
    }
}