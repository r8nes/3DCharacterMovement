using System;
using System.Collections;
using System.Collections.Generic;
using ActionCatGame.Core.Data;
using ActionCatGame.Core.State;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ActionCatGame.Core.State
{
    public class PlayerMovementState : IState
    {
        protected PlayerGroundedData _movementData;

        protected PlayerMovementStateMachine _stateMachine;
        public PlayerMovementState(PlayerMovementStateMachine playerMovementStateMachine)
        {
            _stateMachine = playerMovementStateMachine;
            _movementData = _stateMachine.Player.Data.GroundedData;
            InitData();
        }

        private void InitData() 
        {
            _stateMachine.ReusableData.TimeToReachTargetRotation = _movementData.BaseRotationData.TargetRotationReachTime;
        }

        #region IState Methods
        public virtual void Enter()
        {
            Debug.Log("State:" + GetType().Name);

            AddInputActionsCallBacks();
        }

        public virtual void Exit()
        {
            RemoveInputActionsCallbacks();
        }

        public virtual void HandleInput()
        {
            ReadMovementInput();
        }

        public virtual void Update()
        {
        }

        public virtual void PhysicsUpdate()
        {
            Move();
        }


        public virtual void OnAnimationEnterEvent()
        {
        }

        public virtual void OnAnimationExitEvent()
        {
        }

        public virtual void OnAnimationTransitionEvent()
        {
        }

        #endregion

        #region Main Method

        private void Move()
        {
            if (_stateMachine.ReusableData.MovementInput == Vector2.zero || _stateMachine.ReusableData.MovementSpeedMod == 0f) return;

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
            _stateMachine.ReusableData.ÑurrentTargetRotation.y = targetAngle;

            _stateMachine.ReusableData.DampedTargetRotationPassedTime.y = 0f;
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
            _stateMachine.ReusableData.MovementInput = _stateMachine.Player.Input.PlayerActions.Move.ReadValue<Vector2>();
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

            if (directionAngle != _stateMachine.ReusableData.ÑurrentTargetRotation.y)
            {
                UpdateTargetRotationData(directionAngle);
            }

            return directionAngle;
        }

        protected void RotateTowardsTargetRotation()
        {
            float currentYAngle = _stateMachine.Player.Rigidbody.rotation.eulerAngles.y;

            if (currentYAngle == _stateMachine.ReusableData.ÑurrentTargetRotation.y) return;

            float smoothedYAngled = Mathf.SmoothDampAngle(currentYAngle, _stateMachine.ReusableData.ÑurrentTargetRotation.y, ref _stateMachine.ReusableData.DampedTargetRotationCurrentVelocity.y, _stateMachine.ReusableData.TimeToReachTargetRotation.y - _stateMachine.ReusableData.DampedTargetRotationPassedTime.y);
            _stateMachine.ReusableData.DampedTargetRotationPassedTime.y += Time.deltaTime;

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

        protected Vector3 GetPlayerVerticalVelocity() 
        {
            return new Vector3(0f, _stateMachine.Player.Rigidbody.velocity.y, 0f);
        }

        protected Vector3 GetMovementDirection()
        {
            return new Vector3(_stateMachine.ReusableData.MovementInput.x, 0f, _stateMachine.ReusableData.MovementInput.y);
        }

        protected float GetMovementSpeed()
        {
            return _movementData.BaseSpeed * _stateMachine.ReusableData.MovementSpeedMod * _stateMachine.ReusableData.MovementOnSlopesSpeedMod;
        }

        protected void ResetVelocity() 
        {
            _stateMachine.Player.Rigidbody.velocity = Vector3.zero;
        }

        protected virtual void AddInputActionsCallBacks()
        {
            _stateMachine.Player.Input.PlayerActions.WalkToggle.started += OnWalkToggleStatred;
        }

        protected virtual void RemoveInputActionsCallbacks()
        {
            _stateMachine.Player.Input.PlayerActions.WalkToggle.started -= OnWalkToggleStatred;
        }

        protected virtual void DecelerateHorizontally() 
        {
            Vector3 playerHorizontalVelocity = GetPlayerHorizontalVelocity();

            _stateMachine.Player.Rigidbody.AddForce(-playerHorizontalVelocity * _stateMachine.ReusableData.MovementDecelerationForce, ForceMode.Acceleration);
        }

        protected bool IsMovingHorizontally(float minMagnitude = 0.1f) 
        {
            Vector3 playerHorizontallyVelocity = GetPlayerHorizontalVelocity();

            Vector2 playerHorizontalMovement = new Vector2(playerHorizontallyVelocity.x, playerHorizontallyVelocity.z);

            return playerHorizontalMovement.magnitude > minMagnitude;
        }

        #endregion

        #region Input Methods

        protected virtual void OnWalkToggleStatred(InputAction.CallbackContext context)
        {
            _stateMachine.ReusableData.ShouldWalk = !_stateMachine.ReusableData.ShouldWalk;

        }

        #endregion
    }
}