using System;
using Camera;
using Data.Animations;
using Data.ScriptableObjects;
using Data.States;
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

        private WarriorStateMachine _warriorStateMachine;

        private void Awake()
        {
            EnemyStateReusableData.Initialize(this);

            NavMeshAgent = GetComponent<NavMeshAgent>();
            Rigidbody = GetComponent<Rigidbody>();
            Animator = GetComponent<Animator>();
            MainPlayer = GameObject.FindWithTag("Player").GetComponent<MainPlayer>();
            
            WarriorEnemyAnimationData.Init();

            _warriorStateMachine = new WarriorStateMachine(this);
        }

        private void Start()
        {
            _warriorStateMachine.ChangeState(_warriorStateMachine.IdleWarriorEnemyState);
        }

        private void Update()
        {
            _warriorStateMachine.Update();
        }

        private void FixedUpdate()
        {
            _warriorStateMachine.FixedUpdate();
            
        }

        public void OnMovementStateAnimationEnterEvent()
        {
            _warriorStateMachine.OnAnimationEnterEvent();
        }

        public void OnMovementStateAnimationExitEvent()
        {
            _warriorStateMachine.OnAnimationExitEvent();
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
          
            Debug.Log("Moving");
        }

        public Transform Lock()
        {
            return _lockAim;
        }
    }
}