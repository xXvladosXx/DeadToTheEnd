using Entities.Core;
using UnityEngine;

namespace SkillsSystem
{
    public abstract class Requirement : ScriptableObject
    {
        public abstract bool IsChecked(AliveEntity skillUser);
    }
}