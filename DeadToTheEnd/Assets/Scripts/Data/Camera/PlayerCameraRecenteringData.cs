using System;
using UnityEngine;

namespace Data.Camera
{
    [Serializable]
    public class PlayerCameraRecenteringData
    {
        [field: SerializeField] public float MinimumAngle { get; private set; }
        [field: SerializeField] public float MaximumAngle { get; private set; }
        [field: SerializeField] public float WaitTime { get; private set; }
        [field: SerializeField] public float RecenteringTime { get; private set; }

        public bool IsWithinRange(float angle) => angle >= MinimumAngle && angle <= MaximumAngle;
    }
}