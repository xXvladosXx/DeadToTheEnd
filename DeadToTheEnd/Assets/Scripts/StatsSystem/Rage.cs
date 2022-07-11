using System;
using System.Collections;
using Combat;
using Data.Stats;
using SkillsSystem;
using UnityEngine;
using Utilities;

namespace StatsSystem
{
    [Serializable]
    public class Rage
    {
        [field: SerializeField] public float RageValue { get; private set; }
        [field: SerializeField] public SkillBonus[] SkillBonuses { get; private set; }
        [field: SerializeField] public float RageLock { get; private set; }
        
        private BuffManager _buffManager;
        
        
        private float _maxRagePoints = 100;
        private float _defaultPointsPerAttack = 10;
        private bool _isLocked;
        
        public event Action<float> OnValuePctChanged;
        
        public void Init(BuffManager buffManager)
        {
            _buffManager = buffManager;
        }

        public void AddRagePoints()
        {
            if(_isLocked) return;
            
            RageValue += _defaultPointsPerAttack;
            RageValue = Mathf.Clamp(RageValue, 0, _maxRagePoints);

            if (RageValue == _maxRagePoints)
            {
                _buffManager.SetBuff(SkillBonuses);
                _isLocked = true;
                Coroutines.StartRoutine(StartRageLockedDelay());
                RageValue = 0;
            }

            var ragePct = ((RageValue / _maxRagePoints));
            OnValuePctChanged?.Invoke(ragePct);
        }

        public IEnumerator StartRageLockedDelay()
        {
            yield return new WaitForSeconds(RageLock);

            _isLocked = false;
        }

        public void RemoveRagePoints()
        {
            RageValue = 0;
            OnValuePctChanged?.Invoke(RageValue);
        }

    }
}