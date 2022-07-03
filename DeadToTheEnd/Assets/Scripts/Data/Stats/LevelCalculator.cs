using System;
using GameCore.SaveSystem;
using SaveSystem;
using Unity.VisualScripting;
using UnityEngine;

namespace Data.Stats
{
    [Serializable]
    public class LevelCalculator : IDataSavable
    {
        [field: SerializeField] public int Level { get; private set; } = 1;
        public float GetExpPct => _currentXP/_aliveEntityStatsData.GetExperience(Experience.ExperienceToLevelUp, Level);
    
        private float _currentXP;
        private float _maxXP;

        private AliveEntityStatsData _aliveEntityStatsData;
        
        public event Action<int> OnLevelUp;
        public event Action<float> OnExperiencePctChanged = delegate{ };


        public void Init(AliveEntityStatsData aliveEntityStatsData)
        {
            _aliveEntityStatsData = aliveEntityStatsData;
            _currentXP = 0;
        }

        public void ExperienceReward(float experience)
        {
            _currentXP += experience;
            _maxXP = _aliveEntityStatsData.GetExperience(Experience.ExperienceToLevelUp, Level);
            float currentXpPct = _currentXP / _maxXP;
            OnExperiencePctChanged?.Invoke(currentXpPct);
            
            if (CalculateLevel() > Level)
            {
                Level = CalculateLevel();
                _currentXP = 0;

                OnLevelUp?.Invoke(Level);
                _maxXP = _aliveEntityStatsData.GetExperience(Experience.ExperienceToLevelUp, Level);
                currentXpPct = _currentXP / _maxXP;
                OnExperiencePctChanged?.Invoke(currentXpPct);
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

        public ISerializable SerializableData() =>
            new SavableLevel
            {
                Level = Level,
                Experience = _currentXP
            };

        public void RestoreSerializableData(ISerializable serializable)
        {
            if(serializable is not SavableLevel serializableLevel) return;
            Level = serializableLevel.Level;
            _currentXP = serializableLevel.Experience;
        }


        [Serializable]
        public class SavableLevel : ISerializable
        {
            public int Level;
            public float Experience;
        }
    }
}