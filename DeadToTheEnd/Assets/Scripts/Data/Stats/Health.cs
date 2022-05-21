using System;
using Data.Combat;
using Data.States;
using Data.States.StateData;
using UnityEngine;

namespace Data.Stats
{
    public class Health
    {
        private readonly IReusable _reusableData;
        
        public event Action<AttackData> OnDamageTaken;
        public event Action OnAttackApplied;

        public Health(IReusable reusableData)
        {
            _reusableData = reusableData;
        }
        
        public void TakeDamage(AttackData attackData)
        {
            if(attackData == null) return;
            if (attackData.AttackType == AttackType.Knock)
            {
                OnAttackApplied?.Invoke();

                OnDamageTaken?.Invoke(attackData);
                return;
            }
            
            if (_reusableData.IsBlocking)
            {
                OnAttackApplied?.Invoke();
                return;
            }
            
            OnDamageTaken?.Invoke(attackData);
        }
    }
}