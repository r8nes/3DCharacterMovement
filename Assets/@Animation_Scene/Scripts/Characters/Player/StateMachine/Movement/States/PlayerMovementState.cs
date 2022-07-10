using System;
using System.Collections;
using System.Collections.Generic;
using ActionCatGame.Core.State;
using UnityEngine;

namespace ActionCatGame.Core.State
{
    public class PlayerMovementState : IState
    {
        protected float _baseSpeed = 5f;
        protected float _speedModif = 1f;

        protected Vector3 _currentTargetRotation;
        protected Vector3 _timeToReachTargetRotation;
        protected Vector3 _dampedTargetRotationPassedTime;
        protected Vector3 _dampedTargetRotationCurrentVelocity;

        protected Vector2 _movementInput;
        protected PlayerMovementStateMachine _stateMachine;
        public PlayerMovementState(PlayerMovementStateMachine playerMovementStateMachine)
        {
            _stateMachine = playerMovementStateMachine;

            InitData();
        }

        private void InitData()
        {
            _timeToReachTargetRotation.y = 0.14f;
        }

        #region IState Methods
        public void Enter()
        {
            Debug.Log("State:" + GetType().Name);
        }

        public void Exit()
        {
        }

        public void HandleInput()
        {
            ReadMovementInput();
        }

        public void Update()
        {
        }

        public void PhysicsUpdate()
        {
            Move();
        }

        #endregion

        #region Main Method

        private void Move()
        {
            if (_movementInput == Vector2.zero || _speedModif == 0f) return;

            Vector3 movementDir = GetMovementDirection();

            float targetRotYAngle = Rotate(movementDir);

            Vector3 targetRotationDirection = GetTargetRotationDirection(targetRotYAngle);

            float movementSpeed = GetMovementSpeed();

            Vector3 currentPlayerHorizontalVelocity = GetPlayerHorizontalVelocity();

            _stateMachine.Player.Rigidbody.AddForce(targetRotationDirection * movementSpeed - currentPlayerHorizontalVelocity, ForceMode.VelocityChange);
        }

        private float Rotate(Vector3 direction)
        {
            float directionAngle = UpdateTargetRotation(direction);

            RotateTowardsTargetRotation();

            return directionAngle;
        }

        private void UpdateTargetRotationData(float targetAngle)
        {
            _currentTargetRotation.y = targetAngle;

            _dampedTargetRotationPassedTime.y = 0f;
        }

        private float AddCameraRotToAngle(float angle)
        {
            angle += _stateMachine.Player.MainCameraTransform.eulerAngles.y;

            if (angle > 360f)
                angle -= 360f;

            return angle;
        }

        private float GetDirAngle(Vector3 direction)
        {
            float directionAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;

            if (directionAngle < 0f)
                directionAngle += 360f;
            return directionAngle;
        }

        private void ReadMovementInput()
        {
            _movementInput = _stateMachine.Player.Input.PlayerActions.Move.ReadValue<Vector2>();
        }

        #endregion

        #region Reusable Methods

        protected float UpdateTargetRotation(Vector3 direction, bool shouldConsiderCameraRotation = true)
        {
            float directionAngle = GetDirAngle(direction);

            if (shouldConsiderCameraRotation)
            {
                directionAngle = AddCameraRotToAngle(directionAngle);
            }

            if (directionAngle != _currentTargetRotation.y)
            {
                UpdateTargetRotationData(directionAngle);
            }

            return directionAngle;
        }


        protected void RotateTowardsTargetRotation()
        {
            float currentYAngle = _stateMachine.Player.Rigidbody.rotation.eulerAngles.y;

            if (currentYAngle == _currentTargetRotation.y) return;

            float smoothedYAngled = Mathf.SmoothDampAngle(currentYAngle, _currentTargetRotation.y, ref _dampedTargetRotationCurrentVelocity.y, _timeToReachTargetRotation.y - _dampedTargetRotationPassedTime.y);
            _dampedTargetRotationPassedTime.y += Time.deltaTime;

            Quaternion targetRot = Quaternion.Euler(0f, smoothedYAngled, 0f);

            _stateMachine.Player.Rigidbody.MoveRotation(targetRot);
        }

        protected Vector3 GetPlayerHorizontalVelocity()
        {
            Vector3 playerHorizontalVelocity = _stateMachine.Player.Rigidbody.velocity;
            playerHorizontalVelocity.y = 0f;

            return playerHorizontalVelocity;
        }

        protected Vector3 GetTargetRotationDirection(float targetAngle)
        {
            return Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
        }

        protected Vector3 GetMovementDirection()
        {
            return new Vector3(_movementInput.x, 0f, _movementInput.y);
        }

        protected float GetMovementSpeed()
        {
            return _baseSpeed * _speedModif;
        }

        #endregion
    }
}