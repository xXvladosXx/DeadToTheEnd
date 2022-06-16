using System;
using Unity.VisualScripting;
using UnityEngine;

namespace Data.Stats
{
    [Serializable]
    public class LevelCalculator
    {
        [field: SerializeField] public int Level { get; private set; } = 1;

        private float _currentXP;
        private float _maxXP;

        private AliveEntityStatsData _aliveEntityStatsData;
        
        public event Action<int> OnLevelUp;

        public void Init(AliveEntityStatsData aliveEntityStatsData)
        {
            _aliveEntityStatsData = aliveEntityStatsData;
            _currentXP = 0;
        }

        public void ExperienceReward(float experience)
        {
            _currentXP += experience;
            _maxXP = _aliveEntityStatsData.GetExperience(Experience.ExperienceToLevelUp, Level);
            
            if (CalculateLevel() > Level)
            {
                Level = CalculateLevel();
                OnLevelUp?.Invoke(Level);
            }
        }
        
        public int CalculateLevel()
        {
            int maxLevel = _aliveEntityStatsData.GetLevels();

            for (int level = 1; level < maxLevel; level++)
            {
                float expToLevelUp = _aliveEntityStatsData.GetExperience(Experience.ExperienceToLevelUp, level);

                if (expToLevelUp > _currentXP)
                {
                    return level;
                }
            }

            return maxLevel + 1;
        }
    }
}