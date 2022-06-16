using Entities.Core;
using UnityEngine;

namespace SkillsSystem.SkillPrefab
{
    public interface IMovable 
    {
        void Move(GameObject gameObject, Vector3 direction, float speed);
    }
}