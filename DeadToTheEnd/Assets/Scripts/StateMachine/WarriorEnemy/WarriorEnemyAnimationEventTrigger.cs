using Data.Combat;
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

        public void KnockAttack(float time)
        {
            _enemy.OnAttackMake(time, AttackType.Knock);
        }
        
        public void EasyAttack(float time)
        {
            _enemy.OnAttackMake(time, AttackType.Easy);
        }
        
        public void MediumAttack(float time)
        {
            _enemy.OnAttackMake(time, AttackType.Medium);
        }
        
        public void TriggerOnStateAnimationEnterEvent()
        {
            _enemy.OnMovementStateAnimationEnterEvent();
        }

        public void TriggerOnStateAnimationExitEvent()
        {
            _enemy.OnMovementStateAnimationExitEvent();
        }
        public void TriggerOnStateAnimationHandleEvent()
        {
            _enemy.OnMovementStateAnimationHandleEvent();
        }
        private bool IsInAnimationTransition(int layerIndex = 0)
        {
            return _enemy.Animator.IsInTransition(layerIndex);
        }
        
    }
}