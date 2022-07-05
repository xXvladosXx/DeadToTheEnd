namespace Data.Stats
{
    public interface IStatsable
    {
        void RecalculateStatWithMaxValue();
        void RecalculateStatWithCurrentValue();
        float GetStatValue(Stat stat);
        Stat Stat { get; }
        void Increase(float value);
        void Decrease(float value);
    }
}