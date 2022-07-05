using System.Collections.Generic;
using Entities.Core;
using UnityEngine;

namespace SkillsSystem
{
    public abstract class SkillForm : ScriptableObject
    {
        [SerializeField] private int _maxTargets;
        public abstract void ApplyForm(SkillData skillData);
        
        protected void FindTargets(SkillData skillData, int numColliders, Collider[] hitColliders)
        {
            skillData.SkillTargets = new List<AliveEntity>();
            
            for (int i = 0; i < numColliders; i++)
            {
                if (skillData.SkillTargets.Count > _maxTargets) return;
                var possibleTarget = hitColliders[i];

                if (possibleTarget.TryGetComponent(out AliveEntity skillTarget))
                {
                    if(skillTarget == skillData.SkillUser) continue;
                    
                    skillData.SkillTargets.Add(skillTarget);
                }
            }
        }
    }
}