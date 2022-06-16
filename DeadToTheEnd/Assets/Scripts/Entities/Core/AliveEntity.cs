using System;
using System.Collections.Generic;
using Combat.ColliderActivators;
using Combat.SwordActivators;
using Data.Combat;
using Data.States;
using Data.States.StateData;
using Data.Stats;
using Entities.Enemies;
using SkillsSystem;
using StateMachine.WarriorEnemy;
using UnityEngine;

namespace Entities.Core
{
    [RequireComponent(typeof(Animator), typeof(Rigidbody),
        typeof(Collider))]
    public abstract class AliveEntity : MonoBehaviour, ISkillUser
    {
        [SerializeField] private AliveEntityStatsModifierData _aliveEntityStatsModifierData;
        [field: SerializeField] public float Damage { get; private set; }
        [field: SerializeField] public AliveEntityStatsData AliveEntityStatsData { get; private set; } 
        [field: SerializeField] public LevelCalculator LevelCalculator { get; private set; }

        protected StateMachine.StateMachine StateMachine;
        public Health Health { get; private set; }
        
        public AttackCalculator AttackCalculator { get; protected set; }
        public LongAttackColliderActivator AttackColliderActivator { get; private set; }
        public AliveEntity Target { get; protected set; }
        public IReusable Reusable { get; set; }
        public Rigidbody Rigidbody { get; private set; }
        public Animator Animator { get; private set; }
        public StatsFinder StatsFinder { get; private set; }

        private List<IStatsable> _statsables = new List<IStatsable>();

        protected virtual void Awake()
        {
            Rigidbody = GetComponent<Rigidbody>();
            Animator = GetComponent<Animator>();
            
            AttackColliderActivator = GetComponentInChildren<LongAttackColliderActivator>();
            LevelCalculator.Init(AliveEntityStatsData);
            StatsFinder = new StatsFinder(AliveEntityStatsData, _aliveEntityStatsModifierData, LevelCalculator);
            Health = new Health(StatsFinder);
            
            _statsables.Add(Health);
        }

        private void OnEnable()
        {
            LevelCalculator.OnLevelUp += RecalculateStats;
            AttackCalculator.OnDamageTaken += Health.DecreaseHealth;
            Health.OnDied += OnDied;
        }
        
        private void RecalculateStats(int level)
        {
            foreach (var statsable in _statsables)
            {
                statsable.RecalculateStat();
            }
        }

        private void OnDisable()
        {
            LevelCalculator.OnLevelUp -= RecalculateStats;
            AttackCalculator.OnDamageTaken -= Health.DecreaseHealth;
            Health.OnDied -= OnDied;
        }

        public void OnAttackMake(float time, AttackType attackType)
        {
            AttackColliderActivator.enabled = true;
            var attackData = CreateAttackData(attackType);
            
            AttackColliderActivator.ActivateCollider(time, attackData);
        }
        public void OnMovementStateAnimationEnterEvent()
        {
            StateMachine.TriggerOnStateAnimationEnterEvent();
        }

        public void OnMovementStateAnimationExitEvent()
        {
            StateMachine.TriggerOnStateAnimationExitEvent();
        }
        public void OnMovementStateAnimationHandleEvent()
        {
            StateMachine.TriggerOnStateAnimationHandleEvent();
        }

        protected AttackData CreateAttackData(AttackType attackType)
        {
            var attackData = new AttackData
            {
                AttackType = attackType,
                User = this,
                Damage = StatsFinder.GetStat(Stat.Damage)
            };

            return attackData;
        }
        
        private void OnDied()
        {
            GetComponent<Collider>().enabled = false;
        }
    }
}