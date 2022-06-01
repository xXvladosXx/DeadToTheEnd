using Combat.ColliderActivators;
using Combat.SwordActivators;
using Data.Combat;
using Data.Stats;
using StateMachine.WarriorEnemy;
using UnityEngine;

namespace Entities.Core
{
    [RequireComponent(typeof(AnimationEventTrigger))]
    public abstract class AliveEntity : MonoBehaviour
    {
        public abstract Health Health { get; protected set; }
        public LongSwordColliderActivator SwordColliderActivator { get; private set; }
        
        protected StateMachine.StateMachine StateMachine;
        
        protected virtual void Awake()
        {
            SwordColliderActivator = GetComponentInChildren<LongSwordColliderActivator>();
        }
        public void OnAttackMake(float time, AttackType attackType)
        {
            AttackData attackData = new AttackData
            {
                AttackType = attackType
            };
            
            SwordColliderActivator.ActivateCollider(time, attackData);
        }
        public void OnMovementStateAnimationEnterEvent()
        {
            StateMachine.OnAnimationEnterEvent();
        }

        public void OnMovementStateAnimationExitEvent()
        {
            StateMachine.OnAnimationExitEvent();
        }
        public void OnMovementStateAnimationHandleEvent()
        {
            StateMachine.OnAnimationHandleEvent();
        }
    }
}