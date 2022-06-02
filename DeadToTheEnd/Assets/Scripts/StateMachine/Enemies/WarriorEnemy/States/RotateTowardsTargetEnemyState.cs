using Data.Animations;
using Data.ScriptableObjects;
using StateMachine.Enemies.WarriorEnemy;
using UnityEngine;

namespace StateMachine.WarriorEnemy.States.Movement
{
    public class RotateTowardsTargetEnemyState : BaseWarriorEnemyState
    {
        
        public RotateTowardsTargetEnemyState(WarriorStateMachine warriorStateMachine) : base(warriorStateMachine)
        {
            
        }
        
        public override void Exit()
        {
            BossEnemy.EnemyStateReusableData.IsPerformingAction = false;
            BossEnemy.EnemyStateReusableData.IsRotatingWithRootMotion = false;
            BossEnemy.Animator.SetBool(WarriorEnemyAnimationData.RotateLeftParameterHash, false);
            BossEnemy.Animator.SetBool(WarriorEnemyAnimationData.RotateRightParameterHash, false);
            BossEnemy.Animator.SetBool(WarriorEnemyAnimationData.RotateBehindParameterHash, false);
        }

        public override void Update()
        {
            BossEnemy.Animator.SetFloat(WarriorEnemyAnimationData.VerticalParameterHash, 0);
            BossEnemy.Animator.SetFloat(WarriorEnemyAnimationData.HorizontalParameterHash, 0);
            float viewableAngle = GetViewAngle(BossEnemy.transform,
                BossEnemy.Target.transform);

            switch (viewableAngle)
            {
                case >= 100 and <= 180 when !BossEnemy.EnemyStateReusableData.IsPerformingAction:
                case <= -101 and >= -180 when !BossEnemy.EnemyStateReusableData.IsPerformingAction:
                    RotateWithRootMotion(WarriorEnemyAnimationData.RotateBehindParameterHash);
                    break;
                case <= -45 and >= -100 when !BossEnemy.EnemyStateReusableData.IsPerformingAction:
                    RotateWithRootMotion(WarriorEnemyAnimationData.RotateRightParameterHash);
                    break;
                case >= 45 and <= 100 when !BossEnemy.EnemyStateReusableData.IsPerformingAction:
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
            BossEnemy.EnemyStateReusableData.IsPerformingAction = true;
            BossEnemy.EnemyStateReusableData.IsRotatingWithRootMotion = true;
            BossEnemy.Animator.SetBool(rotateLeftParameterHash, true);
        }
    }
}