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
            base.Enter();

            _stateMachine.ReusableData.MovementSpeedMod = _data.SpeedModif;

            _stateMachine.ReusableData.RotationData = _data.RotationData;

            AddForceOnStationaryState();

            _shouldKeepRotating = _stateMachine.ReusableData.MovementInput != Vector2.zero;

            UpdateRowDashed();

            _startTime = Time.time;
        }


        public override void Exit()
        {
            base.Exit();

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

        private void AddForceOnStationaryState()
        {
            if (_stateMachine.ReusableData.MovementInput != Vector2.zero) return;

            Vector3 characterRotationDir = _stateMachine.Player.transform.forward;

            characterRotationDir.y = 0f;

            UpdateTargetRotation(characterRotationDir, false);

            _stateMachine.Player.Rigidbody.velocity = characterRotationDir * GetMovementSpeed();
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

        protected override void OnMovementCanceled(InputAction.CallbackContext obj)
        {
        }

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
