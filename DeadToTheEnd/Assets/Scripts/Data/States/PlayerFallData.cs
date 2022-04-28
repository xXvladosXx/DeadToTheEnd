using System;
using UnityEngine;

namespace Data.States
{
    [Serializable]
    public class PlayerFallData
    {
        [field: SerializeField] public float SpeedLimit { get; private set; } = 15f;
    }
}