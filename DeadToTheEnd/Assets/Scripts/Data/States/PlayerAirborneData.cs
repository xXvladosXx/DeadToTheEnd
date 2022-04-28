using System;
using UnityEngine;

namespace Data.States
{
    [Serializable]
    public class PlayerAirborneData
    {
        [field: SerializeField] public PlayerJumpData PlayerJumpData { get; private set; }
        [field: SerializeField] public PlayerFallData PlayerFallData { get; private set; }
    }
}