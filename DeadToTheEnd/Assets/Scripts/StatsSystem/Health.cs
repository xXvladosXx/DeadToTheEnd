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
            HealthValue = GetMaxHealth;
        }

        public void Increase(float value)
        {
            HealthValue += value;
            
            if (HealthValue > GetMaxHealth)
            {
                HealthValue = GetMaxHealth;
            }

            InvokeHealthChange();
        }
        
        public void Decrease(float value)
        {
            HealthValue -= value;
            
            if (HealthValue <= 0)
            {
                _isDead = true;
                OnDied?.Invoke();
            }

            InvokeHealthChange();
        }
        
        public Stat Stat => Stat.Health;

        public void DecreaseHealth(AttackData attackData)
        {
            if(_isDead) return;
            
            HealthValue -= attackData.Damage;
            if (HealthValue <= 0)
            {
                _isDead = true;
                OnDied?.Invoke();
                attackData.User.LevelCalculator.ExperienceReward(_statsFinder.GetExperienceReward());
            }
            
            InvokeHealthChange();
        }
        
        public void RecalculateStatWithMaxValue()
        {
            HealthValue = GetMaxHealth;
            InvokeHealthChange();
        }

        public void RecalculateStatWithCurrentValue()
        {
            InvokeHealthChange();
        }

        public float GetStatValue(Stat stat)
        {
            if (stat == Stat.Health)
                return HealthValue;

            return 0;
        }

        private void InvokeHealthChange()
        {
            float currentHealthPct = HealthValue / GetMaxHealth;
            OnHealthPctChanged?.Invoke(currentHealthPct);
        }

       
    }
}