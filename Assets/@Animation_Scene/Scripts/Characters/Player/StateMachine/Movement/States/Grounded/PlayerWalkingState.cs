using System;
using System.Collections;
using System.Collections.Generic;
using ActionCatGame.Core.Data;
using ActionCatGame.Core.State;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ActionCatGame.Core.PlayerState
{
    public class PlayerWalkingState : PlayerMovingState
    {
        private PlayerWalkData _walkData;

        public PlayerWalkingState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
        {
            _walkData = _movementData.WalkData;
        }

        #region IState Methods

        public override void Enter()
        {
            _stateMachine.ReusableData.MovementSpeedMod = _walkData.SpeedModif;

            _stateMachine.ReusableData.BackwardsCameraRecentering = _walkData.BackwardsCameraRecentering;

            base.Enter();

            StartAnimation(_stateMachine.Player.AnimationData.WalkParameterHash);

            _stateMachine.ReusableData.CurrentJumpForce = _airborneData.JumpData.WeakForce;
        }

        public override void Exit()
        {
            base.Exit();

            StopAnimation(_stateMachine.Player.AnimationData.WalkParameterHash);

            SetBaseCameraRecenteringData();
        }

        #endregion

        #region Input Methods

        protected override void OnMovementCanceled(InputAction.CallbackContext obj)
        {
            _stateMachine.ChangeState(_stateMachine.LightStoppingState);

            base.OnMovementCanceled(obj);
        }

        protected override void OnWalkToggleStatred(InputAction.CallbackContext context)
        {
            base.OnWalkToggleStatred(context);

            _stateMachine.ChangeState(_stateMachine.RunningState);
        }
        #endregion
    }
}
