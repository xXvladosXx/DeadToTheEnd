using System.Linq;
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

            if (SkillRequirements.Any(
                    skillRequirement => skillRequirement.IsChecked(_skillData.SkillUser) == false))
                        return;
            
            _skillForm.ApplyForm(_skillData);

            foreach (var skillEffect in SkillEffects)
            {
                skillEffect.ApplyEffect(_skillData);
            }

            foreach (var skillBonus in SkillBonuses)
            {
                skillBonus.ApplyBonus(_skillData);
            }

            foreach (var skillPrefabSpawn in SkillPrefabSpawns)
            {
                skillPrefabSpawn.SpawnPrefab(_skillData);
            }
        }

        public override float GetTime()
        {
            return _skillCooldown;
        }

        public override string GetInfoDisplayText()
        {
            return Description;
        }
    }
}