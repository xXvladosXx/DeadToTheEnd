using System;
using Data.Combat;
using UnityEngine;

namespace Data.States.StateData.Enemy
{
    [Serializable]
    public class EnemyComboData
    {
        [field: SerializeField] public float ComboFirstAttackCooldown { get; set; } = 10f;
        [field: SerializeField] public float ComboFirstAttackSpeed { get; set; } = 3;
        
        [field: SerializeField] public float ComboSecondAttackCooldown { get; set; } = 5f;
        [field: SerializeField] public float ComboSecondAttackSpeed { get; set; } = 10;

        [field: SerializeField] public float ComboFirstAttackDamage { get; set; } = 100;
        [field: SerializeField] public float ComboSecondAttackDamage { get; set; } = 100;
        
    }
}