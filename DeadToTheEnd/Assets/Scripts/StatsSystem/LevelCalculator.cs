using System;
using GameCore.SaveSystem;
using SaveSystem;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.VFX;

namespace Data.Stats
{
    [Serializable]
    public class LevelCalculator : IDataSavable
    {
        [field: SerializeField] public VisualEffect LevelUp { get; private set; }
        [field: SerializeField] public int Level { get; private set; } = 1;
        public float GetExpPct => _currentXP/_aliveEntityStatsData.GetExperience(Experience.ExperienceToLevelUp, Level);
    
        private float _currentXP;
        private float _maxXP;
        private int _lastLevel;

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
                LevelUp.gameObject.SetActive(true);
                LevelUp.Play();
                
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
                CurrentLevel = Level,
                LastLevel = Level,
                Experience = _currentXP
            };

        public void RestoreSerializableData(ISerializable serializable)
        {
            if(serializable is not SavableLevel serializableLevel) return;
            Level = serializableLevel.CurrentLevel;
            _lastLevel = serializableLevel.LastLevel;
            
            _currentXP = serializableLevel.Experience;
        }



        [Serializable]
        public class SavableLevel : ISerializable
        {
            public int CurrentLevel;
            public int LastLevel;
            public float Experience;
        }
    }
}