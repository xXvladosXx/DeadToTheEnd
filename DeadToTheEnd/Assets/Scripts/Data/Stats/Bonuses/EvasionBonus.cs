using Data.Stats;

namespace StatsSystem.Bonuses
{
    public class EvasionBonus : IBonus
    {
        public EvasionBonus(float bonus) => Value = bonus;

        public float Value { get; }
    }
}