using Data.Stats;

namespace StatsSystem.Bonuses
{
    public class AccuracyBonus : IBonus
    {
        public AccuracyBonus(float bonus) => Value = bonus;
        public float Value { get; }
    }
}