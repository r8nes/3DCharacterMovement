using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ActionCatGame.Handler
{
    public class CameraHandler : MonoBehaviour
    {
        private float _defaultPosition;
        private float _lookAngles;
        private float _pivotAngles;
        
        private LayerMask _ignoreLayers;
        private Transform _refTransform;
        
        [SerializeField] private float _lookSpeed = 0.1f;
        [SerializeField] private float _followSpeed = 0.1f;


        [Space(10)]
        [Header("Pivot")]
        [SerializeField] private float _pivotSpeed = 0.03f;
        [SerializeField] private float _minimumPivot = -35;
        [SerializeField] private float _maximumPivot = 35;
        [Space(10)]

        [SerializeField] private Transform _targetTransform;
        [SerializeField] private Transform _cameraTransform;
        [SerializeField] private Transform _cameraPivotTransform;
        [Space(10)]

        [Header("Is Cursor Visible")]
        [SerializeField] private bool _isCursorVisible = true;

        [Space(10)]
        [SerializeField] private Vector3 _cameraTransformPosition;


        public static CameraHandler Instance;

        private void Awake()
        {
            if (Instance == null)
                Instance = this;

            _refTransform = transform;
            _defaultPosition = _cameraTransform.localPosition.z;
            _ignoreLayers = ~(1 << 8 | 1 << 9 | 1 << 10);

            Cursor.visible = _isCursorVisible;
        }

        public void FollowTarget(float delta)
        {
            Vector3 targetPosition = Vector3.Lerp(_refTransform.position, _targetTransform.position, delta / _followSpeed);
            _refTransform.position = targetPosition;
        }

        public void HandleCameraRotation(float delta, float mouseXInput, float mouseYInput) 
        {
            _lookAngles += (mouseXInput * _lookSpeed) / delta;
            _pivotAngles -= (mouseYInput * _pivotSpeed) / delta;
            _pivotAngles = Mathf.Clamp(_pivotAngles, _minimumPivot, _maximumPivot);

            Vector3 rotation = Vector3.zero;
            rotation.y = _lookAngles;

            Quaternion targetRotation = Quaternion.Euler(rotation);
            _refTransform.rotation = targetRotation;

            rotation = Vector3.zero;
            rotation.x = _pivotAngles;

            targetRotation = Quaternion.Euler(rotation);
            _cameraPivotTransform.localRotation = targetRotation;
        }
    }
}
