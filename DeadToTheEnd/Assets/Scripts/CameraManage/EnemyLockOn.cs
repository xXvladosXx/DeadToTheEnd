using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Entities;
using UnityEngine;

public class EnemyLockOn : MonoBehaviour
{
   
    [SerializeField] private LayerMask targetLayers;
    [SerializeField] private Transform enemyTarget_Locator;
    [SerializeField] private CinemachineVirtualCamera _cinemachineVirtualCamera;

    [SerializeField] private Animator cinemachineAnimator;

    [SerializeField] private bool zeroVert_Look;
    [SerializeField] private float noticeZone = 10;
    [SerializeField] private float lookAtSmoothing = 2;
    [SerializeField] private float maxNoticeAngle = 60;
    [SerializeField] private float crossHair_Scale = 0.1f;
    
    [SerializeField] private CameraCursorHider camCursorHider;
    [SerializeField] private Transform lockOnCanvas;
    [SerializeField] private Transform enemy;

    private Transform _currentTarget;
    private Animator _anim;

    private Transform _cam;
    private bool _enemyLocked;
    private float _currentYOffset;
    private Vector3 _pos;
    private MainPlayer _mainPlayer;
    void Start()
    {
        _anim = GetComponent<Animator>();
        _cam = UnityEngine.Camera.main.transform;
        lockOnCanvas.gameObject.SetActive(false);
        _mainPlayer = GetComponent<MainPlayer>();
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
        
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (_mainPlayer.ReusableData.IsTargetLocked)
            {
                ResetTarget();
                _mainPlayer.ReusableData.IsTargetLocked = false;
                _mainPlayer.Animator.SetBool(_mainPlayer.PlayerAnimationData.LockedParameterHash, false);
                return;
            }
            
            if (_currentTarget)
            {
                ResetTarget();
                return;
            }

            if (_currentTarget != ScanNearBy())
            {
                FoundTarget();
                _mainPlayer.ReusableData.IsTargetLocked = true;
                _mainPlayer.ReusableData.Target = enemy;
                _mainPlayer.Animator.SetBool(_mainPlayer.PlayerAnimationData.LockedParameterHash, true);
            }
        }

    }


    void FoundTarget(){
        lockOnCanvas.gameObject.SetActive(true);
        _anim.SetLayerWeight(1, 1);
        cinemachineAnimator.Play("LockOn");
        _enemyLocked = true;
    }

    void ResetTarget()
    {
        _currentTarget = null;
        _enemyLocked = false;
        _anim.SetLayerWeight(1, 0);
        cinemachineAnimator.Play("PlayerCamera");
    }


    private Transform ScanNearBy()
    {
        Collider[] nearbyTargets = Physics.OverlapSphere(transform.position, noticeZone, targetLayers);
        float closestAngle = maxNoticeAngle;
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
        if(zeroVert_Look && _currentYOffset > 1.6f && _currentYOffset < 1.6f * 3) _currentYOffset = 1.6f;
        Vector3 tarPos = closestTarget.position + new Vector3(0, _currentYOffset, 0);
        //if(Blocked(tarPos)) return null;
        return closestTarget;
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
        if(dis/2 > noticeZone) return false; else return true;
    }


    private void LookAtTarget()
    {
        if(_currentTarget == null) {
            ResetTarget();
            return;
        }
        _pos = _currentTarget.position + new Vector3(0, _currentYOffset, 0);
        lockOnCanvas.position = _pos;
        lockOnCanvas.localScale = Vector3.one * ((_cam.position - _pos).magnitude * crossHair_Scale);

        enemyTarget_Locator.position = _pos;
        Vector3 dir = _currentTarget.position - transform.position;
        dir.y = 0;
        Quaternion rot = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.Lerp(transform.rotation, rot, Time.deltaTime * lookAtSmoothing);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, noticeZone);   
    }
}
