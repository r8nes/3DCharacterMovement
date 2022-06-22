using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ActionCatGame.Handler;

namespace ActionCatGame.Movement
{
    public class PlayerLocomotion : MonoBehaviour
    {
        private Transform _cameraObject;
        private InputHandler _inputHandler;
        private Vector3 _moveDirection;

        [HideInInspector]
        public Transform _characterTransform;
        [HideInInspector]
        public AnimatorHandler _animatorHandler;

        //maybe add 'new'?
        private Rigidbody _rigidBody;
        private Camera _normalCamera;

        [Header("Stats")]
        [SerializeField] private float _movementSpeed = 5f;
        [SerializeField] private float _rotationSpeed = 10f;

        #region MOVEMENT

        private Vector3 _normalVector;
        private Vector3 _targetPosition;

        #endregion

        private void Start()
        {
            _rigidBody = GetComponent<Rigidbody>();
            _inputHandler = GetComponent<InputHandler>();
            _animatorHandler = GetComponentInChildren<AnimatorHandler>();

            _cameraObject = Camera.main.transform;
            _characterTransform = transform;

            _animatorHandler.Init();
        }

        private void Update()
        {
            float delta = Time.deltaTime;

            _inputHandler.TickInput(delta);

            _moveDirection = _cameraObject.forward * _inputHandler.Vertical;
            _moveDirection += _cameraObject.right * _inputHandler.Horizontal;
            _moveDirection.Normalize();
            _moveDirection.y = 0;

            float speed = _movementSpeed;
            _moveDirection *= speed;

            Vector3 projectedVelocity = Vector3.ProjectOnPlane(_moveDirection, _normalVector);
            _rigidBody.velocity = projectedVelocity;

            _animatorHandler.UpdateAnimatorValues(_inputHandler.MoveAmoint, 0);

            if (_animatorHandler.CanRotation)
            {
                HandleRotation(delta);
            }
        }

        private void HandleRotation(float delta)
        {
            Vector3 targetDir = Vector3.zero;
            float moveOverride = _inputHandler.MoveAmoint;

            targetDir = _cameraObject.forward * _inputHandler.Vertical;
            targetDir += _cameraObject.right * _inputHandler.Horizontal;

            targetDir.Normalize();
            targetDir.y = 0;

            if (targetDir == Vector3.zero)
                targetDir = _characterTransform.forward;

            float rotSpeed = _rotationSpeed;

            Quaternion tr = Quaternion.LookRotation(targetDir);
            Quaternion targetRotation = Quaternion.Slerp(_characterTransform.rotation, tr, rotSpeed * delta);

            _characterTransform.rotation = targetRotation;
        }
    }
}
