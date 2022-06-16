using UnityEngine;

namespace SkillsSystem
{
    public abstract class SkillRequirement : ScriptableObject
    {
        public abstract bool IsChecked(SkillData skillData);
    }
}