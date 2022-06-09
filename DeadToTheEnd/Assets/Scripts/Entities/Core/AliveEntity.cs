using System;
using Combat.ColliderActivators;
using Combat.SwordActivators;
using Data.Combat;
using Data.States;
using Data.States.StateData;
using Data.Stats;
using Entities.Enemies;
using StateMachine.WarriorEnemy;
using UnityEngine;

namespace Entities.Core
{
    public abstract class AliveEntity : MonoBehaviour
    {
        [SerializeField] private AliveEntityStatsModifierData _aliveEntityStatsModifierData;
        [field: SerializeField] public float Damage { get; private set; }
        [field: SerializeField] public AliveEntityStatsData AliveEntityStatsData { get; private set; } 

        protected StateMachine.StateMachine StateMachine;
        protected Health Health { get; private set; }
        
        public AttackCalculator AttackCalculator { get; protected set; }
        public LongAttackColliderActivator AttackColliderActivator { get; private set; }
        public AliveEntity Target { get; protected set; }
        public IReusable Reusable { get; set; }

        private StatsFinder _statsFinder;
        
        protected virtual void Awake()
        {
            AttackColliderActivator = GetComponentInChildren<LongAttackColliderActivator>();
            _statsFinder = new StatsFinder(AliveEntityStatsData, _aliveEntityStatsModifierData);

            _statsFinder.GetStat(Stat.Health);
        }

        private void OnEnable()
        {
            Debug.Log("Damaged " );

//            AttackCalculator.OnDamageTaken += Health.DecreaseDamage;
        }

        private void OnDisable()
        {
 //           AttackCalculator.OnDamageTaken -= Health.DecreaseDamage;
        }

        public void OnAttackMake(float time, AttackType attackType)
        {
            AttackColliderActivator.enabled = true;
            
            AttackData attackData = new AttackData
            {
                AttackType = attackType,
                User = this,
                Damage = Damage
            };
            
            AttackColliderActivator.ActivateCollider(time, attackData);
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