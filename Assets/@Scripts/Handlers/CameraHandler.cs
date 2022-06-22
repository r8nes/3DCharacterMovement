using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ActionCatGame.Handler
{
    public class CameraHandler : MonoBehaviour
    {
        private float _lookAngles;
        private float _pivotAngles;
        private float _defaultPosition;
        private float _targetPosition;
        
        private LayerMask _ignoreLayers;
        private Transform _refTransform;
        private Vector3 _cameraFollowVelocity = Vector3.zero;
        
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
        [Header("Camera Collision")]
        [SerializeField] private float _cameraSphereRadius = 0.2f;
        [SerializeField] private float _cameraCollisionOffset = 0.2f;
        [SerializeField] private float _minCollisionOffset = 0.2f;

        private Vector3 _cameraTransformPosition;


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
            Vector3 targetPosition = Vector3.SmoothDamp
                (_refTransform.position, _targetTransform.position, ref _cameraFollowVelocity, delta / _followSpeed);
            _refTransform.position = targetPosition;

            HandleCameraCollision(delta);
        }

        public void HandleCameraRotation(float delta, float mouseXInput, float mouseYInput) 
        {
            _lookAngles += (mouseXInput * _lookSpeed) / delta;
            _pivotAngles -= (mouseYInput * _pivotSpeed) / delta;
            _pivotAngles = Mathf.Clamp(_pivotAngles, _minimumPivot, _maximumPivot);

            Vector3 rotation = Vector3.zero;
            rotation.y = _lookAngles;

            //smooth rotation of camera

            Quaternion targetRotation = Quaternion.Euler(rotation);
            //_refTransform.rotation = targetRotation;
            _refTransform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _lookSpeed * Time.deltaTime);

            rotation = Vector3.zero;
            rotation.x = _pivotAngles;

            targetRotation = Quaternion.Euler(rotation);
            //_cameraPivotTransform.localRotation = targetRotation;
            _cameraPivotTransform.localRotation = Quaternion.Slerp(_cameraPivotTransform.localRotation, targetRotation, _lookSpeed * Time.deltaTime);
        }


        // think about camera collision when the player goes to front of wall
        private void HandleCameraCollision(float delta) 
        {
            _targetPosition = _defaultPosition;
            RaycastHit hit;
            Vector3 direction = _cameraTransform.position - _cameraPivotTransform.position;
            direction.Normalize();

            if (Physics.SphereCast(_cameraPivotTransform.position, _cameraSphereRadius, direction, out hit, Mathf.Abs(_targetPosition), _ignoreLayers))
            {
                float dis = Vector3.Distance(_cameraPivotTransform.position, hit.point);
                _targetPosition = -(dis - _cameraCollisionOffset);
            }

            if (Mathf.Abs(_targetPosition) < _minCollisionOffset)
            {
                _targetPosition = -_minCollisionOffset;
            }

            _cameraTransformPosition.z = Mathf.Lerp(_cameraTransform.localPosition.z, _targetPosition, delta / 0.2f);
            _cameraTransform.localPosition = _cameraTransformPosition;
        }
    }
}
