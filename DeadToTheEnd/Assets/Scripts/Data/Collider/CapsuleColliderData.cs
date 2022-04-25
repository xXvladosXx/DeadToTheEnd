﻿using UnityEngine;

namespace Data.Collider
{
    public class CapsuleColliderData
    {
        public CapsuleCollider Collider { get; private set; }
        public Vector3 ColliderCenterInLocalSpace { get; private set; }

        public void Initialize(GameObject gameObject)
        {
            if(Collider != null) 
                return;

            Collider = gameObject.GetComponent<CapsuleCollider>();

            UpdateColliderCentre();
        }

        private void UpdateColliderCentre()
        {
            ColliderCenterInLocalSpace = Collider.center;
        }
    }
}