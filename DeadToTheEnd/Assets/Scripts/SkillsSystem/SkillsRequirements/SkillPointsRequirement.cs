using UnityEngine;

namespace SkillsSystem.SkillsRequirements
{
    [CreateAssetMenu(menuName = "SkillSystem/Requirement/SkillPoints")]
    public class SkillPointsRequirement : PointsRequirement
    {
        public override bool IsChecked(IUser user)
        {
            foreach (var pointsAssignable in user.PointsAssignables)
            {
                if (pointsAssignable.GetType() == typeof(SkillManager))
                {
                    if (Value > pointsAssignable.UnassignedPoints)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public override void ApplyRequirement(IUser user)
        {
            foreach (var pointsAssignable in user.PointsAssignables)
            {
                if (pointsAssignable.GetType() == typeof(SkillManager))
                {
                    pointsAssignable.RemovePoints(Value);
                }
            }
        }
    }
}