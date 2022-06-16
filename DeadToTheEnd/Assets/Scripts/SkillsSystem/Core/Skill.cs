using UnityEngine;

namespace SkillsSystem
{
    public abstract class Skill : ScriptableObject
    {
        public virtual void ApplySkill(ISkillUser skillUser){}
    }
}