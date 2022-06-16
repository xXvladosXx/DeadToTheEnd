using System;
using System.Collections.Generic;
using UnityEngine;

namespace Data.Stats
{
    [CreateAssetMenu(menuName = "Stats/AliveEntityData")]
    public class AliveEntityStatsData : ScriptableObject
    {
        [SerializeField] private AliveEntityData _aliveEntityData;
        
        private Dictionary<Characteristic, float[]> _classData;
        private AliveEntityLevelData[] _experienceData;

        private void CreateData()
        {
            if(_classData != null) return;

            var stats = new Dictionary<Characteristic, float[]>();
            var experienceData = _aliveEntityData.AliveEntityLevelData; 

            foreach (var classCharacteristicsData in _aliveEntityData.AliveEntityStats)
            {
                stats[classCharacteristicsData.characteristic] = classCharacteristicsData.Value;
            }

            _experienceData = experienceData;
            _classData = stats;
        }

        public float ReturnLevelValueCharacteristics(Characteristic characteristic, int level)
        {
            CreateData();

            float[] levels = _classData[characteristic];
            
            if (levels.Length <= 0)
            {
                return 0;
            }
            
            return levels[level-1];
        }

        public int GetLevels() => _experienceData.Length;

        public float GetExperience(Experience experience, int level)
        {
            int levels = _experienceData.Length;
            if (level >= levels || levels <= 0)
                return 0;
            
            if(_experienceData[level].experience == experience)
                return _experienceData[level].Value;

            return 0;
        }
    }

    [Serializable]
    public class AliveEntityData
    {
        public AliveEntityStats[] AliveEntityStats;
        public AliveEntityLevelData[] AliveEntityLevelData;
    }

    [Serializable]
    public class AliveEntityLevelData
    {
        public Experience experience;
        public float Value;
    }
    
    [Serializable]
    public class AliveEntityStats
    {
        public Characteristic characteristic;
        public float[] Value;
    }

    public enum Characteristic
    {
        Strength,
        Intelligence,
        Agility,
    }

    public enum Stat
    {
        Health,
        HealthRegeneration,
        Mana,
        ManaRegeneration,
        Damage,
    }

    public enum Experience
    {
        ExperienceToLevelUp,
        ExperienceReward
    }

    public enum Class
    {
        Player,
        Warrior,
        BlueDragon,
        Goblin
    }
}