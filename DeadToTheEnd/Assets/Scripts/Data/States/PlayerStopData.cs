using System;
using UnityEngine;

namespace Data.States
{
    [Serializable]
    public class PlayerStopData
    {
        [field: SerializeField] public float LightDeceleration { get; private set; } = 5f;
        [field: SerializeField] public float MediumDeceleration { get; private set; } = 6.5f;
        [field: SerializeField] public float HardDeceleration { get; private set; } = 5f;
    }
}