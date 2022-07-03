using System.Collections.Generic;

namespace Data.Stats.Core
{
    public interface IModifier
    {
        IEnumerable<IBonus> AddBonus(Stat[] stats);
    }
}