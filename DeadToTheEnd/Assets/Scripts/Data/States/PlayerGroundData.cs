using System;
using UnityEngine;

namespace Data.States
{
    [Serializable]
    public class PlayerGroundData
    {
        [field: SerializeField] public float BaseSpeed { get; private set; } = 5f;
        [field: SerializeField] public PlayerRotationData BaseRotationData { get; private set; }
        [field: SerializeField] public PlayerWalkData WalkData { get; private set; }
        [field: SerializeField] public PlayerRunData RunData { get; private set; }
    }
}