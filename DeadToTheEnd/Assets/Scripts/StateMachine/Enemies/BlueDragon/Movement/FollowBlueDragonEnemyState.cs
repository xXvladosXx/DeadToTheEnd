using UnityEngine;

namespace StateMachine.Enemies.BlueGragon.Movement
{
    public class FollowBlueDragonEnemyState : BaseBlueDragonEnemyState
    {
        public FollowBlueDragonEnemyState(BlueDragonStateMachine stateMachine) : base(stateMachine)
        {
        }
        
        private float _curTime;
        private float _timeToWait;

        public override void Enter()
        {
            base.Enter();

            Enemy.NavMeshAgent.isStopped = true;
            _curTime = 0;
        }

        public override void Update()
        {
            base.Update();
            
            _curTime += Time.deltaTime;

            if (_curTime > 2)
            {
                DecideWhatToDo();
            }
        }

        private void DecideWhatToDo()
        {
            while (true)
            {
                int choice = Random.Range(0, 2);
                switch (choice)
                {
                    case 0:
                        BlueDragonStateMachine.ChangeState(BlueDragonStateMachine.ForwardBlueDragonEnemyState);
                        break;
                    case 1:
                        BlueDragonStateMachine.ChangeState(BlueDragonStateMachine.OrdinaryAttackBlueDragonEnemyState);
                        break;
                }
                
                break;
            }
        }

        protected override void DecideAttackToDo()
        {
            base.DecideAttackToDo();
            while (true)
            {
                int attackType = Random.Range(0, 1);

                switch (attackType)
                {
                    case 0:
                        if (CanMakeOrdinaryAttack())
                            break;
                        continue;
                }

                break;
            }
        }

        private bool CanMakeOrdinaryAttack()
        {
            return true;
        }
    }
}