using Data.ScriptableObjects;
using Entities;
using UnityEngine;

namespace StateMachine.WarriorEnemy.States.Movement
{
    public class FollowWarriorEnemyState : BaseMovementEnemyState
    {
        private Vector3 _pointToMoveTo;
        private float _timeToWait;
        private float _curTime;

        public FollowWarriorEnemyState(WarriorStateMachine warriorStateMachine) : base(warriorStateMachine)
        {
            
        }
        
        public override void Enter()
        {
            _curTime = 0;
            WarriorStateMachine.WarriorEnemy.NavMeshAgent.isStopped = true;
            _timeToWait = TimeToWait();
        }

        public override void Exit()
        {
            WarriorStateMachine.WarriorEnemy.Animator.SetFloat(WarriorEnemyAnimationData.VerticalParameterHash, 0, .1f, Time.deltaTime);
        }

        public override void Update()
        {
            _curTime += Time.deltaTime;
            base.Update();
           //  if (_curTime > 1)
           //  {
           //      WarriorStateMachine.ChangeState(WarriorStateMachine.RollBackWarriorEnemyState);
           //  }
           // return;
            if(_curTime > _timeToWait)
                DecideWhatToDo();
        }

        private float TimeToWait()
        {
            return Random.Range(.5f, 1.5f);
        }
        
        private void DecideWhatToDo()
        {
            int choice = Random.Range(0, 2);
            switch (choice)
            {
                case 0:
                    WarriorStateMachine.ChangeState(WarriorStateMachine.ForwardMoveWarriorEnemyState);
                    WarriorStateMachine.WarriorEnemy.EnemyStateReusableData.CanStrafe = true;
                    break;
                case 1:
                    if (WarriorStateMachine.WarriorEnemy.EnemyStateReusableData.CanStrafe)
                    {
                        WarriorStateMachine.ChangeState(WarriorStateMachine.StrafeMoveWarriorEnemyState);
                        break;
                    }
                    
                    DecideWhatToDo();
                    break;
            }
        }
    }
}