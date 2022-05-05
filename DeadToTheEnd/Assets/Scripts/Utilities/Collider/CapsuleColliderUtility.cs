using System;
using Data.Collider;
using UnityEngine;

namespace Utilities
{
    [Serializable]
    public class CapsuleColliderUtility 
    {
        public CapsuleColliderData CapsuleColliderData { get; private set; }
        [field: SerializeField] public DefaultColliderData DefaultColliderData { get; private set; }
        [field: SerializeField] public SlopeData SlopeData { get; private set; }

        public void Init(GameObject gameObject)
        {
            if(CapsuleColliderData!= null)
                return;
            
            CapsuleColliderData = new CapsuleColliderData();
            CapsuleColliderData.Initialize(gameObject);
            OnInit();
        }

        protected virtual void OnInit()
        {
            
        }
        
        public void CalculateCapsuleColliderDimensions()
        {
            SetCapsuleColliderRadius(DefaultColliderData.Radius);
            SetCapsuleColliderHeight(DefaultColliderData.Height * (1f - SlopeData.StepHeightPercentage));
            RecalculateCapsuleColliderCenter();

            var halfColliderHeight = CapsuleColliderData.Collider.height / 2f;
            if (halfColliderHeight < CapsuleColliderData.Collider.radius)
            {
                SetCapsuleColliderRadius(halfColliderHeight);
            }

            CapsuleColliderData.UpdateColliderCentre();
        }

        private void RecalculateCapsuleColliderCenter()
        {
            float colliderHeightDifference = DefaultColliderData.Height - CapsuleColliderData.Collider.height;
            Vector3 newColliderCenter =
                new Vector3(0f, DefaultColliderData.CenterY + (colliderHeightDifference / 2f), 0f);

            CapsuleColliderData.Collider.center = newColliderCenter;
        }

        private void SetCapsuleColliderHeight(float height)
        {
            CapsuleColliderData.Collider.height = height;
        }

        private void SetCapsuleColliderRadius(float radius)
        {
            CapsuleColliderData.Collider.radius = radius;
        }
    }
}
