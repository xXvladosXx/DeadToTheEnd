using Entities.Core;
using UnityEngine;

namespace SkillsSystem.SkillsRequirements
{
    [CreateAssetMenu (menuName = "SkillSystem/Requirement/Level")]

    public class LevelRequirement: Requirement
    {
        [SerializeField] private int _level;
        public override bool IsChecked(AliveEntity skillData) => skillData.LevelCalculator.Level >= _level;
    }
}