using Cinemachine;
using UnityEngine;

namespace CameraManage
{
    [RequireComponent(typeof(CinemachineVirtualCamera))]
    public sealed class CameraZoom : MonoBehaviour
    {
        [Range(0f,10f)]
        [SerializeField] private float _defaultDistance = 6f;
        [Range(0f,10f)]
        [SerializeField] private float _minimumDistance = 1f;
        [Range(0f,10f)]
        [SerializeField] private float _maximumDistance = 7f;
        
        [Range(0f,10f)]
        [SerializeField] private float _smoothing = 4f;
        [Range(0f,10f)]
        [SerializeField] private float _zoomSensitivity = 1f;

        private CinemachineFramingTransposer _cinemachineFramingTransposer;
        private CinemachineInputProvider _inputProvider;

        private float _currentTargetDistance;
        private void Awake()
        {
            _cinemachineFramingTransposer = GetComponent<CinemachineVirtualCamera>()
                .GetCinemachineComponent<CinemachineFramingTransposer>();

            _inputProvider = GetComponent<CinemachineInputProvider>();
            _currentTargetDistance = _defaultDistance;
        }

        private void Update()
        {
            Zoom();
        }

        private void Zoom()
        {
            float zoomValue = _inputProvider.GetAxisValue(2) * _zoomSensitivity;

            _currentTargetDistance = Mathf.Clamp(_currentTargetDistance + zoomValue, _minimumDistance, _maximumDistance);

            float currentDistance = _cinemachineFramingTransposer.m_CameraDistance;
            if(currentDistance == _currentTargetDistance)
                return;

            float lerpedZoomValue = Mathf.Lerp(currentDistance, _currentTargetDistance, _smoothing * Time.deltaTime);
            _cinemachineFramingTransposer.m_CameraDistance = lerpedZoomValue;
        }
    }
}
