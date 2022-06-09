using Data.Combat;
using Entities.Enemies;
using StateMachine.WarriorEnemy;
using UnityEngine;

namespace StateMachine.Core
{
    [RequireComponent(typeof(BlueDragonEnemy))]
    public class BlueDragonAnimationEventTrigger : AnimationEventTrigger
    {
        private BlueDragonEnemy _blueDragonEnemy;
        protected override void Awake()
        {
            base.Awake();
            _blueDragonEnemy = GetComponent<BlueDragonEnemy>();
        }

        public void RadiusAttack(float time)
        {
            _blueDragonEnemy.OnRadiusAttack(time, AttackType.Knock);
        }
    }
}