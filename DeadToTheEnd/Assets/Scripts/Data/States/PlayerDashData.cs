using System;
using UnityEngine;

namespace Data.States
{
    [Serializable]
    public class PlayerDashData
    {
        [field: SerializeField] public float SpeedModifier { get; private set; } = 2f;
        [field: SerializeField] public float TimeToBeConsider { get; private set; } = .3f;
        [field: SerializeField] public int DashLimitAmount { get; private set; } = 2;
        [field: SerializeField] public float DashLimitCooldown { get; private set; } = .3f;
        [field: SerializeField] public PlayerRotationData RotationData { get; private set; }
    }
}