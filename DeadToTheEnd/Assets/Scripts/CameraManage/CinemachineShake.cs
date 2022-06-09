using System;
using Cinemachine;
using UnityEngine;

namespace CameraManage
{
    [RequireComponent(typeof(CinemachineVirtualCamera))]
    public class CinemachineShake : MonoBehaviour
    {
        
        private CinemachineVirtualCamera _cinemachineVirtualCamera;
        private CinemachineBasicMultiChannelPerlin _cinemachineBasicMultiChannelPerlin;
        
        private float _shakeTime;
        private float _shakeTimeTotal;
        private float _startingIntensity;
        private void Awake()
        {
            _cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
            _cinemachineBasicMultiChannelPerlin = _cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        }
        private void Update()
        {
            if (_shakeTime > 0)
            {
                _shakeTime -= Time.deltaTime;
                if (_shakeTime <= 0f)
                {
                    _cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 
                    Mathf.Lerp(_startingIntensity, 0, 1- (_shakeTime / _shakeTimeTotal));
                }
            }
        }
        
        public void ShakeCamera(float intensity, float time)
        {
            _cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = intensity;

            _shakeTime = time;
            _startingIntensity = intensity;
            _shakeTimeTotal = time;
        }
    }
}