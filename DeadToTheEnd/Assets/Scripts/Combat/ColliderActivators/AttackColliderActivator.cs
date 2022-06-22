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
        protected CameraShaker CameraShaker;

        public event Action<AttackData> OnTargetHit;

        protected override void Awake()
        {
            base.Awake();
            CameraShaker = new CameraShaker();
        }

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

        protected virtual void OnTriggerEnter(Collider other)
        {
            if(!enabled) return;
           
            if (other.TryGetComponent(out AliveEntity aliveEntity))
            {
                if(aliveEntity == GetComponentInParent<AliveEntity>()) return;
                if(aliveEntity == AttackData.User) return;
                if(aliveEntity.gameObject.layer == AttackData.User.gameObject.layer) return;
               
                aliveEntity.AttackCalculator.TryToTakeDamage(AttackData);
                CameraShaker.ShakeCameraOnAttackHit(AttackData);

                Debug.Log("Hit");
                OnTargetHit?.Invoke(AttackData);
            }
        }
    }
}