using System;
using UnityEngine;

namespace Data.States
{
    [Serializable]
    public sealed class PlayerAirborneData
    {
        [field: SerializeField] public PlayerJumpData PlayerJumpData { get; private set; }
        [field: SerializeField] public PlayerFallData PlayerFallData { get; private set; }
    }
}