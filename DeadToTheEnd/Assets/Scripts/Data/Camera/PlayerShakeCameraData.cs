using System;
using UnityEngine;

namespace Data.Camera
{
    [Serializable]
    public class PlayerShakeCameraData
    {
        [field: SerializeField] public float MediumAttackHitIntensity { get; private set; }
        [field: SerializeField] public float MediumAttackHitTime { get; private set; }
        [field: SerializeField] public float DamageTakenIntensity { get; private set; }
        [field: SerializeField] public float DamageTakenTime { get; private set; }
        [field: SerializeField] public float KnockDamageTakenIntensity { get; private set; }
        [field: SerializeField] public float KnockDamageTakenTime { get; private set; }
    }
}