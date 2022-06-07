using Combat.ColliderActivators;
using Combat.SwordActivators;
using Data.Animations;
using Data.Combat;
using Data.ScriptableObjects;
using Data.States;
using Data.Stats;
using StateMachine.Enemies.GoblinEnemy;
using StateMachine.Enemies.WarriorEnemy;
using UnityEngine;

namespace Entities.Enemies
{
    public class GoblinEnemy : Enemy
    {
        [field: SerializeField] public GoblinEnemyData GoblinEnemyData { get; private set; }
        
        public GoblinStateReusableData GoblinStateReusableData { get; private set; }
        
        public ShieldAttackColliderActivator ShieldAttackColliderActivator { get; private set; }
        public LegAttackColliderActivator LegAttackColliderActivator { get; private set; }

        protected override void Awake()
        {
            base.Awake();

            ShieldAttackColliderActivator = GetComponentInChildren<ShieldAttackColliderActivator>();
            LegAttackColliderActivator = GetComponentInChildren<LegAttackColliderActivator>();
            
            Reusable = new GoblinStateReusableData();
            GoblinStateReusableData = (GoblinStateReusableData) Reusable;
            
            Health = new Health(GoblinStateReusableData);
            
            StateMachine = new GoblinStateMachine(this);
        }
        
        public void ApplyShieldAttack(float time, AttackType attackType)
        {
            ShieldAttackColliderActivator.enabled = true;

            AttackData attackData = new AttackData
            {
                AttackType = attackType
            };
            
            ShieldAttackColliderActivator.ActivateCollider(time, attackData);
        }

        public void ApplyLegAttack(float time, AttackType attackType)
        {
            LegAttackColliderActivator.enabled = true;
            
            AttackData attackData = new AttackData
            {
                AttackType = attackType
            };
            
            LegAttackColliderActivator.ActivateCollider(time, attackData);
        }
    }
}