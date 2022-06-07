using Data.Combat;
using Entities.Enemies;
using StateMachine.WarriorEnemy;
using UnityEngine;

namespace StateMachine.Core
{
    [RequireComponent(typeof(GoblinEnemy))]
    public class GoblinAnimationEventTrigger : AnimationEventTrigger
    {
        private GoblinEnemy _goblinEnemy;
        protected override void Awake()
        {
            base.Awake();
            _goblinEnemy = GetComponent<GoblinEnemy>();
        }

        public void LegAttack(float time)
        {
            _goblinEnemy.ApplyLegAttack(time, AttackType.Knock);
        }

        public void ShieldAttack(float time)
        {
            _goblinEnemy.ApplyShieldAttack(time, AttackType.Medium);
        }
    }
}