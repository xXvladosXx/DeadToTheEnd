using UnityEngine;

namespace SkillsSystem
{
    public abstract class SkillBonus : ScriptableObject
    {
        public abstract void ApplyBonus(SkillData skillData);
    }
}