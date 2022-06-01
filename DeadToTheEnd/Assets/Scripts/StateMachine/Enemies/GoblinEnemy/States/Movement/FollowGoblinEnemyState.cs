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
            
           // _timeToWaitAttack = TimeToWaitAttack();
        }

        public override void Exit()
        {
            base.Exit();
            GoblinEnemy.Animator.SetFloat(GoblinEnemyAnimationData.VerticalParameterHash, 0, .1f, Time.deltaTime);
        }
        
        public override void Update()
        {
            if(GoblinEnemy.EnemyStateReusableData.IsPerformingAction) return;
            _curTime += Time.deltaTime;
            base.Update();

            if (_curTime > _timeToWaitMovement)
            {
                DecideWhatToDo();
                return;
            }

            if (_curTime > _timeToWaitAttack)
            {
               // DecideAttackToDo();
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
                int choice = Random.Range(0, 2);
                switch (choice)
                {
                    case 0:
                        GoblinStateMachine.ChangeState(GoblinStateMachine.ForwardMoveGoblinEnemyState);
                        GoblinEnemy.EnemyStateReusableData.CanStrafe = true;
                        break;
                    /*case 2:
                        if (!IsEnoughDistance(3, GoblinStateMachine.AliveEntity.transform, 
                                GoblinEnemy.MainPlayer.transform) &&
                            GoblinEnemy.EnemyStateReusableData.CanStrafe)
                        {
                            GoblinStateMachine.ChangeState(GoblinStateMachine.StrafeMoveWarriorEnemyState);
                            break;
                        }

                        continue;*/
                    case 1:
                        if (IsEnoughDistance(GoblinEnemy.GoblinEnemyData.GoblinFollowData.DistanceToRoll,
                                GoblinStateMachine.AliveEntity.transform, 
                                GoblinEnemy.MainPlayer.transform))
                        {
                            GoblinStateMachine.ChangeState(GoblinStateMachine.RollGoblinEnemyState);
                            break;
                        }

                        continue;
                }

                break;
            }
        }
    }
}