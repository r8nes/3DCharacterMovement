using System.Collections;
using System.Collections.Generic;
using ActionCatGame.Core.State;
using UnityEngine;

namespace ActionCatGame.Core.PlayerState
{
    public class PlayerAirborneState : PlayerMovementState
    {
        public PlayerAirborneState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
        {
        }

        #region Reusable Methods

        protected override void OnContactWithGround(Collider collider)
        {
            _stateMachine.ChangeState(_stateMachine.IdlingState);
        }

        #endregion
    }
}
