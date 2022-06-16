using UnityEngine;
using UnityEngine.Rendering;

namespace Data.Stats
{
    public class StatsFinder
    {
        private AliveEntityStatsData _aliveEntityStatsData;
        private AliveEntityStatsModifierData _aliveEntityStatsModifierData;
        
        private readonly LevelCalculator _levelCalculator;

        public StatsFinder(AliveEntityStatsData aliveEntityStatsData,
            AliveEntityStatsModifierData aliveEntityStatsModifierData,
            LevelCalculator levelCalculator)
        {
            _aliveEntityStatsData = aliveEntityStatsData;
            _aliveEntityStatsModifierData = aliveEntityStatsModifierData;
            _levelCalculator = levelCalculator;
        }
        
        public float GetStat(Stat stat)
        {
            var characteristic = _aliveEntityStatsModifierData.StatModifier[stat];
            float starterValue = _aliveEntityStatsData.ReturnLevelValueCharacteristics(characteristic.Characteristic, _levelCalculator.Level);
            var characteristicWithModifier = starterValue * _aliveEntityStatsModifierData.StatModifier[stat].Value;
            
            //float valueWithBonus = GetBonus(characteristics) + starterValue;
            
            return characteristicWithModifier;
        }

        public float GetExperienceReward() => _aliveEntityStatsData.GetExperience(Experience.ExperienceReward, _levelCalculator.Level);
    }
}