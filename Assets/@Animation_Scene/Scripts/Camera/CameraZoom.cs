using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

namespace ActionCatGame
{
    public class CameraZoom : MonoBehaviour
    {
        [Range(0, 10f)] [SerializeField] private float _defaultDistance = 6f;
        [Range(0, 10f)] [SerializeField] private float _minDistance = 6f;
        [Range(0, 10f)] [SerializeField] private float _maxDistance = 6f;
        [Range(0, 10f)] [SerializeField] private float _sensetivity = 1f;
        [Range(0, 10f)] [SerializeField] private float _smothing = 4f;

        private CinemachineFramingTransposer _frameTransposer;
        private CinemachineInputProvider _inputProvider;

        private float _currentTargetDistance;

        private void Awake()
        {
            _frameTransposer = GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineFramingTransposer>();
            _inputProvider = GetComponent<CinemachineInputProvider>();

            _currentTargetDistance = _defaultDistance;
        }

        private void Update()
        {
            Zoom();
        }

        private void Zoom()
        {
            float zoomValue = _inputProvider.GetAxisValue(2) * _sensetivity;
            _currentTargetDistance = Mathf.Clamp(_currentTargetDistance + zoomValue, _minDistance, +_maxDistance);
            float currentDistance = _frameTransposer.m_CameraDistance;

            if (currentDistance == _currentTargetDistance) return;

            float lerpedZoomValue = Mathf.Lerp(currentDistance, _currentTargetDistance, _smothing * Time.deltaTime);
            _frameTransposer.m_CameraDistance = lerpedZoomValue;
        }
    }
}
