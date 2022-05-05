using System;
using Data.ScriptableObjects;
using Data.States;
using StateMachine.WarriorEnemy;
using UnityEngine;
using UnityEngine.AI;

namespace Entities
{
    [RequireComponent(typeof(Rigidbody), typeof(NavMeshAgent))]
    public class Enemy : AliveEntity
    {
        [field: SerializeField] public WarriorEnemyData WarriorEnemyData { get; private set; }
        [field: SerializeField] public EnemyStateReusableData EnemyStateReusableData { get; private set; }
        
        public NavMeshAgent NavMeshAgent { get; private set; }
        public Rigidbody Rigidbody { get; private set; }
        
        public MainPlayer MainPlayer { get; private set; }

        private WarriorStateMachine _warriorStateMachine;

        private void Awake()
        {
            EnemyStateReusableData.Initialize(this);

            NavMeshAgent = GetComponent<NavMeshAgent>();
            Rigidbody = GetComponent<Rigidbody>();
            MainPlayer = GameObject.FindWithTag("Player").GetComponent<MainPlayer>();

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
    }
}