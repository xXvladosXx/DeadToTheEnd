using Data.Animations;
using Data.Layers;
using Data.ScriptableObjects;
using Data.States;
using UnityEngine;
using Utilities;
using Utilities.Camera;
using Utilities.Collider;

namespace Entities
{
    [RequireComponent(typeof(PlayerInput), typeof(Rigidbody))]
    public sealed class MainPlayer : AliveEntity
    {
        [field: SerializeField] public PlayerData PlayerData { get; private set; }
        [field: SerializeField] public PlayerColliderUtility ColliderUtility { get; private set; }
        [field: SerializeField] public PlayerLayerData PlayerLayerData { get; private set; }
        [field: SerializeField] public PlayerCameraUtility CameraUtility { get; private set; }
        [field: SerializeField] public AnimationData AnimationData { get; private set; }
        [field: SerializeField] public PlayerStateReusableData ReusableData { get; set; }
        public Transform MainCamera { get; private set; }
        public PlayerInput InputAction { get; private set; }
        public Rigidbody Rigidbody { get; private set; }
        public Animator Animator { get; private set; }

        private void Awake()
        {
            InputAction = GetComponent<PlayerInput>();
            Rigidbody = GetComponent<Rigidbody>();
            Animator = GetComponent<Animator>();
        
            ColliderUtility.Init(gameObject);
            ColliderUtility.CalculateCapsuleColliderDimensions();
            CameraUtility.Init();
            AnimationData.Init();

            MainCamera = UnityEngine.Camera.main.transform;
            ReusableData = new PlayerStateReusableData();
        }

        private void OnValidate()
        {
            ColliderUtility.Init(gameObject);
            ColliderUtility.CalculateCapsuleColliderDimensions();
        }

        private void Update()
        {
            NormalizeMovement();
        }

        private void FixedUpdate()
        {
            Float();
        }
    
        private void NormalizeMovement()
        {
            if (Rigidbody.velocity.magnitude > ReusableData.MovementSpeedModifier)
            {
                Rigidbody.velocity = Rigidbody.velocity.normalized *
                                     ReusableData.MovementSpeedModifier;
            }
        }
        private void Float()
        {
            Vector3 capsuleColliderCenterInWorld = ColliderUtility
                .CapsuleColliderData.Collider.bounds.center;

            Ray downwardsRayFromCapsuleCenter = new Ray(capsuleColliderCenterInWorld, Vector3.down);
            if (Physics.Raycast(downwardsRayFromCapsuleCenter, out RaycastHit raycastHit, ColliderUtility.SlopeData.FloatRayDistance, 
                    PlayerLayerData.GroundLayer))
            {
                float groundAngle = Vector3.Angle(raycastHit.normal, -downwardsRayFromCapsuleCenter.direction);
                float slopeSpeedModifier = SetSlopeSpeedModifierOnAngle(groundAngle);
                
                if(slopeSpeedModifier == 0f)
                    return;
                
                float distanceToFloatingPoint = ColliderUtility
                    .CapsuleColliderData.ColliderCenterInLocalSpace.y * transform.localScale.y - raycastHit.distance;
                
                if(distanceToFloatingPoint == 0f)
                    return;

                float amountToLift = distanceToFloatingPoint * ColliderUtility.SlopeData.StepReachForce - GetPlayerVerticalVelocity().y;
                var liftForce = new Vector3(0f, amountToLift, 0f);
                Rigidbody.AddForce(liftForce, ForceMode.VelocityChange);
            }
        }
        private Vector3 GetPlayerVerticalVelocity()
        {
            return new Vector3(0f, Rigidbody.velocity.y, 0f);
        }
        private float SetSlopeSpeedModifierOnAngle(float angle)
        {
            float slopeSpeedModifier = PlayerData.GroundData.SlopeSpeedAngles.Evaluate(angle);

            if (ReusableData.MovementSlopeSpeedModifier != slopeSpeedModifier)
            {
                ReusableData.MovementSlopeSpeedModifier = slopeSpeedModifier;
            }

            return slopeSpeedModifier;
        }

        private void OnTriggerEnter(Collider other)
        {
        }

        private void OnTriggerExit(Collider other)
        {
        }
    }
}