using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Data.Stats;
using Data.Stats.Core;
using SkillsSystem;
using SkillsSystem.SkillBonuses;
using StateMachine.WarriorEnemy.Components;
using StatsSystem.Bonuses;
using UnityEngine;

public class BuffManager : MonoBehaviour, IModifier
{
    private Dictionary<StatBonus, float> _buffEffects = new Dictionary<StatBonus, float>();
    private Dictionary<StatBonus, float> _currentBuffEffects = new Dictionary<StatBonus, float>();
        
    public event Action<StatBonus> OnBonusAdded;
    public event Action OnBonusRemoved;
    public event Action OnStatModified;

    public void SetBuff(SkillBonus[] buffs)
    {
        foreach (var buff in buffs)
        {
            foreach (var buffBonus in buff.StatBonuses)
            {
                if (_buffEffects.ContainsKey(buffBonus))
                {
                    _buffEffects[buffBonus] = buffBonus.Time;
                    continue;
                }
                _buffEffects.Add(buffBonus, buffBonus.Time);
                OnBonusAdded?.Invoke(buffBonus);
            }
        }

        _currentBuffEffects = new Dictionary<StatBonus, float>(_buffEffects);
        OnStatModified?.Invoke();
    }

    private void Update()
    {
        if(_buffEffects.Count == 0) return;
        foreach (var buffEffect in _buffEffects)
        {
            _currentBuffEffects[buffEffect.Key] -= Time.deltaTime;

            if (buffEffect.Value < 0)
            {
                OnBonusRemoved?.Invoke();
                _currentBuffEffects.Remove(buffEffect.Key);
                OnStatModified?.Invoke();
            }
        }

        _buffEffects = new Dictionary<StatBonus, float>(_currentBuffEffects);
    }

    public float GetTimeOfBonus(StatTimeableBonus statBonus)
    {
        _buffEffects.TryGetValue(statBonus, out var time);
        var timePct = time / statBonus.Time;
        
        return timePct;
    }

    public IEnumerable<IBonus> AddBonus(Stat[] stats)
    {
        IBonus CharacteristicToBonus(Stat c, float value)
            => c switch {
                Stat.CriticalChance => new CriticalChanceBonus(value),
                Stat.CriticalDamage => new CriticalDamageBonus(value),
                Stat.Health => new HealthBonus(value),
                Stat.Damage => new DamageBonus(value),
                Stat.AttackSpeed => new AttackSpeedBonus(value),
                Stat.MovementSpeed => new MovementSpeedBonus(value),
                Stat.ManaRegeneration => new ManaRegenerationBonus(value),
                Stat.HealthRegeneration => new HealthRegenerationBonus(value),
                Stat.Mana => new ManaBonus(value),
                Stat.Evasion => new EvasionBonus(value),
                Stat.Accuracy => new AccuracyBonus(value),
                _ => throw new IndexOutOfRangeException()
            };

        return _currentBuffEffects.Keys
            .Select(x => CharacteristicToBonus(x.Stat, x.Value));
    }
}
