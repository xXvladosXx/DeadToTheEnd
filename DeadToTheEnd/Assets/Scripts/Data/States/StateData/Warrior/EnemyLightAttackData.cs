using System;
using UnityEngine;

namespace Data.States.StateData.Enemy
{
    [Serializable]
    public class EnemyLightAttackData
    {
        [field: SerializeField] public float TimeToEndAttack { get; private set; } = 2f;
    }
}