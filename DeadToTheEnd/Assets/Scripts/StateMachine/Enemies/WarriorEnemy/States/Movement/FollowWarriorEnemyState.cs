using Data.ScriptableObjects;
using Entities;
using StateMachine.Enemies.WarriorEnemy;
using UnityEngine;

namespace StateMachine.WarriorEnemy.States.Movement
{
    public class FollowWarriorEnemyState : BaseWarriorEnemyState
    {
        private Vector3 _pointToMoveTo;
        private float _timeToWaitMovement;
        private float _timeToWaitAttack;
        private float _curTime;

        public FollowWarriorEnemyState(WarriorStateMachine warriorStateMachine) : base(warriorStateMachine)
        {
            
        }
        
        public override void Enter()
        {
            base.Enter();
            _curTime = 0;
            _timeToWaitMovement = TimeToWaitMovement();
            _timeToWaitAttack = TimeToWaitAttack();
        }

        public override void Exit()
        {
            base.Exit();
            BossEnemy.Animator.SetFloat(WarriorEnemyAnimationData.VerticalParameterHash, 0, .1f, Time.deltaTime);
        }

        public override void Update()
        {
            if(BossEnemy.EnemyStateReusableData.IsPerformingAction) return;
            _curTime += Time.deltaTime;
            base.Update();

            if (_curTime > _timeToWaitMovement)
            {
                DecideWhatToDo();
                return;
            }

            if (_curTime > _timeToWaitAttack)
            {
                DecideAttackToDo();
            }
        }

        private float TimeToWaitMovement()
        {
            return Random.Range(1.5f, 2.5f);
        }
        
        private void DecideWhatToDo()
        {
            while (true)
            {
                int choice = Random.Range(0, 3);
                switch (choice)
                {
                    case 0:
                        WarriorStateMachine.ChangeState(WarriorStateMachine.ForwardMoveWarriorEnemyState);
                        BossEnemy.EnemyStateReusableData.CanStrafe = true;
                        break;
                    case 1:
                        if (!IsEnoughDistance(3, WarriorStateMachine.AliveEntity.transform, 
                                BossEnemy.MainPlayer.transform) &&
                            BossEnemy.EnemyStateReusableData.CanStrafe)
                        {
                            WarriorStateMachine.ChangeState(WarriorStateMachine.StrafeMoveWarriorEnemyState);
                            break;
                        }

                        continue;
                    case 2:
                        if (IsEnoughDistance(2, WarriorStateMachine.AliveEntity.transform, 
                                BossEnemy.MainPlayer.transform))
                        {
                            WarriorStateMachine.ChangeState(WarriorStateMachine.RollBackWarriorEnemyState);
                            break;
                        }

                        continue;
                }

                break;
            }
        }
        
        private float TimeToWaitAttack()
        {
            return Random.Range(bossEnemyData.EnemyAttackData.MinTimeToAttack,
                bossEnemyData.EnemyAttackData.MaxTimeToAttack);
        }

        
    }
}