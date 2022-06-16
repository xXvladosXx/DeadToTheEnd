using UnityEngine;

namespace SkillsSystem.SkillEffects
{
    public abstract class SkillContinuousEffect : SkillEffect
    {
        [SerializeField] protected float _time;
        
        public abstract void ApplyContinuousEffect(SkillData skillData);
    }
}