using System;
using Data.Combat;
using Entities;
using UnityEngine;

namespace Combat.ColliderActivators
{
    public class AttackColliderActivator : ColliderActivator
    {
        private float _time = -1;
        private AttackData _attackData;
        
        public event Action<AttackData> OnTargetHit;

        private void Update()
        {
            if (_time < 0 )
            {
                DeactivateCollider();
                return;
            }

            _time -= Time.deltaTime;
        }

        public override void ActivateCollider(float time, AttackData attackData = null)
        {
            base.ActivateCollider(time, attackData);
            _attackData = attackData;
            _time = time;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out AliveEntity aliveEntity))
            {
                if(aliveEntity == GetComponentInParent<AliveEntity>()) return;
                aliveEntity.Health.TakeDamage(_attackData);
                OnTargetHit?.Invoke(_attackData);
            }
        }
    }
}