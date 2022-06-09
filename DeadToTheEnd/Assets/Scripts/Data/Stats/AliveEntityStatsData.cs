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

        private void CreateData()
        {
            var stats = new Dictionary<Characteristic, float[]>();

            foreach (var classCharacteristicsData in _aliveEntityData.AliveEntityStats)
            {
                stats[classCharacteristicsData.characteristic] = classCharacteristicsData.Value;
            }

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

            return levels[level - 1];
        }

        public int GetLevels(Characteristic characteristic)
        {
            CreateData();

            float[] levels = _classData[characteristic];
            return levels.Length;
        }
    }

    [Serializable]
    public class AliveEntityData
    {
        public AliveEntityStats[] AliveEntityStats;
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
        Agility
    }

    public enum Stat
    {
        Health,
        HealthRegeneration,
        Mana,
        ManaRegeneration,
        Damage,
    }

    public enum Class
    {
        Player,
        Warrior,
        BlueDragon,
        Goblin
    }
}