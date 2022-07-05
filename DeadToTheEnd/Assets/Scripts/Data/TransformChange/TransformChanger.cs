using Data.Combat;
using UnityEngine;

namespace Data.TransformChange
{
    public abstract class TransformChanger : ScriptableObject
    {
        [SerializeField] protected float Speed;

        public abstract void Change(GameObject gameObject, AttackData attackData);
        public abstract void SetData(AttackData attackData, GameObject gameObject);
    }
}