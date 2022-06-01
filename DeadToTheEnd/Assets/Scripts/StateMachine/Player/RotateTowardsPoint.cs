using System;
using UnityEngine;

namespace StateMachine.Player
{
    public class RotateTowardsPoint : MonoBehaviour
    {
        [SerializeField] private LayerMask _aimLayerMask;
        [SerializeField] private GameObject _target;
        
        public float Speed = 2; 
        private void Update()
        {
            Vector3 mouseWorldPosition = Vector3.zero;
            Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
            Ray ray = Camera.main.ScreenPointToRay(screenCenterPoint);
            if (Physics.Raycast(ray, out RaycastHit raycastHit, Mathf.Infinity, _aimLayerMask))
            {
                _target.transform.position = raycastHit.point;
                mouseWorldPosition = raycastHit.point;
            }

            if (Input.GetKey(KeyCode.C))
            {
                Vector3 worldAimTarget = mouseWorldPosition;
                worldAimTarget.y = transform.position.y;
                Vector3 aimDirection = (worldAimTarget - transform.position).normalized;

                transform.forward = Vector3.Lerp(transform.forward, aimDirection, Time.deltaTime * 20f);
            }
        }
    }
}