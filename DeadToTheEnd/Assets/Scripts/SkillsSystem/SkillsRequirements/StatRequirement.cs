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

        public override bool IsChecked(IUser data)
        {
            foreach (var statsable in data.Statsables)
            {
                if (statsable.Stat == _stat)
                {
                    if (statsable.GetStatValue(_stat) < _value)
                    {
                        return false;
                    }
                }
            }
            
            return true;
        }

        public override void ApplyRequirement(IUser user)
        {
            foreach (var statsable in user.Statsables)
            {
                if(statsable.Stat == _stat)
                    statsable.Decrease(_value);
            }
        }

        public override string Data()
        {
            return $"Requires: {_stat} {_value}";
        }
    }
}