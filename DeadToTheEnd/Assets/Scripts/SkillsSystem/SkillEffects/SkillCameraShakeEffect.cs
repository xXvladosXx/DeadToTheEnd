using System;
using Data.Camera;
using UnityEngine;

namespace SkillsSystem.SkillEffects
{
    [CreateAssetMenu (menuName = "SkillSystem/Effect/CameraShake")]
    public class SkillCameraShakeEffect : SkillEffect
    {
        [field: SerializeField] public ShakeCameraData ShakeCameraData { get; private set; }
        public override void ApplyEffect(SkillData skillData)
        {
            skillData.AttackData.ShakeCameraData = ShakeCameraData;
        }

        public override string Data()
        {
            return string.Empty;
        }
    }
}