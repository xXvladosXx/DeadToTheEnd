using Data.Combat;
using Entities.Enemies;
using StateMachine.WarriorEnemy;
using UnityEngine;

namespace StateMachine.Core
{
    [RequireComponent(typeof(StandardEnemy))]
    public class StandardEnemyAnimationEventTrigger : AnimationEventTrigger
    {
        private StandardEnemy _standardEnemy;
        protected override void Awake()
        {
            base.Awake();
            _standardEnemy = GetComponent<StandardEnemy>();
        }

        public void RadiusAttack(float time)
        {
            _standardEnemy.OnRadiusAttack(time, AttackType.Knock);
        }

        public void CollectEnergyRight(GameObject particle)
        {
            _standardEnemy.OnEnergyCollect(particle, false);
        }
        
        public void CollectEnergyLeft(GameObject particle)
        {
            _standardEnemy.OnEnergyCollect(particle, true);
        }
        
        public void RangeAttack(GameObject projectile)
        {
            _standardEnemy.OnRangeAttack(AttackType.Knock, projectile);
        }

        public void RangeAttackHand(GameObject projectile)
        {
            _standardEnemy.OnHandRangeAttack(AttackType.Medium, projectile);
        }
        
        public void RangeAttackBothHands(GameObject projectile)
        {
        }
    }
}