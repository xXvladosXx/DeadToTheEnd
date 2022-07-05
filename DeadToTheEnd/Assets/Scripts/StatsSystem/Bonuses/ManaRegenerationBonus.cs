using Data.Stats;

namespace StatsSystem.Bonuses
{
    public class ManaRegenerationBonus : IBonus
    {
        public ManaRegenerationBonus(float bonus) => Value = bonus;
        public float Value { get; }
    }
}