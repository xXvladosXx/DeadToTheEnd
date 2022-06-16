using System.Collections.Generic;
using Data.Combat;
using Entities.Core;

namespace SkillsSystem
{
    public class SkillData
    {
        public AliveEntity SkillUser { get; set; } 
        public List<AliveEntity> SkillTargets { get; set; } 
        public AttackData AttackData { get; set; }
    }
}