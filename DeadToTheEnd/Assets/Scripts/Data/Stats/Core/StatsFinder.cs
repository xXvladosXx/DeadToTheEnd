using System.Collections.Generic;
using System.Linq;
using Data.Stats.Core;
using StatsSystem.Bonuses;
using UnityEngine;
using UnityEngine.Rendering;

namespace Data.Stats
{
    public class StatsFinder
    {
        private AliveEntityStatsData _aliveEntityStatsData;
        private AliveEntityStatsModifierData _aliveEntityStatsModifierData;
        
        private readonly LevelCalculator _levelCalculator;
        private readonly List<IModifier> _modifiers;
        public float GetExperienceReward() => _aliveEntityStatsData.GetExperience(Experience.ExperienceReward, _levelCalculator.Level);

        public StatsFinder(AliveEntityStatsData aliveEntityStatsData,
            AliveEntityStatsModifierData aliveEntityStatsModifierData,
            LevelCalculator levelCalculator, List<IModifier> modifiers)
        {
            _aliveEntityStatsData = aliveEntityStatsData;
            _aliveEntityStatsModifierData = aliveEntityStatsModifierData;
            _levelCalculator = levelCalculator;
            _modifiers = modifiers;
        }
        
        public float GetStat(Stat stat)
        {
            var characteristicWithModifier = GetBonus(stat);
            //float valueWithBonus = GetBonus(characteristics) + starterValue;
            
            return characteristicWithModifier;
        }

        private float GetBonus(Stat stat)
        {
            bool IsBonusAssignableToCharacteristics(IBonus bonus) 
                => (bonus, stat) switch
                {
                    (HealthBonus b, Stat.Health) => true,
                    (DamageBonus b, Stat.Damage) => true,
                    (CriticalChanceBonus b, Stat.CriticalChance) => true,
                    (CriticalDamageBonus b, Stat.CriticalDamage) => true,
                    (AttackSpeedBonus b, Stat.AttackSpeed) => true,
                    (MovementSpeedBonus b, Stat.MovementSpeed) => true,
                    (ManaRegenerationBonus b, Stat.ManaRegeneration) => true,
                    (ManaBonus b, Stat.Mana) => true,
                    (HealthRegenerationBonus b, Stat.HealthRegeneration) => true,
                    (EvasionBonus b, Stat.Evasion) => true,
                    (AccuracyBonus b, Stat.Accuracy) => true,
                    _ => false
                };

            return _modifiers
                .SelectMany(x => x.AddBonus(new[] { stat }))
                .Where(IsBonusAssignableToCharacteristics)
                .Sum(x => x.Value);
        }

    }
}