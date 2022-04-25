using System;
using UnityEngine;

namespace Data.Collider
{
    [Serializable]
    public class SlopData
    {
        [field: SerializeField] public float StepHeightPercentage { get; private set; } = .25f;
    }
}