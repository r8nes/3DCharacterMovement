using System;
using System.Collections;
using System.Collections.Generic;
using ActionCatGame.Core.State;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ActionCatGame.Core.PlayerState
{
    public class PlayerWalkingState : PlayerMovingState
    {
        public PlayerWalkingState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
        {
        }

        #region IState Methods

        public override void Enter()
        {
            base.Enter();

            _stateMachine.ReusableData.MovementSpeedMod = _movementData.WalkData.SpeedModif;
        }

        #endregion

        #region Input Methods

        protected override void OnMovementCanceled(InputAction.CallbackContext obj)
        {
            _stateMachine.ChangeState(_stateMachine.LightStoppingState);
        }

        protected override void OnWalkToggleStatred(InputAction.CallbackContext context)
        {
            base.OnWalkToggleStatred(context);

            _stateMachine.ChangeState(_stateMachine.RunningState);
        }
        #endregion
    }
}
