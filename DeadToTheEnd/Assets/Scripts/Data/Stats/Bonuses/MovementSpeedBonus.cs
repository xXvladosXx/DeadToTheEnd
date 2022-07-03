using Data.Stats;

namespace StatsSystem.Bonuses
{
    public class MovementSpeedBonus : IBonus
    {
        public MovementSpeedBonus(float bonus) => Value = bonus;
        public float Value { get; }
    }
}