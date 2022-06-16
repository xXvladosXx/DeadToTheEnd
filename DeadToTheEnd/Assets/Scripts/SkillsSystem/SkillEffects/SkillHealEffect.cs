using UnityEngine;

namespace SkillsSystem.SkillEffects
{
    public class SkillHealEffect : SkillEffect
    {
        public override void ApplyEffect(SkillData skillData)
        {
            Debug.Log("Healed");
        }
    }
}