using Data.Stats;
using Entities.Core;
using UnityEngine;

namespace SkillsSystem.SkillsRequirements
{
    [CreateAssetMenu(menuName = "SkillSystem/Requirement/Stat")]
    public class StatRequirement : Requirement
    {
        [SerializeField] private Stat _stat;
        [SerializeField] private float _value;

        public override bool IsChecked(AliveEntity skillData)
        {
            return true;
        }
    }
}