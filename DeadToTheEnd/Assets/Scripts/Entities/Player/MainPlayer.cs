using System;
using CameraManage;
using Combat.ColliderActivators;
using Combat.SwordActivators;
using Data.Animations;
using Data.Camera;
using Data.Combat;
using Data.Layers;
using Data.ScriptableObjects;
using Data.Shop;
using Data.States;
using Data.Stats;
using Entities.Core;
using InventorySystem;
using LootSystem;
using StateMachine;
using UnityEngine;
using Utilities;
using Utilities.Camera;
using Utilities.Collider;
using Utilities.Raycast;

namespace Entities
{
    [RequireComponent(typeof(PlayerInput), typeof(ItemEquipper))]
    public sealed class MainPlayer : AliveEntity
    {
        [field:SerializeField] public SwordActivator LongSwordActivator { get; private set; }
        [field:SerializeField] public ShortSwordActivator[] ShortSwordsActivator { get; private set; }
        [field: SerializeField] public PlayerColliderUtility ColliderUtility { get; private set; }
        [field: SerializeField] public PlayerLayerData PlayerLayerData { get; private set; }
        [field: SerializeField] public PlayerCameraUtility CameraUtility { get; private set; }
        [field: SerializeField] public PlayerAnimationData PlayerAnimationData { get; private set; }
        [field: SerializeField] public Gold Gold { get; private set; }

        [field: SerializeField] private float _distanceToPickObject;
        public Transform MainCamera { get; private set; }
        public PlayerInput InputAction { get; private set; }
        public ItemEquipper ItemEquipper { get; private set; }
        
        public PlayerStateReusableData PlayerStateReusable { get; private set; }
        public ShortAttackColliderActivator[] ShortSwordColliderActivators { get; private set; } 
        public DefenseColliderActivator DefenseColliderActivator { get; private set; }
        
        protected override void Awake()
        {
            base.Awake();

            InputAction = GetComponent<PlayerInput>();
            
            ColliderUtility.Init(gameObject);
            ColliderUtility.CalculateCapsuleColliderDimensions();
            CameraUtility.Init();
            PlayerAnimationData.Init();

            MainCamera = UnityEngine.Camera.main.transform;

            DefenseColliderActivator = GetComponentInChildren<DefenseColliderActivator>();
            ShortSwordColliderActivators = GetComponentsInChildren<ShortAttackColliderActivator>();
            ItemEquipper = GetComponent<ItemEquipper>();

            Reusable = new PlayerStateReusableData();
            PlayerStateReusable = (PlayerStateReusableData) Reusable;
            AttackCalculator = new AttackCalculator(PlayerStateReusable);
            StateMachine = new PlayerStateMachine(this);
        }

        private void Start()
        {
            StateMachine.ChangeState(StateMachine.StartState());
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

        public void ActivateRightSword(float time, AttackType attackType)
        {
            var attackData = CreateAttackData(attackType);
            
            foreach (var shortSwordColliderActivator in ShortSwordColliderActivators)
            {
                if (!shortSwordColliderActivator.RightSword) continue;
                
                shortSwordColliderActivator.enabled = true;
                shortSwordColliderActivator.ActivateCollider(time, attackData);
            }
        }
        public void ActivateLeftSword(float time, AttackType attackType)
        {
            var attackData = CreateAttackData(attackType);
            
            foreach (var shortSwordColliderActivator in ShortSwordColliderActivators)
            {
                if (shortSwordColliderActivator.RightSword) continue;
                
                shortSwordColliderActivator.enabled = true;
                shortSwordColliderActivator.ActivateCollider(time, attackData);
            }
        }

    }
}