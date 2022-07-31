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

        #region IState Methods

        public override void Enter()
        {
            base.Enter();

            StartAnimation(_stateMachine.Player.AnimationData.AirborneParameterHash);

            ResetSprintState();
        }

        public override void Exit()
        {
            base.Exit();

            StopAnimation(_stateMachine.Player.AnimationData.AirborneParameterHash);
        }

        #endregion

        #region Reusable Methods

        protected override void OnContactWithGround(Collider collider)
        {
            _stateMachine.ChangeState(_stateMachine.LightLandingState);
        }

        protected virtual void ResetSprintState() 
        {
            _stateMachine.ReusableData.ShouldSprint = false;
        }

        #endregion
    }
}
