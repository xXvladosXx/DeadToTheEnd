using Data.Stats;
using StatsSystem;

namespace StatsSystem.Bonuses
{
    public class CriticalDamageBonus : IBonus
    {
        public CriticalDamageBonus(float bonus) => Value = bonus;
        public float Value { get; }
    }
}