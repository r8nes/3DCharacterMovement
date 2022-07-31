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

        #region IState Methods

        public override void Enter()
        {
            base.Enter();

            StartAnimation(_stateMachine.Player.AnimationData.LandingParameterHash);
        }

        public override void Exit()
        {
            base.Exit();

            StopAnimation(_stateMachine.Player.AnimationData.LandingParameterHash);
        }

        #endregion
    }
}
