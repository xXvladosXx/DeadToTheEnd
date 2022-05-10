using Entities;
using UnityEngine;

namespace StateMachine.WarriorEnemy
{
    [RequireComponent(typeof(Enemy))]
    public class WarriorEnemyAnimationEventTrigger: MonoBehaviour
    {
        private Enemy _enemy;

        private void Awake()
        {
            _enemy = GetComponent<Enemy>();
        }

        public void TriggerOnStateAnimationEnterEvent()
        {
            _enemy.OnMovementStateAnimationEnterEvent();
        }

        public void TriggerOnStateAnimationExitEvent()
        {
            _enemy.OnMovementStateAnimationExitEvent();
        }

        private bool IsInAnimationTransition(int layerIndex = 0)
        {
            return _enemy.Animator.IsInTransition(layerIndex);
        }
        
    }
}