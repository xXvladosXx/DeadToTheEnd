using System.Collections.Generic;
using Data.Stats;
using UnityEngine;

namespace SkillsSystem
{
    public interface ISkillUser
    {
        StatsFinder StatsFinder { get; }
        LevelCalculator LevelCalculator { get; }
        List<IStatsable> Statsables { get; }
    }
}
