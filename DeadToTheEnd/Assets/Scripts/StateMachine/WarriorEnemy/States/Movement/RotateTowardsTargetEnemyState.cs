using Data.Animations;
using Data.ScriptableObjects;
using UnityEngine;

namespace StateMachine.WarriorEnemy.States.Movement
{
    public class RotateTowardsTargetEnemyState : BaseMovementEnemyState, IState
    {
        
        public RotateTowardsTargetEnemyState(WarriorStateMachine warriorStateMachine) : base(warriorStateMachine)
        {
            
        }
        
        public override void Exit()
        {
            WarriorStateMachine.WarriorEnemy.EnemyStateReusableData.IsPerformingAction = false;
            WarriorStateMachine.WarriorEnemy.EnemyStateReusableData.IsRotatingWithRootMotion = false;
            WarriorStateMachine.WarriorEnemy.Animator.SetBool(WarriorEnemyAnimationData.RotateLeftParameterHash, false);
            WarriorStateMachine.WarriorEnemy.Animator.SetBool(WarriorEnemyAnimationData.RotateRightParameterHash, false);
            WarriorStateMachine.WarriorEnemy.Animator.SetBool(WarriorEnemyAnimationData.RotateBehindParameterHash, false);
        }

        public override void Update()
        {
            WarriorStateMachine.WarriorEnemy.Animator.SetFloat(WarriorEnemyAnimationData.VerticalParameterHash, 0);
            WarriorStateMachine.WarriorEnemy.Animator.SetFloat(WarriorEnemyAnimationData.HorizontalParameterHash, 0);
            float viewableAngle = GetViewAngle(WarriorStateMachine.WarriorEnemy.transform,
                WarriorStateMachine.WarriorEnemy.MainPlayer.transform);

            switch (viewableAngle)
            {
                case >= 100 and <= 180 when !WarriorStateMachine.WarriorEnemy.EnemyStateReusableData.IsPerformingAction:
                case <= -101 and >= -180 when !WarriorStateMachine.WarriorEnemy.EnemyStateReusableData.IsPerformingAction:
                    RotateWithRootMotion(WarriorEnemyAnimationData.RotateBehindParameterHash);
                    break;
                case <= -45 and >= -100 when !WarriorStateMachine.WarriorEnemy.EnemyStateReusableData.IsPerformingAction:
                    RotateWithRootMotion(WarriorEnemyAnimationData.RotateRightParameterHash);
                    break;
                case >= 45 and <= 100 when !WarriorStateMachine.WarriorEnemy.EnemyStateReusableData.IsPerformingAction:
                    RotateWithRootMotion(WarriorEnemyAnimationData.RotateLeftParameterHash);
                    break;
                default:
                    break;
            }
        }

        public override void OnAnimationExitEvent()
        {
            WarriorStateMachine.ChangeState(WarriorStateMachine.FollowWarriorEnemyState);
        }
        
        private void RotateWithRootMotion(int rotateLeftParameterHash)
        {
            WarriorStateMachine.WarriorEnemy.EnemyStateReusableData.IsPerformingAction = true;
            WarriorStateMachine.WarriorEnemy.EnemyStateReusableData.IsRotatingWithRootMotion = true;
            WarriorStateMachine.WarriorEnemy.Animator.SetBool(rotateLeftParameterHash, true);
        }
    }
}