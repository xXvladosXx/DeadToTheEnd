using Data.Stats;
using Entities.Core;
using UnityEngine;

namespace SkillsSystem.SkillsRequirements
{
    [CreateAssetMenu(menuName = "SkillSystem/Requirement/Characteristic")]
    public class CharacteristicRequirement : Requirement
    {
        [SerializeField] private Characteristic _characteristic;
        [SerializeField] private float _value;
        
        public override bool IsChecked(ISkillUser skillData)
        {
            return true;
        }

        public override void ApplyRequirement(ISkillUser skillUser)
        {
            
        }

        public override string Data()
        {
            return $"Requires: {_characteristic} {_value}";
        }
    }
}