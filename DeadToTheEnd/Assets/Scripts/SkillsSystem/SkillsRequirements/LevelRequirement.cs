using Entities.Core;
using UnityEngine;

namespace SkillsSystem.SkillsRequirements
{
    [CreateAssetMenu (menuName = "SkillSystem/Requirement/Level")]

    public class LevelRequirement: Requirement
    {
        [SerializeField] private int _level;
        public override bool IsChecked(IUser data) => data.LevelCalculator.Level >= _level;
        public override void ApplyRequirement(IUser user)
        {
            
        }

        public override string Data()
        {
            return $"Requires level: {_level}";
        }
    }
}