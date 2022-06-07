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
            WarriorEnemy.WarriorStateReusableData.CanStrafe = false;
            WarriorEnemy.NavMeshAgent.isStopped = false;
            WarriorEnemy.NavMeshAgent.speed = WarriorEnemyData.EnemyStrafeData.StrafeSpeedModifer;
            
            _curTime = 0;
            _timeToStrafe = DecideTime(WarriorEnemyData.EnemyStrafeData.StrafeMinTime, WarriorEnemyData.EnemyStrafeData.StrafeMaxTime);
            DecideDirectionOfStrafe();
        }

        public override void Exit()
        {
            base.Exit();

            WarriorEnemy.Animator.SetBool("move", false);
            WarriorEnemy.Animator.SetFloat(WarriorEnemyAnimationData.VerticalParameterHash, 0f, .2f, Time.deltaTime);
            WarriorEnemy.Animator.SetFloat(WarriorEnemyAnimationData.HorizontalParameterHash, 0f,  .2f, Time.deltaTime);
        }

        public override void Update()
        {
            MakeComboFirstAttack();
            MakeComboSecondAttack();
            if(WarriorEnemy.WarriorStateReusableData.IsPerformingAction) return;
            
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
            var offsetPlayer = WarriorEnemy.transform.position -
                               WarriorEnemy.Target.transform.position;

            var dir = Vector3.Cross(offsetPlayer, Vector3.up);
            WarriorEnemy.NavMeshAgent.SetDestination(
                WarriorEnemy.transform.position + dir);

            var lookPos = WarriorEnemy.Target.transform.position -
                          WarriorEnemy.transform.position;
            lookPos.y = 0f;
            var rotation = Quaternion.LookRotation(lookPos);
            WarriorStateMachine.AliveEntity.transform.rotation = Quaternion.Slerp(WarriorStateMachine.AliveEntity.transform.rotation, rotation,
                Time.deltaTime * 15);
            
            WarriorEnemy.Animator.SetBool("move", true);
            WarriorEnemy.Animator.SetFloat(WarriorEnemyAnimationData.VerticalParameterHash, .5f, .2f, Time.deltaTime);
            WarriorEnemy.Animator.SetFloat(WarriorEnemyAnimationData.HorizontalParameterHash, 0f, .2f, Time.deltaTime);

        }

        private void StrafeLeft()
        {
            var offsetPlayer = WarriorEnemy.Target.transform.position -
                               WarriorEnemy.transform.position;

            var dir = Vector3.Cross(offsetPlayer, Vector3.up);
            WarriorEnemy.NavMeshAgent.SetDestination(
                WarriorEnemy.transform.position + dir);

            var lookPos = WarriorEnemy.Target.transform.position -
                          WarriorEnemy.transform.position;
            lookPos.y = 0f;
            var rotation = Quaternion.LookRotation(lookPos);
            WarriorStateMachine.AliveEntity.transform.rotation = Quaternion.Slerp(WarriorStateMachine.AliveEntity.transform.rotation, rotation,
                Time.deltaTime * 15);
            
            WarriorEnemy.Animator.SetBool("move", true);
            WarriorEnemy.Animator.SetFloat(WarriorEnemyAnimationData.VerticalParameterHash, -.5f, .2f, Time.deltaTime);
            WarriorEnemy.Animator.SetFloat(WarriorEnemyAnimationData.HorizontalParameterHash, 0f, .2f, Time.deltaTime);
        }
    }
}