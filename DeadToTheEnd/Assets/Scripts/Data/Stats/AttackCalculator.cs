using System;
using Data.Combat;
using Data.States;
using Data.States.StateData;
using UnityEngine;

namespace Data.Stats
{
    public class AttackCalculator
    {
        private readonly IReusable _reusableData;
        
        public event Action OnAttackApplied;
        public event Action<AttackData> OnDamageTaken;

        public AttackCalculator(IReusable reusableData)
        {
            _reusableData = reusableData;
        }
        
        public void TryToTakeDamage(AttackData attackData)
        {
            if(attackData == null) return;
            
            if(_reusableData.IsRolling) 
                return;

            if (_reusableData.IsTargetBehind)
            {
                OnDamageTaken?.Invoke(attackData);
                Debug.Log("Damaged " + attackData.User);

                return;
            }

            if (attackData.AttackType == AttackType.Knock)
            {
                Debug.Log("Damaged " + attackData.User);

                OnDamageTaken?.Invoke(attackData);
                return;
            }
            
            if (_reusableData.IsBlocking)
            {
                Debug.Log("From blocking");
                OnAttackApplied?.Invoke();
                return;
            }
            
            Debug.Log("Damaged " + attackData.User);

            OnDamageTaken?.Invoke(attackData);
        }
    }
}