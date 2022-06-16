using Data.Stats;
using UnityEngine;

namespace SkillsSystem.SkillsRequirements
{
    [CreateAssetMenu(menuName = "SkillSystem/Requirement/Characteristic")]
    public class SkillCharacteristicRequirement : SkillRequirement
    {
        [SerializeField] private Characteristic _characteristic;
        [SerializeField] private float _value;
        
        public override bool IsChecked(SkillData skillData)
        {
            return true;
        }
    }
}