using Data.Animations;
using Data.ScriptableObjects;
using StateMachine.Enemies.WarriorEnemy;
using UnityEngine;

namespace StateMachine.WarriorEnemy.States.Movement
{
    public class StrafeMoveWarriorEnemyState : BaseWarriorEnemyState, IState
    {
        private float _curTime;
        private float _timeToStrafe;
        private bool _strafeDirectionRight;

        public StrafeMoveWarriorEnemyState(WarriorStateMachine warriorStateMachine) : base(warriorStateMachine)
        {
           
        }
        
        public override void Enter()
        {
            BossEnemy.EnemyStateReusableData.CanStrafe = false;
            BossEnemy.NavMeshAgent.isStopped = false;
            BossEnemy.NavMeshAgent.speed = bossEnemyData.EnemyStrafeData.StrafeSpeedModifer;
            
            _curTime = 0;
            _timeToStrafe = DecideTimeOfMoving(bossEnemyData.EnemyStrafeData.StrafeMinTime, bossEnemyData.EnemyStrafeData.StrafeMaxTime);
            DecideDirectionOfStrafe();
        }

        public override void Exit()
        {
            base.Exit();

            BossEnemy.Animator.SetBool("move", false);
            BossEnemy.Animator.SetFloat(WarriorEnemyAnimationData.VerticalParameterHash, 0f, .2f, Time.deltaTime);
            BossEnemy.Animator.SetFloat(WarriorEnemyAnimationData.HorizontalParameterHash, 0f,  .2f, Time.deltaTime);
        }

        public override void Update()
        {
            MakeComboFirstAttack();
            MakeComboSecondAttack();
            if(BossEnemy.EnemyStateReusableData.IsPerformingAction) return;
            
            _curTime += Time.deltaTime;
            if (_curTime > _timeToStrafe)
            {
                WarriorStateMachine.ChangeState(WarriorStateMachine.FollowWarriorEnemyState);
                return;
            }
            
            if (_strafeDirectionRight)
            {
                StrafeRight();
                return;
            }
            
            StrafeLeft();
        }
        
        private void DecideDirectionOfStrafe()
        {
            int choice = Random.Range(0, 1);
            _strafeDirectionRight = choice switch
            {
                0 => false,
                1 => true,
                _ => _strafeDirectionRight
            };
        }
        
        private void StrafeRight()
        {
            var offsetPlayer = BossEnemy.transform.position -
                               BossEnemy.MainPlayer.transform.position;

            var dir = Vector3.Cross(offsetPlayer, Vector3.up);
            BossEnemy.NavMeshAgent.SetDestination(
                BossEnemy.transform.position + dir);

            var lookPos = BossEnemy.MainPlayer.transform.position -
                          BossEnemy.transform.position;
            lookPos.y = 0f;
            var rotation = Quaternion.LookRotation(lookPos);
            WarriorStateMachine.AliveEntity.transform.rotation = Quaternion.Slerp(WarriorStateMachine.AliveEntity.transform.rotation, rotation,
                Time.deltaTime * 15);
            
            BossEnemy.Animator.SetBool("move", true);
            BossEnemy.Animator.SetFloat(WarriorEnemyAnimationData.VerticalParameterHash, .5f, .2f, Time.deltaTime);
            BossEnemy.Animator.SetFloat(WarriorEnemyAnimationData.HorizontalParameterHash, 0f, .2f, Time.deltaTime);

        }

        private void StrafeLeft()
        {
            var offsetPlayer = BossEnemy.MainPlayer.transform.position -
                               BossEnemy.transform.position;

            var dir = Vector3.Cross(offsetPlayer, Vector3.up);
            BossEnemy.NavMeshAgent.SetDestination(
                BossEnemy.transform.position + dir);

            var lookPos = BossEnemy.MainPlayer.transform.position -
                          BossEnemy.transform.position;
            lookPos.y = 0f;
            var rotation = Quaternion.LookRotation(lookPos);
            WarriorStateMachine.AliveEntity.transform.rotation = Quaternion.Slerp(WarriorStateMachine.AliveEntity.transform.rotation, rotation,
                Time.deltaTime * 15);
            
            BossEnemy.Animator.SetBool("move", true);
            BossEnemy.Animator.SetFloat(WarriorEnemyAnimationData.VerticalParameterHash, -.5f, .2f, Time.deltaTime);
            BossEnemy.Animator.SetFloat(WarriorEnemyAnimationData.HorizontalParameterHash, 0f, .2f, Time.deltaTime);
        }
    }
}