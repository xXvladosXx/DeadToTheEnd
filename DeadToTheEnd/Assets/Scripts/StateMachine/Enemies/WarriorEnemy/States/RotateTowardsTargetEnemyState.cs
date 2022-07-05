using Data.Animations;
using Data.ScriptableObjects;
using StateMachine.Core;
using StateMachine.Enemies.BlueGragon;
using StateMachine.Enemies.WarriorEnemy;
using UnityEngine;

namespace StateMachine.WarriorEnemy.States.Movement
{
    public class RotateTowardsTargetEnemyState : BaseEnemyState
    {
        
        public RotateTowardsTargetEnemyState(StandardEnemyStateMachine warriorStateMachine) : base(warriorStateMachine)
        {
            
        }

        public override void Exit()
        {
            base.Exit();
            Enemy.Reusable.IsRotatingWithRootMotion = false;
            Enemy.Animator.SetBool(Enemy.EnemyAnimationData.RotateLeftParameterHash, false);
            Enemy.Animator.SetBool(Enemy.EnemyAnimationData.RotateRightParameterHash, false);
            Enemy.Animator.SetBool(Enemy.EnemyAnimationData.RotateBehindParameterHash, false);
        }

        public override void Update()
        {
            Enemy.Animator.SetFloat(Enemy.EnemyAnimationData.VerticalParameterHash, 0);
            Enemy.Animator.SetFloat(Enemy.EnemyAnimationData.HorizontalParameterHash, 0);
            float viewableAngle = GetViewAngle(Enemy.transform,
                Enemy.Target.transform);

            switch (viewableAngle)
            {
                case >= 100 and <= 180:
                case <= -101 and >= -180:
                    RotateWithRootMotion(Enemy.EnemyAnimationData.RotateBehindParameterHash);
                    break;
                case <= -45 and >= -100:
                    RotateWithRootMotion(Enemy.EnemyAnimationData.RotateRightParameterHash);
                    break;
                case >= 45 and <= 100:
                    RotateWithRootMotion(Enemy.EnemyAnimationData.RotateLeftParameterHash);
                    break;
                default:
                    break;
            }
        }

        public override void TriggerOnStateAnimationExitEvent()
        {
            StateMachine.ChangeState(StateMachine.StartState());
        }
        
        private void RotateWithRootMotion(int rotateLeftParameterHash)
        {
            Enemy.Reusable.IsRotatingWithRootMotion = true;
            Enemy.Animator.SetBool(rotateLeftParameterHash, true);
        }
    }
}