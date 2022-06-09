using System;
using Data.Combat;
using UnityEngine;

namespace Data.Stats
{
    [Serializable]
    public class Health
    {
        [field: SerializeField] public float HealthValue { get; private set; }

        public Health(float value)
        {
            HealthValue = value;
        }
        
        public void DecreaseDamage(AttackData attackData)
        {
            HealthValue -= attackData.Damage;
            Debug.Log(attackData.Damage);

            if (HealthValue <= 0)
            {
                Debug.Log("HealthValue");
            }
        }
    }
}