using UnityEngine;

namespace SkillsSystem.SkillBonuses
{
    [CreateAssetMenu (menuName = "SkillSystem/Bonus/TimeableBonus")]
    public class StatTimeableBonus : StatBonus
    {
        [field: SerializeField] public float Time { get; private set; }
    }
}