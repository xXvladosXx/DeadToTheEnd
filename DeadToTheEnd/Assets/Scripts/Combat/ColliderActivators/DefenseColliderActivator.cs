using System;
using UnityEngine;

namespace Combat.ColliderActivators
{
    public class DefenseColliderActivator : ColliderActivator
    {
        [SerializeField] private GameObject _blockParticle;
        [SerializeField] private Transform _positionToSpawn;
        [SerializeField] private float _timeToDestroyParticle = 1f;

        private void Update()
        {
            if(enabled)
                print("Enabled");
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out LongSwordColliderActivator attackColliderActivator))
            {
                Debug.Log("Spawn");
                var particle = Instantiate(_blockParticle,  transform.position, Quaternion.identity);
                Destroy(particle, _timeToDestroyParticle);
            }
        }
    }
}