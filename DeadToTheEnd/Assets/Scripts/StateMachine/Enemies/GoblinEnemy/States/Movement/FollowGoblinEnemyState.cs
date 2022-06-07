using UnityEngine;

namespace StateMachine.Enemies.GoblinEnemy.States.Movement
{
    public class FollowGoblinEnemyState : BaseGoblinEnemyState
    {
        private Vector3 _pointToMoveTo;
        private float _timeToWaitMovement;
        private float _timeToWaitAttack;
        private float _curTime;

        public FollowGoblinEnemyState(GoblinStateMachine stateMachine) : base(stateMachine)
        {
        }
        
        public override void Enter()
        {
            base.Enter();
            _curTime = 0;
            _timeToWaitMovement = TimeToWaitMovement();
        }

        public override void Exit()
        {
            base.Exit();

            GoblinEnemy.Animator.SetFloat(GoblinEnemyAnimationData.VerticalParameterHash, 0, .1f, Time.deltaTime);
        }
        
        public override void Update()
        {
            _curTime += Time.deltaTime;
            base.Update();
            
            if (_curTime > _timeToWaitMovement)
            {
                DecideWhatToDo();
            }
        }
        
        private float TimeToWaitMovement()
        {
            return Random.Range(GoblinEnemy.GoblinEnemyData.GoblinFollowData.MinTimeToWait, 
                GoblinEnemy.GoblinEnemyData.GoblinFollowData.MaxTimeToWait);
        }
        private void DecideWhatToDo()
        {
            while (true)
            {
                int choice = Random.Range(0, 3);
                switch (choice)
                {
                    case 0:
                        GoblinStateMachine.ChangeState(GoblinStateMachine.ForwardMoveGoblinEnemyState);
                        break;
                    case 2:
                        DecideAttackToDo();
                        break;

                    case 1:
                        if (IsEnoughDistance(GoblinEnemy.GoblinEnemyData.GoblinFollowData.DistanceToRoll,
                                GoblinStateMachine.AliveEntity.transform, 
                                GoblinEnemy.Target.transform))
                        {
                            GoblinStateMachine.ChangeState(GoblinStateMachine.BaseRollEnemyState);
                            break;
                        }

                        continue;
                }

                break;
            }
        }

        
    }
}