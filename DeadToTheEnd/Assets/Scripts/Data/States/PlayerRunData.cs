using System;
using UnityEngine;

namespace Data.States
{
    [Serializable]
    public class PlayerRunData
    {
        [field: SerializeField] public float SpeedModifier { get; private set; } = 1;
    }
}