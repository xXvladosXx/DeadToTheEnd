using Data.Stats;
using StatsSystem;

namespace StatsSystem.Bonuses
{
    public class AttackSpeedBonus : IBonus
    {
        public AttackSpeedBonus(float damage) => Value = damage;
        public float Value { get; }
    }
}