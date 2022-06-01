namespace StateMachine.Enemies.GoblinEnemy.States.Movement
{
    public class RotateGoblinEnemyState : BaseGoblinEnemyState
    {
        public RotateGoblinEnemyState(GoblinStateMachine stateMachine) : base(stateMachine)
        {
        }
        
         public override void Exit()
        {
            GoblinEnemy.EnemyStateReusableData.IsPerformingAction = false;
            GoblinEnemy.EnemyStateReusableData.IsRotatingWithRootMotion = false;
            GoblinEnemy.Animator.SetBool(GoblinEnemyAnimationData.RotateLeftParameterHash, false);
            GoblinEnemy.Animator.SetBool(GoblinEnemyAnimationData.RotateRightParameterHash, false);
            GoblinEnemy.Animator.SetBool(GoblinEnemyAnimationData.RotateBehindParameterHash, false);
        }

        public override void Update()
        {
            GoblinEnemy.Animator.SetFloat(GoblinEnemyAnimationData.VerticalParameterHash, 0);
            GoblinEnemy.Animator.SetFloat(GoblinEnemyAnimationData.HorizontalParameterHash, 0);
            float viewableAngle = GetViewAngle(GoblinEnemy.transform,
                GoblinEnemy.MainPlayer.transform);

            switch (viewableAngle)
            {
                case >= 100 and <= 180 when !GoblinEnemy.EnemyStateReusableData.IsPerformingAction:
                case <= -101 and >= -180 when !GoblinEnemy.EnemyStateReusableData.IsPerformingAction:
                    RotateWithRootMotion(GoblinEnemyAnimationData.RotateBehindParameterHash);
                    break;
                case <= -45 and >= -100 when !GoblinEnemy.EnemyStateReusableData.IsPerformingAction:
                    RotateWithRootMotion(GoblinEnemyAnimationData.RotateRightParameterHash);
                    break;
                case >= 45 and <= 100 when !GoblinEnemy.EnemyStateReusableData.IsPerformingAction:
                    RotateWithRootMotion(GoblinEnemyAnimationData.RotateLeftParameterHash);
                    break;
                default:
                    break;
            }
        }

        public override void OnAnimationExitEvent()
        {
            GoblinStateMachine.ChangeState(GoblinStateMachine.FollowGoblinEnemyState);
        }
        
        private void RotateWithRootMotion(int rotateLeftParameterHash)
        {
            GoblinEnemy.EnemyStateReusableData.IsPerformingAction = true;
            GoblinEnemy.EnemyStateReusableData.IsRotatingWithRootMotion = true;
            GoblinEnemy.Animator.SetBool(rotateLeftParameterHash, true);
        }
    }
}