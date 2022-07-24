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

            ResetSprintState();
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
