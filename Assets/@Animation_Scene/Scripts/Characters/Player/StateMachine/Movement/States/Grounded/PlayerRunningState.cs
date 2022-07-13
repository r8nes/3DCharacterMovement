using System.Collections;
using System.Collections.Generic;
using ActionCatGame.Core.State;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ActionCatGame.Core.PlayerState
{
    public class PlayerRunningState : PlayerMovingState
    {
        public PlayerRunningState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
        {
        }

        #region IState Methods
        public override void Enter()
        {
            base.Enter();

            _stateMachine.ReusableData.MovementSpeedMod = _movementData.RunData.SpeedModif;
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
