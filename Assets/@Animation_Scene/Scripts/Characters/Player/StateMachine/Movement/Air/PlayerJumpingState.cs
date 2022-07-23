using System;
using System.Collections;
using System.Collections.Generic;
using ActionCatGame.Core.State;
using UnityEngine;

namespace ActionCatGame.Core.PlayerState
{
    public class PlayerJumpingState : PlayerAirborneState
    {
        public PlayerJumpingState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
        {
        }
        
        #region IState Method
        
        public override void Enter()
        {
            base.Enter();

            _stateMachine.ReusableData.MovementSpeedMod = 0f;

            Jump();
        }

        #endregion

        #region Main Method

        private void Jump()
        {
            Vector3 jumpForce = _stateMachine.ReusableData.CurrentJumpForce;

            Vector3 playerForward = _stateMachine.Player.transform.forward;

            jumpForce.x *= playerForward.x;
            jumpForce.z *= playerForward.z;

            ResetVelocity(); 

            _stateMachine.Player.Rigidbody.AddForce(jumpForce, ForceMode.VelocityChange);
        }

        #endregion
    }
}
