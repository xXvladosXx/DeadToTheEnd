using Data.Combat;
using Entities;
using StateMachine.WarriorEnemy;
using UnityEngine;

namespace StateMachine.Player
{
    [RequireComponent(typeof(MainPlayer))]
    public class PlayerAnimationEventTrigger : AnimationEventTrigger
    {
        private MainPlayer _mainPlayer;
        protected override void Awake()
        {
            base.Awake();
            _mainPlayer = GetComponent<MainPlayer>();
        }

        public void OnRightSwordColliderActivate(float time)
        {
            _mainPlayer.ActivateRightSword(time, AttackType.Easy);
        }
        
        public void OnLeftSwordColliderActivate(float time)
        {
            _mainPlayer.ActivateLeftSword(time, AttackType.Easy);
        }
    }
}