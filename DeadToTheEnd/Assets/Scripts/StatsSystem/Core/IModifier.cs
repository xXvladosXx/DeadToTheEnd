using System;
using System.Collections.Generic;

namespace Data.Stats.Core
{
    public interface IModifier
    {
        public event Action OnStatModified;
        IEnumerable<IBonus> AddBonus(Stat[] stats);
    }
}