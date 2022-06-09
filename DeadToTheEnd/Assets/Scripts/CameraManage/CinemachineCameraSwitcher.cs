using System;
using Cinemachine;
using UnityEngine;

namespace CameraManage
{
    public class CinemachineCameraSwitcher : MonoBehaviour
    {
        [SerializeField] private CinemachineShake[] _cinemachineShakes;

        private CinemachineShake _currentCinemachineShake;
        public static CinemachineCameraSwitcher Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
            _currentCinemachineShake = _cinemachineShakes[0];
        }

        public void ChangeCamera()
        {
            if (_currentCinemachineShake == _cinemachineShakes[0])
            {
                _currentCinemachineShake = _cinemachineShakes[1];
            }
            else
            {
                _currentCinemachineShake = _cinemachineShakes[0];
            }
        }

        public void ShakeCamera(float intensity, float time)
        {
            _currentCinemachineShake.ShakeCamera(intensity, time);
        }
    }
}