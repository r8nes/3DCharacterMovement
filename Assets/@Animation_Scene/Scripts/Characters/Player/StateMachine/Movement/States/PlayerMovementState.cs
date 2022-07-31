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
        protected PlayerAirborneData _airborneData;

        protected PlayerMovementStateMachine _stateMachine;

        public PlayerMovementState(PlayerMovementStateMachine playerMovementStateMachine)
        {
            _stateMachine = playerMovementStateMachine;

            _movementData = _stateMachine.Player.Data.GroundedData;
            _airborneData = _stateMachine.Player.Data.AirborneData;

            SetBaseCameraRecenteringData();

            InitData();
        }

        private void InitData()
        {
            SetBaseRotationData();
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

        public virtual void OnTriggerEnter(Collider collider)
        {
            if (_stateMachine.Player.LayerData.IsGroundLayer(collider.gameObject.layer))
            {
                OnContactWithGround(collider);

                return;
            }
        }

        public void OnTriggerExit(Collider collider)
        {
            if (_stateMachine.Player.LayerData.IsGroundLayer(collider.gameObject.layer))
            {
                OnContactWithGroundExited(collider);

                return;
            }
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
        protected void StartAnimation(int hash)
        {
            _stateMachine.Player.Animator.SetBool(hash, true);
        }

        protected void StopAnimation(int hash)
        {
            _stateMachine.Player.Animator.SetBool(hash, false);
        }

        protected void SetBaseRotationData()
        {
            _stateMachine.ReusableData.RotationData = _movementData.BaseRotationData;

            _stateMachine.ReusableData.TimeToReachTargetRotation = _stateMachine.ReusableData.RotationData.TargetRotationReachTime;
        }

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

        protected float GetMovementSpeed(bool shouldConsidersSlopes = true)
        {
            float movementSpeed = _movementData.BaseSpeed * _stateMachine.ReusableData.MovementSpeedMod * _stateMachine.ReusableData.MovementOnSlopesSpeedMod;

            if (shouldConsidersSlopes)
            {
                movementSpeed *= _stateMachine.ReusableData.MovementOnSlopesSpeedMod;
            }

            return movementSpeed;
        }

        protected void ResetVelocity() 
        {
            _stateMachine.Player.Rigidbody.velocity = Vector3.zero;
        }

        protected void ResetVerticalVelocity()
        {
            Vector3 playerHorizontalVelocity = GetPlayerHorizontalVelocity();

            _stateMachine.Player.Rigidbody.velocity = playerHorizontalVelocity;
        }

        protected virtual void AddInputActionsCallBacks()
        {
            _stateMachine.Player.Input.PlayerActions.WalkToggle.started += OnWalkToggleStatred;
            _stateMachine.Player.Input.PlayerActions.Look.started += OnMouseMovementStarted;
            _stateMachine.Player.Input.PlayerActions.Move.canceled += OnMovementCanceled;
            _stateMachine.Player.Input.PlayerActions.Move.performed += OnMovementPerformed;
        }

        protected virtual void RemoveInputActionsCallbacks()
        {
            _stateMachine.Player.Input.PlayerActions.WalkToggle.started -= OnWalkToggleStatred;
            _stateMachine.Player.Input.PlayerActions.Look.started -= OnMouseMovementStarted;
            _stateMachine.Player.Input.PlayerActions.Move.canceled -= OnMovementCanceled;
            _stateMachine.Player.Input.PlayerActions.Move.performed -= OnMovementPerformed;
        }

        protected virtual void DecelerateHorizontally() 
        {
            Vector3 playerHorizontalVelocity = GetPlayerHorizontalVelocity();

            _stateMachine.Player.Rigidbody.AddForce(-playerHorizontalVelocity * _stateMachine.ReusableData.MovementDecelerationForce, ForceMode.Acceleration);
        }

        protected virtual void DecelerateVertically() 
        {
            Vector3 playerVerticalVelocity = GetPlayerVerticalVelocity();

            _stateMachine.Player.Rigidbody.AddForce(-playerVerticalVelocity * _stateMachine.ReusableData.MovementDecelerationForce, ForceMode.Acceleration);
        }

        protected bool IsMovingHorizontally(float minMagnitude = 0.1f) 
        {
            Vector3 playerHorizontallyVelocity = GetPlayerHorizontalVelocity();

            Vector2 playerHorizontalMovement = new Vector2(playerHorizontallyVelocity.x, playerHorizontallyVelocity.z);

            return playerHorizontalMovement.magnitude > minMagnitude;
        }

        protected bool IsMovingUp(float minimumVelocity = 0.1f) 
        {
            return GetPlayerVerticalVelocity().y > minimumVelocity;
        }

        protected bool IsMovingDown(float minimumVelocity = 0.1f)
        {
            return GetPlayerVerticalVelocity().y < -minimumVelocity;
        }

        protected virtual void OnContactWithGround(Collider collider)
        {
        }
        protected virtual void OnContactWithGroundExited(Collider collider)
        {
        }

        protected void UpdateCameraRecenteringState(Vector2 movementInput) 
        {
            if (movementInput == Vector2.zero) return;

            if (movementInput == Vector2.up)
            {
                DisableCameraRecentering();

                return;
            }

            float cameraVerticalAngle = _stateMachine.Player.MainCameraTransform.eulerAngles.x;

            if (cameraVerticalAngle >= 270f)
            {
                cameraVerticalAngle -= 360f;
            }

            cameraVerticalAngle = Mathf.Abs(cameraVerticalAngle);

            if (movementInput == Vector2.down)
            {
                SetCameraRecentering(cameraVerticalAngle, _stateMachine.ReusableData.BackwardsCameraRecentering);

                return;
            }

            SetCameraRecentering(cameraVerticalAngle, _stateMachine.ReusableData.SideWaysCameraRecentering);
        }

        private void SetCameraRecentering(float cameraVerticalAngle, List<PlayerCameraRecenteringData> cameraRecentering)
        {
            foreach (PlayerCameraRecenteringData data in cameraRecentering)
            {
                if (!data.IsWithinRange(cameraVerticalAngle))
                    continue;

                EnableCameraRecentering(data.WaitTime, data.RecenteringTime);

                return;
            }

            DisableCameraRecentering();
        }

        protected void EnableCameraRecentering(float waitTime = -1, float recenteringTime = -1f) 
        {
            float movementSpeed = GetMovementSpeed();

            if (movementSpeed == 0f)
            {
                movementSpeed = _movementData.BaseSpeed;
            }

            _stateMachine.Player.CameraUtility.EnableRecentering(waitTime, recenteringTime, _movementData.BaseSpeed, movementSpeed);
        }

        protected void DisableCameraRecentering() 
        {
            _stateMachine.Player.CameraUtility.DisableRecentering();
        }

        protected void SetBaseCameraRecenteringData()
        {
            _stateMachine.ReusableData.BackwardsCameraRecentering = _movementData.BackwardsCameraRecentering;
            _stateMachine.ReusableData.SideWaysCameraRecentering = _movementData.SidewaysCameraRecentering;
        }

        #endregion 

        #region Input Methods 

        protected virtual void OnWalkToggleStatred(InputAction.CallbackContext context)
        {
            _stateMachine.ReusableData.ShouldWalk = !_stateMachine.ReusableData.ShouldWalk;
        }

        private void OnMouseMovementStarted(InputAction.CallbackContext obj)
        {
            UpdateCameraRecenteringState(_stateMachine.ReusableData.MovementInput);
        }

        private void OnMovementPerformed(InputAction.CallbackContext obj)
        {
            UpdateCameraRecenteringState(obj.ReadValue<Vector2>());
        }

        protected virtual void OnMovementCanceled(InputAction.CallbackContext obj)
        {
            DisableCameraRecentering();
        }

        #endregion
    }
}