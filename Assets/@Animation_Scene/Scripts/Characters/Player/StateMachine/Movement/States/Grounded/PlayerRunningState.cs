using System;
using System.Collections;
using System.Collections.Generic;
using ActionCatGame.Core.Data;
using ActionCatGame.Core.State;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ActionCatGame.Core.PlayerState
{
    public class PlayerRunningState : PlayerMovingState
    {
        private PlayerSprintData _sprintData;
        private float _startTime;
        public PlayerRunningState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
        {
            _sprintData = _movementData.SprintData;
        }

        #region IState Methods
        public override void Enter()
        {
            base.Enter();

            _stateMachine.ReusableData.MovementSpeedMod = _movementData.RunData.SpeedModif;

            _startTime = Time.time;
        }

        public override void Update()
        {
            base.Update();

            if (!_stateMachine.ReusableData.ShouldWalk) return;

            if (Time.time < _startTime + _sprintData.RunToWalkTime) return;

            StopRunning();
        }

        #endregion

        #region Main Methods

        private void StopRunning()
        {
            if (_stateMachine.ReusableData.MovementInput == Vector2.zero)
            {
                _stateMachine.ChangeState(_stateMachine.IdlingState);
                return;
            }

            _stateMachine.ChangeState(_stateMachine.WalkingState);
        }

        #endregion

        #region Input Methods

        protected override void OnWalkToggleStatred(InputAction.CallbackContext context)
        {
            base.OnWalkToggleStatred(context);

            _stateMachine.ChangeState(_stateMachine.WalkingState);
        }
        #endregion
    }
}
