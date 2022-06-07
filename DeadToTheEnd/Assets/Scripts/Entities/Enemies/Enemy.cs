using CameraManage;
using Combat.ColliderActivators;
using Data.Animations;
using Data.ScriptableObjects;
using Data.Stats;
using Entities.Core;
using UnityEngine;
using UnityEngine.AI;

namespace Entities.Enemies
{
    [RequireComponent(typeof(Rigidbody), typeof(NavMeshAgent),
        typeof(Animator))]
    public abstract class Enemy : AliveEntity, ILockable
    {
        [field: SerializeField] public EnemyAnimationData EnemyAnimationData { get; protected set; }
        [field: SerializeField] public EnemyData EnemyData { get; protected set; }
        
        [SerializeField] private Transform _lockAim;
        public NavMeshAgent NavMeshAgent { get; private set; }
        public Rigidbody Rigidbody { get; private set; }
        public Animator Animator { get; private set; }
        public DefenseColliderActivator DefenseColliderActivator { get; private set; }

        public override Health Health { get; protected set; }

        protected override void Awake()
        {
            base.Awake();

            DefenseColliderActivator = GetComponentInChildren<DefenseColliderActivator>();
            NavMeshAgent = GetComponent<NavMeshAgent>();
            Rigidbody = GetComponent<Rigidbody>();
            Animator = GetComponent<Animator>();
            
            EnemyAnimationData.Init();
            
            Target = GameObject.FindWithTag("Player").GetComponent<MainPlayer>();
        }

        private void Start()
        {
            StateMachine.ChangeState(StateMachine.StartState());
        }

        private void Update()
        {
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
    }
}