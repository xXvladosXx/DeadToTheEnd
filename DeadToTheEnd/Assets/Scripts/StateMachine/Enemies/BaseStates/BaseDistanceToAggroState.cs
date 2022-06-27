using StateMachine.Core;
using StateMachine.Enemies.BlueGragon;
using StateMachine.WarriorEnemy.States.Movement;

namespace StateMachine.Enemies.BaseStates
{
    public class BaseDistanceToAggroState : StandardEnemyState
    {
        public BaseDistanceToAggroState(StandardEnemyStateMachine stateMachine) : base(stateMachine)
        {
        }
        
        public override void Update()
        {
            if (IsEnoughDistance(EnemyData.EnemyIdleData.DistanceToFindTarget,
                    Enemy.transform, Enemy.Target.transform))
            {
                StandardEnemyStateMachine.ChangeState(StandardEnemyStateMachine.FollowStandardEnemyState);
            }
        }
    }
}