using StateMachine.Enemies.GoblinEnemy.States.Hit;
using UnityEngine;

namespace StateMachine.Enemies.GoblinEnemy.States.Movement.Hit
{
    public class MediumHitGoblinEnemyState : BaseHitGoblinEnemyState
    {
        public MediumHitGoblinEnemyState(GoblinStateMachine stateMachine) : base(stateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();

            StartAnimation(GoblinEnemyAnimationData.MediumHitParameterHash);
        }

        public override void Exit()
        {
            base.Exit();
            Debug.Log("Exited");
            StopAnimation(GoblinEnemyAnimationData.MediumHitParameterHash);
        }
    }
}