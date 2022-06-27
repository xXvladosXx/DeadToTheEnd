using System;
using System.Collections.Generic;
using Combat.ColliderActivators;
using Data.Combat;
using Data.ScriptableObjects;
using Data.States.StateData;
using Data.Stats;
using StateMachine.Enemies.BlueGragon;
using UnityEngine;
using UnityEngine.Pool;

namespace Entities.Enemies
{
    public class StandardEnemy : Enemy
    {
        [SerializeField] private Transform _rightHand;
        [SerializeField] private Transform _leftHand;

        [field: SerializeField] public StandardEnemyData StandardEnemyData { get; private set; }

        [field: SerializeField]
        public StandardEnemyStateReusableData StandardEnemyStateReusableData { get; private set; }

        private RadiusAttackColliderActivator RadiusAttackColliderActivator { get; set; }

        private List<GameObject> _activeEffects = new List<GameObject>();
        
        //private ObjectPool<GameObject> _pool;

        protected override void Awake()
        {
            base.Awake();

            Reusable = new StandardEnemyStateReusableData();
            StandardEnemyStateReusableData = (StandardEnemyStateReusableData) Reusable;

            AttackCalculator = new AttackCalculator(StandardEnemyStateReusableData);
            StateMachine = new StandardEnemyStateMachine(this);

            RadiusAttackColliderActivator = GetComponentInChildren<RadiusAttackColliderActivator>();

            // _pool = new ObjectPool<GameObject>(() => { return Instantiate(_effect); },
            //     effect => { effect.gameObject.SetActive(true); },
            //     effect => { effect.gameObject.SetActive(false); },
            //     effect => { Destroy(effect.gameObject); },
            //     false, 10, 10);
        }

        private void OnAnimatorMove()
        {
            if (StandardEnemyStateReusableData.IsRotatingWithRootMotion)
            {
                transform.rotation *= Animator.deltaRotation;
                Rigidbody.velocity = Vector3.zero;
                return;
            }

            float delta = Time.deltaTime;
            Rigidbody.drag = 0;
            Vector3 deltaPosition = Animator.deltaPosition;
            deltaPosition.y = 0;
            Vector3 velocity = deltaPosition / delta;
            Rigidbody.AddForce(velocity);
        }

        public void OnRadiusAttack(float time, AttackType knock)
        {
            RadiusAttackColliderActivator.enabled = true;
            var attackData = CreateAttackData(knock);

            RadiusAttackColliderActivator.ActivateCollider(time, attackData);
        }

        public void OnHandRangeAttack(AttackType medium, GameObject projectile)
        {
            Vector3 target = new Vector3(Target.transform.position.x, transform.position.y,
                Target.transform.position.z);
            transform.LookAt(target);
            var spawnedProjectile = Instantiate(projectile,
                _rightHand.position,
                Quaternion.identity);

            var attackData = CreateAttackData(medium);
            spawnedProjectile.GetComponentInChildren<AttackColliderActivator>()
                .ActivateCollider(2f, attackData);

            foreach (var activeEffect in _activeEffects)
            {
                activeEffect.SetActive(false);
                Destroy(activeEffect);
            }

            _activeEffects.Clear();
        }

        public void OnEnergyCollect(GameObject particle, bool leftHand)
        {
            GameObject newPart = null;
            newPart = Instantiate(particle, leftHand ? _leftHand : _rightHand);

            _activeEffects.Add(newPart);
        }

        public void OnRangeAttack(AttackType knock, GameObject projectile)
        {
            var spawnedProjectile = Instantiate(projectile,
                Target.transform.position,
                Quaternion.identity);

            var attackData = CreateAttackData(knock);
            spawnedProjectile.GetComponentInChildren<AttackColliderActivator>()
                .ActivateCollider(2f, attackData);
        }
    }
}