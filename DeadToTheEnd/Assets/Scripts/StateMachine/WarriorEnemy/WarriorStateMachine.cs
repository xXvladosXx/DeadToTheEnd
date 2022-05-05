using Entities;
using StateMachine.WarriorEnemy.States.Movement;

namespace StateMachine.WarriorEnemy
{
    public class WarriorStateMachine : StateMachine
    {
        public Enemy WarriorEnemy { get; }
        
        public IdleWarriorEnemyState IdleWarriorEnemyState { get; }
        public FollowWarriorEnemyState FollowWarriorEnemyState { get; }
        public PatrolWarriorEnemyState PatrolWarriorEnemyState { get; }


        public WarriorStateMachine(Enemy warriorEnemy)
        {
            WarriorEnemy = warriorEnemy;

            IdleWarriorEnemyState = new IdleWarriorEnemyState(this);
            FollowWarriorEnemyState = new FollowWarriorEnemyState(this);
            PatrolWarriorEnemyState = new PatrolWarriorEnemyState(this);
        }
    }
}