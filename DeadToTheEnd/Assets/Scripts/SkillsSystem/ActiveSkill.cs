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
        
        [field: SerializeField] public SkillRequirement[] SkillRequirements { get; private set; }
        [field: SerializeField] public SkillEffect[] SkillEffects { get; private set; }
        [field: SerializeField] public SkillBonus[] SkillBonuses { get; private set; }
        [field: SerializeField] public SkillPrefabSpawn[] SkillPrefabSpawns { get; private set; }

        private SkillData _skillData;
        public override void ApplySkill(ISkillUser skillUser)
        {
            base.ApplySkill(skillUser);

            _skillData = new SkillData
            {
                SkillUser = skillUser as AliveEntity
            };

            if (SkillRequirements.Any(
                    skillRequirement => skillRequirement.IsChecked(_skillData) == false))
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
        }


        public void SpawnPrefab()
        {
            foreach (var skillPrefabSpawn in SkillPrefabSpawns)
            {
                skillPrefabSpawn.SpawnPrefab(_skillData);
            }
        }
    }
}