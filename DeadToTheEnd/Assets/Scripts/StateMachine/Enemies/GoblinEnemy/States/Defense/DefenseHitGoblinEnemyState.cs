using StateMachine.Enemies.GoblinEnemy.States.Movement;
using UnityEngine;

namespace StateMachine.Enemies.GoblinEnemy.States.Defense
{
    public class DefenseHitGoblinEnemyState : BaseGoblinEnemyState
    {
        public DefenseHitGoblinEnemyState(GoblinStateMachine stateMachine) : base(stateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();

            GoblinEnemy.GoblinStateReusableData.CurrentAmountOfFrontHits++;
            if (GoblinEnemy.GoblinStateReusableData.CurrentAmountOfFrontHits >
                GoblinEnemy.GoblinStateReusableData.MaxAmountOfFrontHits)
            {
                GoblinEnemy.GoblinStateReusableData.CurrentAmountOfFrontHits = 0;
                GoblinStateMachine.ChangeState(GoblinStateMachine.SecondComboAttackGoblinEnemyState);
                
                GoblinEnemy.GoblinStateReusableData.MaxAmountOfFrontHits = Random.Range(3,6);

                return;
            }
            
            StartAnimation(GoblinEnemyAnimationData.BlockHitParameterHash);
        }

        public override void Exit()
        {
            base.Exit();
            StopAnimation(GoblinEnemyAnimationData.BlockHitParameterHash);
        }

        public override void TriggerOnStateAnimationExitEvent()
        {
            base.TriggerOnStateAnimationExitEvent();
            GoblinStateMachine.ChangeState(GoblinStateMachine.FollowGoblinEnemyState);
        }
    }
}