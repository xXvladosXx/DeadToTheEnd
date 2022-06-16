using Data.Stats;
using UnityEngine;

namespace SkillsSystem.SkillsRequirements
{
    [CreateAssetMenu(menuName = "SkillSystem/Requirement/Stat")]
    public class SkillStatRequirement : SkillRequirement
    {
        [SerializeField] private Stat _stat;
        [SerializeField] private float _value;

        public override bool IsChecked(SkillData skillData)
        {
            return true;
        }
    }
}