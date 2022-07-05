using Data.Stats;
using StatsSystem;

namespace StatsSystem.Bonuses
{
    public class AttackSpeedBonus : IBonus
    {
        public AttackSpeedBonus(float speed) => Value = speed;
        public float Value { get; }
    }
}