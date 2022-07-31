using System;
using ActionCatGame.Core.Data;
using ActionCatGame.Core.State;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ActionCatGame.Core.PlayerState
{
    public class PlayerDashingState : PlayerGroundedState
    {
        private PlayerDashData _data;

        private float _startTime;

        private int _dashedUsed;

        private bool _shouldKeepRotating;

        public PlayerDashingState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
        {
            _data = _movementData.DashData;
        }
        
        #region IState Methods
        
        public override void Enter()
        {
            _stateMachine.ReusableData.MovementSpeedMod = _data.SpeedModif;
            
            base.Enter();

            StartAnimation(_stateMachine.Player.AnimationData.DashParameterHash);

            _stateMachine.ReusableData.CurrentJumpForce = _airborneData.JumpData.StrongForce;

            _stateMachine.ReusableData.RotationData = _data.RotationData;

            Dash();

            _shouldKeepRotating = _stateMachine.ReusableData.MovementInput != Vector2.zero;

            UpdateRowDashed();

            _startTime = Time.time;
        }


        public override void Exit()
        {
            base.Exit();

            StopAnimation(_stateMachine.Player.AnimationData.DashParameterHash);

            SetBaseRotationData();
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();

            if (!_shouldKeepRotating) return;

            RotateTowardsTargetRotation();
        }

        public override void OnAnimationTransitionEvent()
        {
            if (_stateMachine.ReusableData.MovementInput == Vector2.zero)
            {
                _stateMachine.ChangeState(_stateMachine.HardStoppingState);
                
                return;
            }

            _stateMachine.ChangeState(_stateMachine.SprintingState);
        }

        #endregion

        #region Main Methods

        private void Dash()
        {
            Vector3 dashDirection = _stateMachine.Player.transform.forward;

            dashDirection.y = 0f;

            UpdateTargetRotation(dashDirection, false);

            if (_stateMachine.ReusableData.MovementInput != Vector2.zero) 
            {
                UpdateTargetRotation(GetMovementDirection());

                dashDirection = GetTargetRotationDirection(_stateMachine.ReusableData.ÑurrentTargetRotation.y);
            }

            _stateMachine.Player.Rigidbody.velocity = dashDirection * GetMovementSpeed(false);
        }

        private void UpdateRowDashed()
        {
            if (!IsInRow()) _dashedUsed = 0;
            
            ++_dashedUsed;

            if (_dashedUsed == _data.DashesLimit)
            {
                _dashedUsed = 0;

                _stateMachine.Player.Input.DisableActionFor(_stateMachine.Player.Input.PlayerActions.Dash, _data.DashLimitCooldown);      
            }
        }

        private bool IsInRow()
        {
            return Time.time < _startTime + _data.TimeToBeConsiderRow;
        }

        #endregion

        #region Input Methods

        protected override void OnDashStarted(InputAction.CallbackContext obj)
        {
        }


        private void OnMovementPerformed(InputAction.CallbackContext obj)
        {
            _shouldKeepRotating = true;
        }

        #endregion

        #region Reusable Methods

        protected override void AddInputActionsCallBacks()
        {
            base.AddInputActionsCallBacks();

            _stateMachine.Player.Input.PlayerActions.Move.performed += OnMovementPerformed;
        }

        protected override void RemoveInputActionsCallbacks()
        {
            base.RemoveInputActionsCallbacks();
            
            _stateMachine.Player.Input.PlayerActions.Move.performed -= OnMovementPerformed;
        }

        #endregion
    }
}
