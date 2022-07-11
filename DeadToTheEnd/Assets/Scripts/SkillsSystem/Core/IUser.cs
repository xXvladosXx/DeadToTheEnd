using System.Collections.Generic;
using Data.Stats;
using StatsSystem.Core;
using UnityEngine;

namespace SkillsSystem
{
    public interface IUser
    {
        StatsFinder StatsFinder { get; }
        LevelCalculator LevelCalculator { get; }
        List<IStatsable> Statsables { get; }
        
        List<IPointsAssignable> PointsAssignables { get; }
    }
}
