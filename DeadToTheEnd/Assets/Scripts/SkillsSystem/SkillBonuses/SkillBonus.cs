using System;
using RotaryHeart.Lib.SerializableDictionary;
using SkillsSystem.SkillBonuses;
using UnityEngine;

namespace SkillsSystem
{
    [CreateAssetMenu (menuName = "SkillSystem/SkillBonus")]
    public class SkillBonus : ScriptableObject
    {
        [field: SerializeField] public StatTimeableBonus[] StatBonuses { get; private set; }
        public void ApplyBonus(SkillData skillData)
        {
        }

        public  string Data()
        {
            return "Damage bonus: <color=#8FCE00>125</color>";
        }
    }
}