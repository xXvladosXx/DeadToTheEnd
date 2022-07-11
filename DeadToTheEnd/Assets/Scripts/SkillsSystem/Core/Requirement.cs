using Entities.Core;
using UnityEngine;

namespace SkillsSystem
{
    public abstract class Requirement : ScriptableObject
    {
        public abstract bool IsChecked(IUser user);
        public abstract void ApplyRequirement(IUser user);
        public abstract string Data();
    }
}