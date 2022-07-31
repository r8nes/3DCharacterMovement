using System.Collections;
using System.Collections.Generic;
using ActionCatGame.Core.State;
using UnityEngine;

namespace ActionCatGame.Core.PlayerState
{
    public class PlayerMediumStoppingState : PlayerStoppingState
    {
        public PlayerMediumStoppingState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
        {
        }

        #region IState Methods

        public override void Enter()
        {
            base.Enter();

            StartAnimation(_stateMachine.Player.AnimationData.MediumStopParameterHash);

            _stateMachine.ReusableData.MovementDecelerationForce = _movementData.StopData.MediumDecelerationForce;

            _stateMachine.ReusableData.CurrentJumpForce = _airborneData.JumpData.MediumForce;
        }

        public override void Exit()
        {
            base.Exit();

            StopAnimation(_stateMachine.Player.AnimationData.MediumStopParameterHash);
        }

        #endregion
    }
}
