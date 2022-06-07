using StateMachine.Enemies.BaseStates;
using UnityEngine;

namespace StateMachine.Enemies.GoblinEnemy.States.Combat
{
    public class OrdinaryAttackGoblinEnemyState : BaseOrdinaryAttackEnemyState
    {
        public OrdinaryAttackGoblinEnemyState(GoblinStateMachine stateMachine) : base(stateMachine)
        {
        }

        public override void OnAnimationEnterEvent()
        {
            base.OnAnimationEnterEvent();
            Enemy.NavMeshAgent.speed = Enemy.EnemyData.EnemyOrdinaryAttackData.WalkSpeedModifer;

            Enemy.NavMeshAgent.isStopped = false;
        }

        public override void OnAnimationExitEvent()
        {
            base.OnAnimationExitEvent();
            StateMachine.ChangeState(StateMachine.StartState());
        }

    }
}