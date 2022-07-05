using UnityEngine;

namespace SkillsSystem
{
    public abstract class SkillEffect : ScriptableObject
    {
        public abstract void ApplyEffect(SkillData skillData);
        public abstract string Data();
    }
}