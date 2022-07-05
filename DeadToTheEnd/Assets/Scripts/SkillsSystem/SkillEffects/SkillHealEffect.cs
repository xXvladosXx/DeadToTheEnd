using UnityEngine;

namespace SkillsSystem.SkillEffects
{
    public class SkillHealEffect : SkillEffect
    {
        public override void ApplyEffect(SkillData skillData)
        {
            Debug.Log("Healed");
        }

        public override string Data()
        {
            return "Healing for 100 hp";
        }
    }
}