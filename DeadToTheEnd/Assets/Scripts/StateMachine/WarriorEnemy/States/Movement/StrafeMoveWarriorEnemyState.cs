using Data.Animations;
using Data.ScriptableObjects;
using UnityEngine;

namespace StateMachine.WarriorEnemy.States.Movement
{
    public class StrafeMoveWarriorEnemyState : BaseMovementEnemyState, IState
    {
        private float _curTime;
        private float _timeToStrafe;
        private bool _strafeDirectionRight;

        public StrafeMoveWarriorEnemyState(WarriorStateMachine warriorStateMachine) : base(warriorStateMachine)
        {
           
        }
        
        public override void Enter()
        {
            WarriorStateMachine.WarriorEnemy.EnemyStateReusableData.CanStrafe = false;
            WarriorStateMachine.WarriorEnemy.NavMeshAgent.isStopped = false;
            WarriorStateMachine.WarriorEnemy.NavMeshAgent.speed = WarriorEnemyData.EnemyStrafeData.StrafeSpeedModifer;
            
            _curTime = 0;
            _timeToStrafe = DecideTimeOfMoving(WarriorEnemyData.EnemyStrafeData.StrafeMinTime, WarriorEnemyData.EnemyStrafeData.StrafeMaxTime);
            DecideDirectionOfStrafe();
        }

        public override void Exit()
        {
            base.Exit();

            WarriorStateMachine.WarriorEnemy.Animator.SetBool("move", false);
            WarriorStateMachine.WarriorEnemy.Animator.SetFloat(WarriorEnemyAnimationData.VerticalParameterHash, 0f, .2f, Time.deltaTime);
            WarriorStateMachine.WarriorEnemy.Animator.SetFloat(WarriorEnemyAnimationData.HorizontalParameterHash, 0f,  .2f, Time.deltaTime);
        }

        public override void Update()
        {
            MakeComboFirstAttack();
            MakeComboSecondAttack();
            if(WarriorStateMachine.WarriorEnemy.EnemyStateReusableData.IsPerformingAction) return;
            
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
            var offsetPlayer = WarriorStateMachine.WarriorEnemy.transform.position -
                               WarriorStateMachine.WarriorEnemy.MainPlayer.transform.position;

            var dir = Vector3.Cross(offsetPlayer, Vector3.up);
            WarriorStateMachine.WarriorEnemy.NavMeshAgent.SetDestination(
                WarriorStateMachine.WarriorEnemy.transform.position + dir);

            var lookPos = WarriorStateMachine.WarriorEnemy.MainPlayer.transform.position -
                          WarriorStateMachine.WarriorEnemy.transform.position;
            lookPos.y = 0f;
            var rotation = Quaternion.LookRotation(lookPos);
            WarriorStateMachine.WarriorEnemy.transform.rotation = Quaternion.Slerp(WarriorStateMachine.WarriorEnemy.transform.rotation, rotation,
                Time.deltaTime * 15);
            
            WarriorStateMachine.WarriorEnemy.Animator.SetBool("move", true);
            WarriorStateMachine.WarriorEnemy.Animator.SetFloat(WarriorEnemyAnimationData.VerticalParameterHash, .5f, .2f, Time.deltaTime);
            WarriorStateMachine.WarriorEnemy.Animator.SetFloat(WarriorEnemyAnimationData.HorizontalParameterHash, 0f, .2f, Time.deltaTime);

        }

        private void StrafeLeft()
        {
            var offsetPlayer = WarriorStateMachine.WarriorEnemy.MainPlayer.transform.position -
                               WarriorStateMachine.WarriorEnemy.transform.position;

            var dir = Vector3.Cross(offsetPlayer, Vector3.up);
            WarriorStateMachine.WarriorEnemy.NavMeshAgent.SetDestination(
                WarriorStateMachine.WarriorEnemy.transform.position + dir);

            var lookPos = WarriorStateMachine.WarriorEnemy.MainPlayer.transform.position -
                          WarriorStateMachine.WarriorEnemy.transform.position;
            lookPos.y = 0f;
            var rotation = Quaternion.LookRotation(lookPos);
            WarriorStateMachine.WarriorEnemy.transform.rotation = Quaternion.Slerp(WarriorStateMachine.WarriorEnemy.transform.rotation, rotation,
                Time.deltaTime * 15);
            
            WarriorStateMachine.WarriorEnemy.Animator.SetBool("move", true);
            WarriorStateMachine.WarriorEnemy.Animator.SetFloat(WarriorEnemyAnimationData.VerticalParameterHash, -.5f, .2f, Time.deltaTime);
            WarriorStateMachine.WarriorEnemy.Animator.SetFloat(WarriorEnemyAnimationData.HorizontalParameterHash, 0f, .2f, Time.deltaTime);
        }
    }
}