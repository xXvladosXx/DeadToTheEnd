using Data.Combat;
using UnityEngine;

namespace SkillsSystem.SkillEffects
{
    [CreateAssetMenu (menuName = "SkillSystem/Effect/Damage")]

    public class SkillDamageEffect : SkillEffect
    {
        [SerializeField] private float _damage;
        [SerializeField] private AttackType _attackType;
        public override void ApplyEffect(SkillData skillData)
        {
            var attackData = new AttackData
            {
                AttackType = _attackType,
                Damage = _damage,
                User = skillData.SkillUser
            };

            skillData.AttackData = attackData;
                
            foreach (var target in skillData.SkillTargets)
            {
                target.AttackCalculator.TryToTakeDamage(attackData);
            }
        }

        public override string Data()
        {
            return $"Damage: <color=#D21C1C>{_damage}</color> \n" +
                   $"Example elemental damage: <color=#51AEF4>Ice</color> ";
        }
    }
}