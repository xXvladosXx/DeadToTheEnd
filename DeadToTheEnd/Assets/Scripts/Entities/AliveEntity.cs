using System;
using Combat.ColliderActivators;
using Data.Combat;
using Data.Stats;
using UnityEngine;

namespace Entities
{
    public abstract class AliveEntity : MonoBehaviour
    {
        public abstract Health Health { get; protected set; }
        public LongSwordColliderActivator LongSwordColliderActivator { get; private set; }
        
        protected StateMachine.StateMachine StateMachine;
        
        protected virtual void Awake()
        {
            LongSwordColliderActivator = GetComponentInChildren<LongSwordColliderActivator>();
        }
        public void OnAttackMake(float time, AttackType attackType)
        {
            AttackData attackData = new AttackData
            {
                AttackType = attackType
            };
            
            LongSwordColliderActivator.ActivateCollider(time, attackData);
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