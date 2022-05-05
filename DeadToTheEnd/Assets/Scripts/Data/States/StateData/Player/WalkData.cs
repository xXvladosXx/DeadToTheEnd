using System;
using System.Collections.Generic;
using Data.Camera;
using UnityEngine;

namespace Data.States
{
    [Serializable]
    public sealed class WalkData
    {
        [field: SerializeField] public float SpeedModifier { get; private set; } = 0.225f;
        [field: SerializeField] public List<PlayerCameraRecenteringData> BackCameraRecenteringDatas { get; private set; }

    }
}