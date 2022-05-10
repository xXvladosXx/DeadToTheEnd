using Data.ScriptableObjects;
using StateMachine.WarriorEnemy.States.Movement;

namespace StateMachine.WarriorEnemy.States.Combat
{
    public class BaseAttackEnemyState : BaseMovementEnemyState, IState
    {
        private readonly WarriorStateMachine _warriorStateMachine;
        private readonly WarriorEnemyData _warriorEnemyData;
        
        public BaseAttackEnemyState(WarriorStateMachine warriorStateMachine) : base(warriorStateMachine)
        {
            _warriorStateMachine = warriorStateMachine;
            
            _warriorEnemyData = _warriorStateMachine.WarriorEnemy.WarriorEnemyData;
        }
        public void Enter()
        {
            
        }

        public void Exit()
        {
           
        }

        public void HandleInput()
        {
           
        }

        public void Update()
        {
           
        }

        public void FixedUpdate()
        {
          
        }

        public void OnAnimationEnterEvent()
        {
           
        }

        public void OnAnimationExitEvent()
        {
           
        }
    }
}