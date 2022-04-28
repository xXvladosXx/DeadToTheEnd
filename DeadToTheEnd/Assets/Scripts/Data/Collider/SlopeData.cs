using System;
using UnityEngine;

namespace Data.Collider
{
    [Serializable]
    public class SlopeData
    {
        [field: SerializeField] public float StepHeightPercentage { get; private set; } = .25f;
        [field: SerializeField] public float FloatRayDistance { get; private set; } = 2f;
        [field: SerializeField] public float StepReachForce { get; private set; } = 25f;
    }
}