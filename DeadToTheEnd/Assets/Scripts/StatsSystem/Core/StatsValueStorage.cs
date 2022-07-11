using System;
using System.Collections.Generic;
using Entities.Core;
using GameCore.SaveSystem;
using UnityEngine;

namespace Data.Stats.Core
{
    public class StatsValueStorage : IDataSavable
    {
        private AliveEntityStatsModifierData _statsAccordingToClass;
        private AliveEntityStatsData _statsData;
        private LevelCalculator _levelCalculator;

        private Dictionary<Characteristic, int> _assignedPoints = new Dictionary<Characteristic, int>();
        private Dictionary<Characteristic, int> _transitionPoints = new Dictionary<Characteristic, int>();
        
        public int UnassignedPoints { get; private set; } = 18;
        private int _defaultLevelUpPoints = 5;
        
        public event Action OnStatsChange;

        public StatsValueStorage(AliveEntityStatsModifierData aliveEntityStatsModifierData,
            AliveEntityStatsData aliveEntityStatsData, LevelCalculator levelCalculator )
        {
            _statsAccordingToClass = aliveEntityStatsModifierData;
            _statsData = aliveEntityStatsData;
            _levelCalculator = levelCalculator;
            
            AddStat(Characteristic.Agility);
            AddStat(Characteristic.Intelligence);
            AddStat(Characteristic.Strength);
            
        }

      
        public float GetCalculatedStat(Stat stat)
        {
            if(_statsAccordingToClass.StatModifier.TryGetValue(stat, out var c))
            {
                if (_assignedPoints == null) return 0;
                foreach (var assignedPoint in _assignedPoints)
                {
                    if (assignedPoint.Key == c.Characteristic)
                    {
                        float f = assignedPoint.Value * c.Value;
                        return f;
                    }
                }

                return 0;
            }
            return 0;
        }

       
        public int GetProposedPoints(Characteristic characteristic)
        {
            return (GetPoints(characteristic) + GetPointInTransition(characteristic));
        }

        public void AssignPoints(Characteristic characteristic, int points)
        {
            if (!CanAssignPoints(characteristic, points)) return;

            _transitionPoints[characteristic] = GetPointInTransition(characteristic) + points;
            UnassignedPoints -= points;
            
            StatsChanged();
        }

        public bool CanAssignPoints(Characteristic characteristic, int points)
        {
            if (GetPointInTransition(characteristic) + points < 0) return false;
            if (UnassignedPoints < points) return false;

            return true;
        }

        public void Confirm()
        {
            foreach (var characteristic in _transitionPoints.Keys)
            {
                _assignedPoints[characteristic] = GetProposedPoints(characteristic);
            }

            StatsChanged();

            _transitionPoints.Clear();
        }

        public void StatsChanged()
        {
            OnStatsChange?.Invoke();
        }
       
        public ISerializable SerializableData()
        {
            var statsValueSavableStorage = new StatsValueSavableStorage
            {
                AssignedPoints = _assignedPoints,
                UnassignedPoints = UnassignedPoints
            };
            
            return statsValueSavableStorage;
        }
        public void RestoreSerializableData(ISerializable serializable)
        {
            if(serializable is not StatsValueSavableStorage serializableStats) return;
            _assignedPoints = serializableStats.AssignedPoints;
            UnassignedPoints = serializableStats.UnassignedPoints;
            
            StatsChanged();
        }
        
        public void AddNewUnassignedPoints()
        {
            UnassignedPoints += _defaultLevelUpPoints;
        }
        
        private void AddStat(Characteristic characteristic)
        {
            _assignedPoints.Add(characteristic, (int) _statsData.ReturnLevelValueCharacteristics(characteristic, _levelCalculator.Level));
        }

        private int GetPoints(Characteristic characteristic)
        {
            return _assignedPoints.ContainsKey(characteristic) ? _assignedPoints[characteristic] : 0;
        }

        private int GetPointInTransition(Characteristic characteristic)
        {
            return _transitionPoints.ContainsKey(characteristic) ? _transitionPoints[characteristic] : 0;
        }

    }

    [Serializable]
    public class StatsValueSavableStorage : ISerializable
    {
        public Dictionary<Characteristic, int> AssignedPoints;
        public int UnassignedPoints;
    }
}