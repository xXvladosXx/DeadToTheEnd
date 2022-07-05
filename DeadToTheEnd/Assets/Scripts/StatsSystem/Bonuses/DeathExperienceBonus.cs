using Data.Stats;

namespace StatsSystem.Bonuses
{
    public class DeathExperienceBonus: IBonus
    {
        public DeathExperienceBonus(float bonus) => Value = bonus;

        public float Value { get; }
    }
}