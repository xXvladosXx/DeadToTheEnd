using System;
using System.Linq;
using System.Text;
using Entities.Core;
using SkillsSystem.SkillForms;
using UnityEngine;

namespace SkillsSystem
{
    [CreateAssetMenu (menuName = "SkillSystem/ActiveSkill")]
    public class ActiveSkill : Skill
    {
        [SerializeField] private SkillForm _skillForm;
        [SerializeField] private float _skillCooldown;
        
        [field: SerializeField] public Requirement[] SkillRequirements { get; private set; }
        [field: SerializeField] public SkillEffect[] SkillEffects { get; private set; }
        [field: SerializeField] public SkillBonus[] SkillBonuses { get; private set; }
        [field: SerializeField] public SkillPrefabSpawn[] SkillPrefabSpawns { get; private set; }

        private SkillData _skillData;
        
        public override void ApplySkill(ISkillUser skillUser, int index = 0)
        {
            base.ApplySkill(skillUser,index);
            _skillData = new SkillData
            {
                SkillUser = skillUser as AliveEntity
            };

            _skillForm.ApplyForm(_skillData);

            foreach (var skillRequirement in SkillRequirements)
            {
                skillRequirement.ApplyRequirement(skillUser);
            }

            foreach (var skillEffect in SkillEffects)
            {
                skillEffect.ApplyEffect(_skillData);
            }

            _skillData.SkillUser.BuffManager.SetBuff(SkillBonuses);

            foreach (var skillBonus in SkillBonuses)
            {
                skillBonus.ApplyBonus(_skillData);
            }
            
            foreach (var skillPrefabSpawn in SkillPrefabSpawns)
            {
                skillPrefabSpawn.SpawnPrefab(_skillData);
            }
        }

        public bool CheckRequirementsToCast(ISkillUser skillUser)
        {
            if (SkillRequirements.Length == 0) return true;
            
            if (SkillRequirements.Any(skillRequirement => !skillRequirement.IsChecked(skillUser)))
                return false;
            
            return true;
        }

        public override float GetTime()
        {
            return _skillCooldown;
        }

        public override string GetInfoDisplayText()
        {
            StringBuilder = new StringBuilder();
           
            
            foreach (var skillEffect in SkillEffects)
            {
                if(skillEffect.Data() == string.Empty) continue;
                StringBuilder.Append(skillEffect.Data()).AppendLine();
            }

            foreach (var skillBonus in SkillBonuses)
            {
                StringBuilder.Append(skillBonus.Data()).AppendLine();
            }

            foreach (var skillPrefabSpawn in SkillPrefabSpawns)
            {
                StringBuilder.Append(skillPrefabSpawn.Data()).AppendLine();
            }
            
            foreach (var requirement in SkillRequirements)
            {
                StringBuilder.Append(requirement.Data()).AppendLine();
            }
            return StringBuilder.ToString();
        }
    }
}