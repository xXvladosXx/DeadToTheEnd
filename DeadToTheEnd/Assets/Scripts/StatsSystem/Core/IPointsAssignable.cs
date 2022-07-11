using System;

namespace StatsSystem.Core
{
    public interface IPointsAssignable
    {
        public event Action OnPointsChange;
        public int UnassignedPoints { get; }
        public void RemovePoints(int value);
    }
}