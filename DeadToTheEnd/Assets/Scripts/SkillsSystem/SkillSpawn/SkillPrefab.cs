using UnityEngine;

namespace SkillsSystem.Core
{
    public abstract class SkillPrefab : ScriptableObject
    {
        [TextArea(2, 5)]
        [SerializeField] protected string Description;
        public abstract void SpawnPrefab(SkillData skillData);

        public virtual string Data()
        {
            return Description;
        }
    }
}