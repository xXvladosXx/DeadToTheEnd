using System;
using UnityEngine;

namespace Data.States
{
    [Serializable]
    public class PlayerWalkData
    {
        [field: SerializeField] public float SpeedModifier { get; private set; } = 0.225f;
    }
}