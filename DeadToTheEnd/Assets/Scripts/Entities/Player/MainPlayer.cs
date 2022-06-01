using System;
using CameraManage;
using Combat.ColliderActivators;
using Combat.SwordActivators;
using Data.Animations;
using Data.Camera;
using Data.Combat;
using Data.Layers;
using Data.ScriptableObjects;
using Data.States;
using Data.Stats;
using Entities.Core;
using StateMachine;
using UnityEngine;
using Utilities;
using Utilities.Camera;
using Utilities.Collider;

namespace Entities
{
    [RequireComponent(typeof(PlayerInput), typeof(Rigidbody))]
    public sealed class MainPlayer : AliveEntity
    {
        [field:SerializeField] public SwordActivator LongSwordActivator { get; private set; }
        [field:SerializeField] public ShortSwordActivator[] ShortSwordsActivator { get; private set; }
        [field: SerializeField] public PlayerData PlayerData { get; private set; }
        [field: SerializeField] public PlayerColliderUtility ColliderUtility { get; private set; }
        [field: SerializeField] public PlayerLayerData PlayerLayerData { get; private set; }
        [field: SerializeField] public PlayerCameraUtility CameraUtility { get; private set; }
        [field: SerializeField] public PlayerAnimationData PlayerAnimationData { get; private set; }
        [field: SerializeField] public PlayerStateReusableData ReusableData { get; set; }
        public Transform MainCamera { get; private set; }
        public PlayerInput InputAction { get; private set; }
        public Rigidbody Rigidbody { get; private set; }
        public Animator Animator { get; private set; }

        public ShortSwordColliderActivator[] ShortSwordColliderActivators { get; private set; } 
        public DefenseColliderActivator DefenseColliderActivator { get; private set; }

        public override Health Health { get; protected set; }

        private CameraShaker _cameraShaker;

        protected override void Awake()
        {
            base.Awake();

            InputAction = GetComponent<PlayerInput>();
            Rigidbody = GetComponent<Rigidbody>();
            Animator = GetComponent<Animator>();
            
            ColliderUtility.Init(gameObject);
            ColliderUtility.CalculateCapsuleColliderDimensions();
            CameraUtility.Init();
            PlayerAnimationData.Init();

            MainCamera = UnityEngine.Camera.main.transform;

            DefenseColliderActivator = GetComponentInChildren<DefenseColliderActivator>();
            ShortSwordColliderActivators = GetComponentsInChildren<ShortSwordColliderActivator>();

            _cameraShaker = new CameraShaker(PlayerData);
            ReusableData = new PlayerStateReusableData();
            Health = new Health(ReusableData);
            StateMachine = new PlayerStateMachine(this, gameObject);
        }

        private void Start()
        {
            StateMachine.ChangeState(StateMachine.StartState());
        }

        private void OnEnable()
        {
            SwordColliderActivator.OnTargetHit += _cameraShaker.ShakeCameraOnAttackHit;
            Health.OnDamageTaken += _cameraShaker.ShakeCameraOnDamageTaken;
        }

        private void OnValidate()
        {
            ColliderUtility.Init(gameObject);
            ColliderUtility.CalculateCapsuleColliderDimensions();
        }

        private void Update()
        {
            StateMachine.HandleInput();
            StateMachine.Update();
        }

        private void FixedUpdate()
        {
            StateMachine.FixedUpdate();
        }

        private void OnDisable()
        {
            SwordColliderActivator.OnTargetHit -= _cameraShaker.ShakeCameraOnAttackHit;
            Health.OnDamageTaken -= _cameraShaker.ShakeCameraOnAttackHit;
        }
    }
}