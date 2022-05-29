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
            foreach (var shortSwordColliderActivator in mainPlayer.ShortSwordColliderActivators)
            {
                if (shortSwordColliderActivator.RightSword)
                {
                    shortSwordColliderActivator.ActivateCollider(time);
                }
            }
        }
        
        public void OnLeftSwordColliderActivate(float time)
        {
            if (Entity is not MainPlayer mainPlayer) return;
            foreach (var shortSwordColliderActivator in mainPlayer.ShortSwordColliderActivators)
            {
                if (!shortSwordColliderActivator.RightSword)
                {
                    shortSwordColliderActivator.ActivateCollider(time);
                }
            }
        }
    }
}