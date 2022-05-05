using System;
using System.Collections.Generic;
using Data.Camera;
using UnityEngine;

namespace Data.States
{
    [Serializable]
    public sealed class PlayerIdleData
    {
        [field: SerializeField] public List<PlayerCameraRecenteringData> BackCameraRecenteringDatas { get; private set; }
    }
}