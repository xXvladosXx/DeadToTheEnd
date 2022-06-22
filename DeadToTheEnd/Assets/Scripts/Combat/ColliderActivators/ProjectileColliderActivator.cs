using Entities.Core;
using UnityEngine;

namespace Combat.ColliderActivators
{
    public class ProjectileColliderActivator : ChangeableColliderActivator
    {
        [SerializeField] private GameObject _onCollision;
        [SerializeField] private float _timeToDestroy;

        protected override void Awake()
        {
            base.Awake();
            Destroy(gameObject, _timeToDestroy);
        }

        protected override void OnTriggerEnter(Collider other)
        {
            base.OnTriggerEnter(other);
            if (other.TryGetComponent(out AliveEntity aliveEntity))
            {
                if (aliveEntity == GetComponentInParent<AliveEntity>()) return;
                if (aliveEntity == AttackData.User) return;
                if (aliveEntity.gameObject.layer == AttackData.User.gameObject.layer) return;

                DeactivateCollider();
            }
        }
    }
}