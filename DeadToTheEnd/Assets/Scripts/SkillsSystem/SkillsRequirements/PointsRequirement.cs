using UnityEngine;

namespace SkillsSystem.SkillsRequirements
{
    public abstract class PointsRequirement : Requirement
    {
        [SerializeField] protected int Value;

        public override string Data()
        {
            return $"Requires points: {Value}";
        }
    }
}