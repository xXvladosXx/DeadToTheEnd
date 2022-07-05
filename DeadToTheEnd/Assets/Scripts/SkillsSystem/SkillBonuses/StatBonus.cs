using Data.Stats;
using UnityEngine;
using UnityEngine.UI;

namespace SkillsSystem
{
    [CreateAssetMenu (menuName = "SkillSystem/Bonus/StatBonus")]
    public class StatBonus : ScriptableObject
    {
        [field: SerializeField] public Sprite Sprite { get; private set; }
        [field: SerializeField] public bool Positive { get; private set; }
        [field: SerializeField] public Stat Stat { get; private set; }
        [field: SerializeField] public float Value { get; private set; }
    }
}