using UnityEngine;

namespace SkillsSystem.SkillsRequirements
{
    [CreateAssetMenu (menuName = "SkillSystem/Requirement/Level")]

    public class SkillLevelRequirement: SkillRequirement
    {
        [SerializeField] private int _level;
        public override bool IsChecked(SkillData skillData) => skillData.SkillUser.LevelCalculator.Level >= _level;
    }
}