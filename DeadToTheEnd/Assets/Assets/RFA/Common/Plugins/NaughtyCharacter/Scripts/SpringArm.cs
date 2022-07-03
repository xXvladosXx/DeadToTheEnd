using UnityEngine;

namespace BLINK.Controller
{
    public class SpringArm : MonoBehaviour
    {
        public float TargetLength = 3.0f;
        public float playerYOffset = 1.75f;
        public float collisionDistance = 1;
        public float distanceBehindCamera = 1.5f;
        public float SpeedDamp = 0.0f;
        public Transform CollisionSocket;
        public LayerMask CollisionMask = 0;
        public Camera Camera;

        private Vector3 _socketVelocity;

        private void LateUpdate()
        {
            
        }
    }
}