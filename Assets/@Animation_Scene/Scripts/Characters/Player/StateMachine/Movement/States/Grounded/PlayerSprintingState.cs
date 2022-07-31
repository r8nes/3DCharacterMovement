using System;
using System.Collections;
using System.Collections.Generic;
using ActionCatGame.Core.Data;
using ActionCatGame.Core.State;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ActionCatGame.Core.PlayerState
{
    public class PlayerSprintingState : PlayerMovingState
    {
        private float _startTime;

        private bool _keepSprinting;
        private bool _shouldResetSprintState;

        private PlayerSprintData _data;
        public PlayerSprintingState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
        {
            _data = _movementData.SprintData;
        }

        #region IState Methods

        public override void Enter()
        {
            _stateMachine.ReusableData.MovementSpeedMod = _data.SpeedModif;

            base.Enter();

            StartAnimation(_stateMachine.Player.AnimationData.SprintParameterHash);

            _stateMachine.ReusableData.CurrentJumpForce = _airborneData.JumpData.StrongForce;

            _shouldResetSprintState = true;

            _startTime = Time.time;
        }

        public override void Update()
        {
            base.Update();

            if (_keepSprinting) return;

            if (Time.time < _startTime + _data.SprintToRunTime) return;

            StopSprinting();
        }

        public override void Exit()
        {
            base.Exit();

            StopAnimation(_stateMachine.Player.AnimationData.SprintParameterHash);

            if (_shouldResetSprintState)
            {
                _keepSprinting = false;

                _stateMachine.ReusableData.ShouldSprint = false;
            }
        }
        #endregion

        #region Main Methods

        private void StopSprinting()
        {
            if (_stateMachine.ReusableData.MovementInput == Vector2.zero)
            {
                _stateMachine.ChangeState(_stateMachine.IdlingState);
                return;
            }

            _stateMachine.ChangeState(_stateMachine.RunningState);
        }

        #endregion

        #region Reusable Methods

        protected override void AddInputActionsCallBacks()
        {
            base.AddInputActionsCallBacks();

            _stateMachine.Player.Input.PlayerActions.Sprint.performed += OnSprintPerformed;
        }

        protected override void RemoveInputActionsCallbacks()
        {
            base.RemoveInputActionsCallbacks();

            _stateMachine.Player.Input.PlayerActions.Sprint.performed -= OnSprintPerformed;
        }

        protected override void OnFall()
        {
            _shouldResetSprintState = false;

            base.OnFall();
        }
        #endregion

        #region Input Methods

        protected override void OnMovementCanceled(InputAction.CallbackContext obj)
        {
            _stateMachine.ChangeState(_stateMachine.HardStoppingState);

            base.OnMovementCanceled(obj);
        }

        protected override void OnJumpStarted(InputAction.CallbackContext obj)
        {
            _shouldResetSprintState = false;

            base.OnJumpStarted(obj);
        }

        private void OnSprintPerformed(InputAction.CallbackContext obj)
        {
            _keepSprinting = true;

            _stateMachine.ReusableData.ShouldSprint = true;
        }

        #endregion
    }
}
