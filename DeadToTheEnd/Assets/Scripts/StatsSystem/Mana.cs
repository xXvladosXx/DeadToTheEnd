using System;
using Data.Combat;
using UnityEngine;

namespace Data.Stats
{
    public class Mana: IStatsable
    {
        [field: SerializeField] public float ManaValue { get; private set; }
        public float GetMaxMana => _statsFinder.GetStat(Stat.Mana);
        public event Action<float> OnManaPctChanged = delegate{ };
        
        private StatsFinder _statsFinder;
        private bool _isDead;

        public event Action OnDied;
        public Mana(StatsFinder statsFinder)
        {
            _statsFinder = statsFinder;
            ManaValue = GetMaxMana;
        }

        public void Increase(float value)
        {
            ManaValue += value;
            
            if (ManaValue > GetMaxMana)
            {
                ManaValue = GetMaxMana;
            }

            InvokeManaChange();
        }
        
        public void Decrease(float value)
        {
            ManaValue -= value;
            
            if (ManaValue < 0)
            {
                ManaValue = 0;
            }
            
            InvokeManaChange();
        }
        
        public Stat Stat => Stat.Mana;

        public void RecalculateStatWithMaxValue()
        {
            ManaValue = GetMaxMana;
            InvokeManaChange();
        }

        public void RecalculateStatWithCurrentValue()
        {
            
        }

        public float GetStatValue(Stat stat)
        {
            if (stat == Stat.Mana)
                return ManaValue;

            return 0;
        }
        private void InvokeManaChange()
        {
            float currentHealthPct = ManaValue / GetMaxMana;
            OnManaPctChanged?.Invoke(currentHealthPct);
        }
        

    }
}