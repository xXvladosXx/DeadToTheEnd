using System;
using UnityEngine;

namespace Data.Collider
{
    [Serializable]
    public class PlayerTriggerColliderData
    {
        [field: SerializeField] public BoxCollider GroundCheckCollider { get; private set; }
        public Vector3 GroundCheckColliderExtents { get; private set; }

        public void Initialize()
        {
            GroundCheckColliderExtents = GroundCheckCollider.bounds.center;
        }
    }
}