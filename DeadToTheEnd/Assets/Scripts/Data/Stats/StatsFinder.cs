using UnityEngine;
using UnityEngine.Rendering;

namespace Data.Stats
{
    public class StatsFinder
    {
        private AliveEntityStatsData _aliveEntityStatsData;
        private AliveEntityStatsModifierData _aliveEntityStatsModifierData;
        
        public StatsFinder(AliveEntityStatsData aliveEntityStatsData, AliveEntityStatsModifierData aliveEntityStatsModifierData)
        {
            _aliveEntityStatsData = aliveEntityStatsData;
            _aliveEntityStatsModifierData = aliveEntityStatsModifierData;
        }
        
        /*public int CalculateLevel(float currentExp)
        {
            int maxLevel = _aliveEntityStatsData.GetLevels(Characteristics.ExperienceToLevelUp);

            for (int level = 1; level < maxLevel; level++)
            {
                float expToLevelUp = _starterCharacterData.ReturnLevelValueCharacteristics(_class, Characteristics.ExperienceToLevelUp, level);

                if (expToLevelUp > currentExp)
                {
                    _level = level;

                    return level;
                }
            }

            _level = maxLevel + 1;
            
            return maxLevel + 1;
        }*/
        
        public float GetStat(Stat stat)
        {
            var characteristic = _aliveEntityStatsModifierData.StatModifier[stat];
            float starterValue = _aliveEntityStatsData.ReturnLevelValueCharacteristics(characteristic.Characteristic, 1);
            var characteristicWithModifier = starterValue * _aliveEntityStatsModifierData.StatModifier[stat].Value;
            
            Debug.Log(characteristicWithModifier);
            //float valueWithBonus = GetBonus(characteristics) + starterValue;
            
            return starterValue;
        }
    }
}