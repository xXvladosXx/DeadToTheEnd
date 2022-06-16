using StateMachine.Enemies.GoblinEnemy.States.Movement;
using UnityEngine;

namespace StateMachine.Enemies.GoblinEnemy.States.Hit
{
    public class BaseHitGoblinEnemyState : BaseGoblinEnemyState
    {
        protected BaseHitGoblinEnemyState(GoblinStateMachine stateMachine) : base(stateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();
            GoblinEnemy.GoblinStateReusableData.CurrentAmountOfBackHits++;
        }

        public override void TriggerOnStateAnimationExitEvent()
        {
            base.TriggerOnStateAnimationExitEvent();
            if (GoblinEnemy.GoblinStateReusableData.CurrentAmountOfBackHits >
                GoblinEnemy.GoblinStateReusableData.MaxAmountOfBackHits)
            {
                GoblinEnemy.GoblinStateReusableData.CurrentAmountOfBackHits = 0;
                GoblinEnemy.GoblinStateReusableData.MaxAmountOfBackHits = Random.Range(0,4);

                DecideBackAttack();
                return;
            }
            GoblinStateMachine.ChangeState(GoblinStateMachine.FollowGoblinEnemyState);
        }

        private void DecideBackAttack()
        {
            int attackType = Random.Range(0, 3);

            switch (attackType)
            {
                case 0:
                    GoblinStateMachine.ChangeState(GoblinStateMachine.OrdinaryAttackGoblinEnemyState);
                    break;
                case 1:
                    GoblinStateMachine.ChangeState(GoblinStateMachine.RangeAttackGoblinEnemyState);
                    break;
                case 2:
                    GoblinStateMachine.ChangeState(GoblinStateMachine.HeavyAttackGoblinEnemyState);
                    break;
            }
        }
    }
}