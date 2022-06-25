using UnityEngine;

namespace Utilities.Raycast
{
    public class RaycastChecker
    {
        public static Transform CheckRaycast(Vector3 startPosition, Vector3 direction,
            float distance, int layer)
        {
            Transform target = null;

            RaycastHit raycastHit;
                
            if (Physics.Raycast(startPosition, direction, out raycastHit,
                    distance, layer))
            {
                target = raycastHit.collider.gameObject.transform;
            }

            return target;
        }
        
        public static Transform CheckRaycast(Vector3 startPosition, Vector3 direction,
            float distance)
        {
            Transform target = null;

            RaycastHit raycastHit;
                
            if (Physics.Raycast(startPosition, direction, out raycastHit,
                    distance))
            {
                target = raycastHit.collider.gameObject.transform;
            }

            Debug.Log(target);
            return target;
        }

        public static Transform CheckRaycastExcept(Vector3 startPosition, Vector3 direction,
            float distance, int notInLayer)
        {
            Transform target = null;

            RaycastHit raycastHit;

            if (Physics.Raycast(startPosition, direction, out raycastHit,
                    distance, ~notInLayer))
            {
                target = raycastHit.collider.gameObject.transform;
            }

            return target;
        }
    }
}