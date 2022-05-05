using System;
using UnityEngine;

namespace Data.States
{
    [Serializable]
    public sealed class RunData
    {
        [field: SerializeField] public float SpeedModifier { get; private set; } = 1;
    }
}