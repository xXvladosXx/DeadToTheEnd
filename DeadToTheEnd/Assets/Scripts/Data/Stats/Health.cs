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
        
        public event Action OnAttackApplied;
        public event Action<AttackData> OnDamageTaken;

        public Health(IReusable reusableData)
        {
            _reusableData = reusableData;
        }
        
        public void TakeDamage(AttackData attackData)
        {
            if(attackData == null) return;
            Debug.Log("Damaged " + attackData.User);
            
            if(_reusableData.IsRolling) 
                return;

            if (attackData.AttackType == AttackType.Knock)
            {
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