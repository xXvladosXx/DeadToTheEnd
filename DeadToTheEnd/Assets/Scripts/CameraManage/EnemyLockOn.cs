using System;
using Cinemachine;
using Entities;
using Entities.Core;
using Entities.Enemies;
using UnityEngine;

namespace CameraManage
{
    [RequireComponent(typeof(MainPlayer))]
    public class EnemyLockOn : MonoBehaviour
    {
        [SerializeField] private LayerMask targetLayers;
        [SerializeField] private Animator _cinemachineAnimator;
        [SerializeField] private bool _zeroVertLook;
        [SerializeField] private float _noticeZone = 10;
        [SerializeField] private float _maxNoticeAngle = 60;

        [SerializeField] private CinemachineVirtualCamera _cinemachineVirtualCamera;

        private Enemy _currentTarget;
        private Animator _anim;

        private Transform _cam;
        private float _currentYOffset;
        private Vector3 _pos;
        private MainPlayer _mainPlayer;
        public event Action<Enemy> OnPlayerLocked;
        public event Action OnPlayerUnlocked;

        private void Awake()
        {
            _anim = GetComponent<Animator>();
            _mainPlayer = GetComponent<MainPlayer>();
        }

        void Start()
        {
            _cam = UnityEngine.Camera.main.transform;
        }

        void Update()
        {
            var modifier = _mainPlayer.transform.eulerAngles.y;
        
            /* if (MainPlayer.ReusableData.IsLocked)
        {
            var componentBase = _cinemachineVirtualCamera.GetCinemachineComponent<CinemachinePOV>();
           
            if (modifier > 0)
            {
                componentBase.m_HorizontalAxis.Value = modifier;
            }
            else
            {
                componentBase.m_HorizontalAxis.Value = 360 - modifier;
            }
            //componentBase.m_HorizontalAxis.Value = 360 -  
        }*/
        }

        public bool FindTarget()
        {
            if (ScanNearBy() != null)
            {
                FoundTarget();
                return true;
            }

            return false;
        }


        private void FoundTarget()
        {
            _currentTarget = ScanNearBy() as Enemy;
            _cinemachineVirtualCamera.LookAt = _currentTarget.transform;
            OnPlayerLocked?.Invoke(_currentTarget);
            _cinemachineAnimator.Play("LockOn");
        }

        public void ResetTarget()
        {
            _currentTarget = null;
            OnPlayerUnlocked?.Invoke();
            _cinemachineAnimator.Play("PlayerCamera");
        }


        public AliveEntity ScanNearBy()
        {
            Collider[] nearbyTargets = Physics.OverlapSphere(transform.position, _noticeZone, targetLayers);
            float closestAngle = _maxNoticeAngle;
            Transform closestTarget = null;
            if (nearbyTargets.Length <= 0) return null;

            for (int i = 0; i < nearbyTargets.Length; i++)
            {
                Vector3 dir = nearbyTargets[i].transform.position - _cam.position;
                dir.y = 0;
                float _angle = Vector3.Angle(_cam.forward, dir);
            
                if (_angle < closestAngle)
                {
                    closestTarget = nearbyTargets[i].transform;
                    closestAngle = _angle;      
                }
            }

            if (!closestTarget ) return null;
            float h1 = closestTarget.GetComponent<CapsuleCollider>().height;
            float h2 = closestTarget.localScale.y;
            float h = h1 * h2;
            float half_h = (h / 2) / 2;
            _currentYOffset = h - half_h;
            if(_zeroVertLook && _currentYOffset > 1.6f && _currentYOffset < 1.6f * 3) _currentYOffset = 1.6f;
            Vector3 tarPos = closestTarget.position + new Vector3(0, _currentYOffset, 0);
            if(Blocked(tarPos)) return null;
            
            return closestTarget.GetComponent<AliveEntity>();
        }

        bool Blocked(Vector3 t){
            RaycastHit hit;
            if(Physics.Linecast(transform.position + Vector3.up * 0.5f, t, out hit)){
                if(!hit.transform.CompareTag("Enemy")) return true;
            }
            return false;
        }

        bool TargetOnRange(){
            float dis = (transform.position - _pos).magnitude;
            if(dis/2 > _noticeZone) return false; else return true;
        }
    }
}
