using System.Collections;
using System.Collections.Generic;
using ActionCatGame.Core.State;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ActionCatGame.Core.PlayerState
{
    public class PlayerLandingState : PlayerGroundedState
    {
        public PlayerLandingState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
        {
        }

        #region Input Methods

        protected override void OnMovementCanceled(InputAction.CallbackContext obj)
        {

        }

        #endregion
    }
}
