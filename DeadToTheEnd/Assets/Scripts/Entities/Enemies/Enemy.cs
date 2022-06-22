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
    [RequireComponent(typeof(NavMeshAgent))]
    public abstract class Enemy : AliveEntity, ILockable
    {
        [field: SerializeField] public EnemyAnimationData EnemyAnimationData { get; protected set; }
        
        [SerializeField] private Transform _lockAim;
        public NavMeshAgent NavMeshAgent { get; private set; }
      
        public DefenseColliderActivator DefenseColliderActivator { get; private set; }

        protected override void Awake()
        {
            base.Awake();

            DefenseColliderActivator = GetComponentInChildren<DefenseColliderActivator>();
            NavMeshAgent = GetComponent<NavMeshAgent>();
         
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