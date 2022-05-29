using System;
using CameraManage;
using Combat;
using Combat.ColliderActivators;
using Data.Animations;
using Data.Combat;
using Data.ScriptableObjects;
using Data.States;
using Data.Stats;
using StateMachine.WarriorEnemy;
using UnityEngine;
using UnityEngine.AI;

namespace Entities
{
    [RequireComponent(typeof(Rigidbody), typeof(NavMeshAgent), typeof(Animator))]
    public class Enemy : AliveEntity, ILockable
    {
        [SerializeField] private Transform _lockAim;
        [field: SerializeField] public WarriorEnemyData WarriorEnemyData { get; private set; }
        [field: SerializeField] public EnemyStateReusableData EnemyStateReusableData { get; private set; }
        [field: SerializeField] public WarriorEnemyAnimationData WarriorEnemyAnimationData { get; private set; }

        public NavMeshAgent NavMeshAgent { get; private set; }
        public Rigidbody Rigidbody { get; private set; }
        public Animator Animator { get; private set; }
        
        public MainPlayer MainPlayer { get; private set; }
        
        public override Health Health { get; protected set; }

        protected override void Awake()
        {
            base.Awake();
            EnemyStateReusableData.Initialize(this);

            Health = new Health(EnemyStateReusableData);
            NavMeshAgent = GetComponent<NavMeshAgent>();
            Rigidbody = GetComponent<Rigidbody>();
            Animator = GetComponent<Animator>();
            MainPlayer = GameObject.FindWithTag("Player").GetComponent<MainPlayer>();
            
            WarriorEnemyAnimationData.Init();

            StateMachine = new WarriorStateMachine(this);
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

        private void OnAnimatorMove()
        {
            if (EnemyStateReusableData.IsRotatingWithRootMotion)
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
        }

        public Transform Lock()
        {
            return _lockAim;
        }
    }
}