using Combat.ColliderActivators;
using Combat.SwordActivators;
using Data.Combat;
using Data.States.StateData;
using Data.Stats;
using StateMachine.WarriorEnemy;
using UnityEngine;

namespace Entities.Core
{
    [RequireComponent(typeof(AnimationEventTrigger))]
    public abstract class AliveEntity : MonoBehaviour
    {
        public abstract Health Health { get; protected set; }
        public LongSwordAttackColliderActivator SwordAttackColliderActivator { get; private set; }
        public AliveEntity Target { get; protected set; }

        
        protected StateMachine.StateMachine StateMachine;
        
        public IReusable Reusable { get; set; }
        
        protected virtual void Awake()
        {
            SwordAttackColliderActivator = GetComponentInChildren<LongSwordAttackColliderActivator>();
        }
        public void OnAttackMake(float time, AttackType attackType)
        {
            AttackData attackData = new AttackData
            {
                AttackType = attackType
            };
            
            SwordAttackColliderActivator.ActivateCollider(time, attackData);
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