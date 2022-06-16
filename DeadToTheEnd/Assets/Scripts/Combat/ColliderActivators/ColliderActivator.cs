using Data.Combat;
using UnityEngine;

namespace Combat.ColliderActivators
{
    public class ColliderActivator : MonoBehaviour
    {
        protected Collider _collider;

        protected virtual void Awake()
        {
            _collider = GetComponent<Collider>();
            _collider.enabled = false;
        }
        
        public virtual void ActivateCollider(float time = .2f, AttackData attackData = null)
        {
            _collider.enabled = true;
        }
        public virtual void DeactivateCollider()
        {
            _collider.enabled = false;
        }
    }
}