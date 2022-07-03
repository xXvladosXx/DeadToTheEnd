using System;
using System.Collections.Generic;
using System.Linq;
using Combat.ColliderActivators;
using Combat.SwordActivators;
using Data.Combat;
using Data.ScriptableObjects;
using Data.States;
using Data.States.StateData;
using Data.Stats;
using Data.Stats.Core;
using Entities.Enemies;
using SkillsSystem;
using StateMachine.WarriorEnemy;
using StatsSystem.Bonuses;
using UnityEngine;

namespace Entities.Core
{
    [RequireComponent(typeof(Animator), 
        typeof(Rigidbody),
        typeof(Collider))]
    
    [RequireComponent(typeof(AnimationEventTrigger))]
    public abstract class AliveEntity : MonoBehaviour, IModifier, ISkillUser
    {
        [field: SerializeField] public AliveEntityStatsModifierData AliveEntityStatsModifierData { get; private set;}
        [field: SerializeField] public AliveEntityStatsData AliveEntityStatsData { get; private set; } 
        [field: SerializeField] public LevelCalculator LevelCalculator { get; private set; }
        [field: SerializeField] public EntityData EntityData { get; protected set; }
        [field: SerializeField] public Health Health { get; private set; }

        protected StateMachine.StateMachine StateMachine;
        
        public AttackCalculator AttackCalculator { get; protected set; }
        public OrdinaryAttackColliderActivator OrdinaryAttackColliderActivator { get; private set; }
        public AliveEntity Target { get; set; }
        public IReusable Reusable { get; set; }
        public Rigidbody Rigidbody { get; private set; }
        public Animator Animator { get; private set; }
        public StatsFinder StatsFinder { get; private set; }
        public StatsValueStorage StatsValueStorage { get; private set; } 

        private List<IStatsable> _statsables = new List<IStatsable>();
        private List<IModifier> _modifiers;

        protected virtual void Awake()
        {
            Rigidbody = GetComponent<Rigidbody>();
            Animator = GetComponent<Animator>();
            StatsValueStorage = new StatsValueStorage(this);

            OrdinaryAttackColliderActivator = GetComponentInChildren<OrdinaryAttackColliderActivator>();
            _modifiers = GetComponents<IModifier>().ToList();
            
            LevelCalculator.Init(AliveEntityStatsData);
            StatsFinder = new StatsFinder(AliveEntityStatsData, AliveEntityStatsModifierData, LevelCalculator, _modifiers);
            Health = new Health(StatsFinder);
            
            _statsables.Add(Health);
        }

        private void OnEnable()
        {
            LevelCalculator.OnLevelUp += RecalculateStats;
            AttackCalculator.OnDamageTaken += Health.DecreaseHealth;
            Health.OnDied += OnDied;
        }
        
        protected void RecalculateStats(int level)
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
            OrdinaryAttackColliderActivator.enabled = true;
            var attackData = CreateAttackData(attackType);
            
            OrdinaryAttackColliderActivator.ActivateCollider(time, attackData);
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
                Damage = StatsFinder.GetStat(Stat.Damage),
                ShakeCameraData = EntityData.ShakeCameraData
            };

            return attackData;
        }
        
        protected virtual void OnDied()
        {
            GetComponent<Collider>().enabled = false;
        }

        public IEnumerable<IBonus> AddBonus(Stat[] stats)
        {
             IBonus BonusTo(Stat stats)
            {
                return stats switch
                {
                    Stat.Damage => new DamageBonus(
                        StatsValueStorage.GetCalculatedStat(Stat.Damage)),
                    Stat.Health => new HealthBonus(
                        StatsValueStorage.GetCalculatedStat(Stat.Health)),
                    Stat.CriticalChance => new CriticalChanceBonus(
                        StatsValueStorage.GetCalculatedStat(Stat.CriticalChance)),
                    Stat.CriticalDamage => new CriticalDamageBonus(
                        StatsValueStorage.GetCalculatedStat(Stat.CriticalDamage)),
                    Stat.ManaRegeneration => new ManaRegenerationBonus(
                        StatsValueStorage.GetCalculatedStat(Stat.ManaRegeneration)),
                    Stat.HealthRegeneration => new HealthRegenerationBonus(
                        StatsValueStorage.GetCalculatedStat(Stat.HealthRegeneration)),
                    Stat.Mana => new ManaBonus(
                        StatsValueStorage.GetCalculatedStat(Stat.Mana)),
                    Stat.Evasion => new EvasionBonus(
                        StatsValueStorage.GetCalculatedStat(Stat.Evasion)),
                    Stat.Accuracy => new AccuracyBonus(
                        StatsValueStorage.GetCalculatedStat(Stat.Accuracy)),
                    _ => throw new ArgumentOutOfRangeException(nameof(stats), stats, null)
                };
            }


            return stats.Select(BonusTo);
        }
    }
}