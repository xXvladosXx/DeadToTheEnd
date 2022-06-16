using System;
using CameraManage;
using Data.Combat;
using Data.Stats;
using Data.TransformChange;
using Entities;
using Entities.Core;
using UnityEngine;

namespace Combat.ColliderActivators
{
    public class AttackColliderActivator : ColliderActivator
    {
        private float _time = -1;
        protected AttackData AttackData;

        public event Action<AttackData> OnTargetHit;
        
        protected virtual void Update()
        {
            if (_time < 0 )
            {
                DeactivateCollider();
                enabled = false;
                return;
            }

            _time -= Time.deltaTime;
        }

        public override void ActivateCollider(float time, AttackData attackData = null)
        {
            base.ActivateCollider(time, attackData);
            AttackData = attackData;
            _time = time;
        }

        private void OnTriggerEnter(Collider other)
        {
            if(!enabled) return;
           
            if (other.TryGetComponent(out AliveEntity aliveEntity))
            {
                if(aliveEntity == GetComponentInParent<AliveEntity>()) return;
                if(aliveEntity == AttackData.User) return;
               
                aliveEntity.AttackCalculator.TryToTakeDamage(AttackData);
                OnTargetHit?.Invoke(AttackData);
            }
        }
    }
}