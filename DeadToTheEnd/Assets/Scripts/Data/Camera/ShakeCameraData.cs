using System;
using UnityEngine;

namespace Data.Camera
{
    [Serializable]
    public class ShakeCameraData
    {
        [field: SerializeField] public float MediumAttackHitIntensity { get; private set; }
        [field: SerializeField] public float MediumAttackHitTime { get; private set; }
        [field: SerializeField] public float EasyAttackHitIntensity { get; private set; }
        [field: SerializeField] public float EasyAttackHitTime { get; private set; }
        [field: SerializeField] public float KnockAttackHitIntensity { get; private set; }
        [field: SerializeField] public float KnockAttackHitTime { get; private set; }
        [field: SerializeField] public float DamageTakenIntensity { get; private set; }
        [field: SerializeField] public float DamageTakenTime { get; private set; }
        [field: SerializeField] public float KnockDamageTakenIntensity { get; private set; }
        [field: SerializeField] public float KnockDamageTakenTime { get; private set; }
    }
}