using CameraManage;
using Combat.ColliderActivators;
using Data.Animations;
using Data.ScriptableObjects;
using Data.Stats;
using Entities.Core;
using LootSystem;
using UnityEngine;
using UnityEngine.AI;

namespace Entities.Enemies
{
    [RequireComponent(typeof(NavMeshAgent))]
    public abstract class Enemy : AliveEntity, ILockable, IInteractable
    {
        [field: SerializeField] public EnemyAnimationData EnemyAnimationData { get; protected set; }
        [field: SerializeField] public PickableObject PickableObject { get; private set; }

        [SerializeField] private float _distanceOfRaycast = 15;
        [SerializeField] private float _distanceOfPickableRaycast = 3;
        [SerializeField] private Transform _lockAim;
        public NavMeshAgent NavMeshAgent { get; private set; }
      
        public DefenseColliderActivator DefenseColliderActivator { get; private set; }

        protected override void Awake()
        {
            base.Awake();

            DefenseColliderActivator = GetComponentInChildren<DefenseColliderActivator>();
            NavMeshAgent = GetComponent<NavMeshAgent>();
         
            EnemyAnimationData.Init();
        }

        private void Start()
        {
            StateMachine.ChangeState(StateMachine.StartState());
        }

        private void Update()
        {
            if(!Target.Health.IsDead)
                StateMachine.Update();
        }

        private void FixedUpdate()
        {
            StateMachine.FixedUpdate();
        }

        public Transform Lock()
        {
            return _lockAim;
        }

        public object ObjectOfInteraction()
        {
            return this;
        }

        public string TextOfInteraction()
        {
            return "";
        }

        public float GetDistanceOfRaycast => _distanceOfRaycast;

        protected override void OnDied()
        {
            base.OnDied();
            PickableObject.SpawnLoot(transform.position, _distanceOfPickableRaycast);
        }
    }
}