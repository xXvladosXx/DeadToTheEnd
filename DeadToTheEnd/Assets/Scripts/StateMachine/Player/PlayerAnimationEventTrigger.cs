using Data.Combat;
using Entities;
using StateMachine.WarriorEnemy;
using UnityEngine;

namespace StateMachine.Player
{
    public class PlayerAnimationEventTrigger : AnimationEventTrigger
    {
        public void OnRightSwordColliderActivate(float time)
        {
            if (Entity is not MainPlayer mainPlayer) return;

            AttackData attackData = new AttackData
            {
                AttackType = AttackType.Easy
            };
            foreach (var shortSwordColliderActivator in mainPlayer.ShortSwordColliderActivators)
            {
                if (shortSwordColliderActivator.RightSword)
                {
                    shortSwordColliderActivator.ActivateCollider(time, attackData);
                }
            }
        }
        
        public void OnLeftSwordColliderActivate(float time)
        {
            if (Entity is not MainPlayer mainPlayer) return;
            
            AttackData attackData = new AttackData
            {
                AttackType = AttackType.Easy
            };
            foreach (var shortSwordColliderActivator in mainPlayer.ShortSwordColliderActivators)
            {
                if (!shortSwordColliderActivator.RightSword)
                {
                    shortSwordColliderActivator.ActivateCollider(time, attackData);
                }
            }
        }
    }
}