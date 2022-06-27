using System;
using Data.Combat;
using UnityEngine;

namespace Data.Stats
{
    [Serializable]
    public class Health : IStatsable
    {
        [field: SerializeField] public float HealthValue { get; private set; }
        public float GetMaxHealth => _statsFinder.GetStat(Stat.Health);
        public event Action<float> OnHealthPctChanged = delegate{ };
        
        private StatsFinder _statsFinder;
        private bool _isDead;

        public event Action OnDied;
        public Health(StatsFinder statsFinder)
        {
            _statsFinder = statsFinder;
            HealthValue = _statsFinder.GetStat(Stat.Health);
        }
        
        public void DecreaseHealth(AttackData attackData)
        {
            if(_isDead) return;
            
            HealthValue -= attackData.Damage;
            if (HealthValue <= 0)
            {
                _isDead = true;
                OnDied?.Invoke();
                Debug.Log("Died");
                attackData.User.LevelCalculator.ExperienceReward(_statsFinder.GetExperienceReward());
            }
            
            float currentHealthPct = HealthValue / _statsFinder.GetStat(Stat.Health);
            OnHealthPctChanged?.Invoke(currentHealthPct);
        }

        public void RecalculateStat()
        {
            HealthValue = _statsFinder.GetStat(Stat.Health);
            Debug.Log(HealthValue);
        }
    }
}