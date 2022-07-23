using System.Collections;
using System.Collections.Generic;
using ActionCatGame.Core.State;
using UnityEngine;

namespace ActionCatGame.Core.PlayerState
{
    public class PlayerLightStoppingState : PlayerStoppingState
    {
        public PlayerLightStoppingState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
        {
        }

        #region IState Methods

        public override void Enter()
        {
            base.Enter();

            _stateMachine.ReusableData.MovementDecelerationForce = _movementData.StopData.LightDecelerationForce;

            _stateMachine.ReusableData.CurrentJumpForce = _airborneData.JumpData.WeakForce;
        }

        #endregion
    }
}
