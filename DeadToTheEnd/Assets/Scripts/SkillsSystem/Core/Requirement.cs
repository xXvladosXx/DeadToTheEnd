using Entities.Core;
using UnityEngine;

namespace SkillsSystem
{
    public abstract class Requirement : ScriptableObject
    {
        public abstract bool IsChecked(ISkillUser skillUser);
        public abstract void ApplyRequirement(ISkillUser skillUser);
        public abstract string Data();
    }
}