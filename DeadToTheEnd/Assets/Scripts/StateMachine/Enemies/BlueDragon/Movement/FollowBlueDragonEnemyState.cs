using System;
using System.Linq;
using StateMachine.Enemies.BaseStates;
using UnityEngine;
using Random = UnityEngine.Random;

namespace StateMachine.Enemies.BlueGragon.Movement
{
    public class FollowBlueDragonEnemyState : BaseBlueDragonEnemyState
    {
        public FollowBlueDragonEnemyState(BlueDragonStateMachine stateMachine) : base(stateMachine)
        {
            
        }
        
        private float _curTime;
        private float _timeToWait;

        public override void Enter()
        {
            base.Enter();

            Enemy.NavMeshAgent.isStopped = true;
            _timeToWait = DecideTime(BlueDragonEnemyData.EnemyWalkData.WalkMinTime,
                BlueDragonEnemyData.EnemyWalkData.WalkMaxTime);
            
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
                        BlueDragonStateMachine.ChangeState(BlueDragonStateMachine.ForwardBlueDragonEnemyState);
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