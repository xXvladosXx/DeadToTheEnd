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
using StatsSystem;
using StatsSystem.Bonuses;
using StatsSystem.Core;
using UnityEngine;

namespace Entities.Core
{
    [RequireComponent(typeof(Animator), 
        typeof(Rigidbody),
        typeof(Collider))]
    
    [RequireComponent(typeof(AnimationEventTrigger),
        typeof(BuffManager))]
    public abstract class AliveEntity : MonoBehaviour, IModifier, IUser
    {
        [field: SerializeField] public AliveEntityStatsModifierData AliveEntityStatsModifierData { get; private set;}
        [field: SerializeField] public AliveEntityStatsData AliveEntityStatsData { get; private set; } 
        [field: SerializeField] public LevelCalculator LevelCalculator { get; private set; }
        [field: SerializeField] public EntityData EntityData { get; protected set; }
        
        [field: SerializeField] public Health Health { get; private set; }
        [field: SerializeField] public Mana Mana { get; private set; }
        [field: SerializeField] public AttackSpeed AttackSpeed { get; private set; }

        protected StateMachine.StateMachine StateMachine;
        
        public AttackCalculator AttackCalculator { get; protected set; }
        public OrdinaryAttackColliderActivator OrdinaryAttackColliderActivator { get; private set; }
        public AliveEntity Target { get; set; }
        public IReusable Reusable { get; set; }
        public Rigidbody Rigidbody { get; private set; }
        public Animator Animator { get; private set; }
        public StatsFinder StatsFinder { get; private set; }
        public StatsValueStorage StatsValueStorage { get; private set; } 
        public BuffManager BuffManager { get; private set; }
        public SkillManager SkillManager { get; private set; }
        public List<IStatsable> Statsables { get; } = new();
        public List<IPointsAssignable> PointsAssignables { get; } = new();

        private List<IModifier> _modifiers;
        public event Action OnStatModified;

        protected virtual void Awake()
        {
            Rigidbody = GetComponent<Rigidbody>();
            Animator = GetComponent<Animator>();
            BuffManager = GetComponent<BuffManager>();
            SkillManager = GetComponent<SkillManager>();
            
            StatsValueStorage = new StatsValueStorage(AliveEntityStatsModifierData, AliveEntityStatsData, LevelCalculator);

            OrdinaryAttackColliderActivator = GetComponentInChildren<OrdinaryAttackColliderActivator>();
            _modifiers = GetComponents<IModifier>().ToList();
            
            LevelCalculator.Init(AliveEntityStatsData);
            StatsFinder = new StatsFinder(AliveEntityStatsData, AliveEntityStatsModifierData, LevelCalculator, _modifiers);
            Health = new Health(StatsFinder);
            Mana = new Mana(StatsFinder);
            AttackSpeed = new AttackSpeed(Animator, StatsFinder);
            
            Statsables.Add(Health);
            Statsables.Add(Mana);
            Statsables.Add(AttackSpeed);
            
            PointsAssignables.Add(SkillManager);
        }

        protected virtual void OnEnable()
        {
            LevelCalculator.OnLevelUp += RecalculateStatsWithMaxValue;
            
            AttackCalculator.OnDamageTaken += Health.DecreaseHealth;
            Health.OnDied += OnDied;

            foreach (var modifier in _modifiers)
            {
                modifier.OnStatModified += RecalculateStatsWithCurrentValue;
            }
        }

        private void RecalculateStatsWithCurrentValue()
        {
            foreach (var statsable in Statsables)
            {
                statsable.RecalculateStatWithCurrentValue();
            }
        }

        protected void RecalculateStatsWithMaxValue(int level)
        {
            foreach (var statsable in Statsables)
            {
                statsable.RecalculateStatWithMaxValue();
            }
        }
        
        protected virtual void OnDisable()
        {
            LevelCalculator.OnLevelUp -= RecalculateStatsWithMaxValue;
            
            AttackCalculator.OnDamageTaken -= Health.DecreaseHealth;
            Health.OnDied -= OnDied;
            
            foreach (var modifier in _modifiers)
            {
                modifier.OnStatModified -= RecalculateStatsWithCurrentValue;
            }
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
                CriticalChance = StatsFinder.GetStat(Stat.CriticalChance),
                CriticalDamage = StatsFinder.GetStat(Stat.CriticalDamage),
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
                    Stat.AttackSpeed => new AttackSpeedBonus(1),
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