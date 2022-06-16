using System;
using Data.TransformChange;
using UnityEngine;

namespace Combat.ColliderActivators
{
    public class ChangeableColliderActivator : AttackColliderActivator
    {
        [SerializeField] protected TransformChanger[] _transformChangers;

        private void Start()
        {
            foreach (var transformChanger in _transformChangers)
            {
                transformChanger.SetData(AttackData);
            }
        }

        protected override void Update()
        {
            base.Update();
            
            foreach (var transformChanger in _transformChangers)
            {
                transformChanger.Change(gameObject);
            }
        }
    }
}