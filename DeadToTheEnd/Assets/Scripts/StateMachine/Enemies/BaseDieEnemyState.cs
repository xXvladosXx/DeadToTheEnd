using StateMachine.Core;
using StateMachine.Enemies.BlueGragon;
using StateMachine.WarriorEnemy.States.Movement;
using UnityEngine;

namespace StateMachine.Enemies
{
    public class BaseDieEnemyState : BaseEnemyState
    {
        public BaseDieEnemyState(StandardEnemyStateMachine stateMachine) : base(stateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();

            Stop();
            StartAnimation(Enemy.EnemyAnimationData.DeathParameterHash);
        }

        public override void Update()
        {
        }

        public override void FixedUpdate()
        {
        }

        public override void TriggerOnStateAnimationExitEvent()
        {
            base.TriggerOnStateAnimationExitEvent();
            StopAnimation(Enemy.EnemyAnimationData.DeathParameterHash);
        }
    }
}