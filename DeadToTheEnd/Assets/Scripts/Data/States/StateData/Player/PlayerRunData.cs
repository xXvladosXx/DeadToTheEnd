using System;
using UnityEngine;

namespace Data.States
{
    [Serializable]
    public sealed class PlayerRunData
    {
        [field: SerializeField] public float SpeedModifier { get; private set; } = 1;
        [field: SerializeField] public float StrafeSpeedModifier { get; private set; } = 1;
    }
}