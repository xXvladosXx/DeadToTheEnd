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
        
        public override bool IsChecked(AliveEntity skillData)
        {
            return true;
        }
    }
}