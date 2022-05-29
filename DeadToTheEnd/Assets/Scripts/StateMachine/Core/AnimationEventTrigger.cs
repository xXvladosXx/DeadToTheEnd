﻿using Data.Combat;
using Entities;
using UnityEngine;

namespace StateMachine.WarriorEnemy
{
    [RequireComponent(typeof(AliveEntity))]
    public class AnimationEventTrigger: MonoBehaviour
    {
        protected AliveEntity Entity;

        private void Awake()
        {
            Entity = GetComponent<AliveEntity>();
        }

        public void KnockAttack(float time)
        {
            Entity.OnAttackMake(time, AttackType.Knock);
        }
        
        public void EasyAttack(float time)
        {
            Entity.OnAttackMake(time, AttackType.Easy);
        }
        
        public void MediumAttack(float time)
        {
            print(time);
            Entity.OnAttackMake(time, AttackType.Medium);
        }
        
        public void TriggerOnStateAnimationEnterEvent()
        {
            Entity.OnMovementStateAnimationEnterEvent();
        }

        public void TriggerOnStateAnimationExitEvent()
        {
            Entity.OnMovementStateAnimationExitEvent();
        }
        public void TriggerOnStateAnimationHandleEvent()
        {
            Entity.OnMovementStateAnimationHandleEvent();
        }
    }
}