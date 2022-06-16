using UnityEngine;

namespace SkillsSystem.Core
{
    public abstract class SkillPrefab : ScriptableObject
    {
        public abstract void SpawnPrefab(SkillData skillData);
    }
}