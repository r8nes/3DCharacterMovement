using System;
using System.Collections;
using System.Collections.Generic;
using ActionCatGame.Core.Data;
using ActionCatGame.Core.State;
using UnityEngine;

namespace ActionCatGame.Core.PlayerState
{
    public class PlayerIdlingState : PlayerGroundedState
    {
        private PlayerIdleData _data;
        public PlayerIdlingState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
        {
            _data = _movementData.IdleData;
        }

        #region IState Method
        
        public override void Enter() 
        {
            _stateMachine.ReusableData.MovementSpeedMod = 0f;

            _stateMachine.ReusableData.BackwardsCameraRecentering = _data.BackwardsCameraRecentering;

            base.Enter();

            _stateMachine.ReusableData.CurrentJumpForce = _airborneData.JumpData.StationaryForce;

            ResetVelocity();
        }

        public override void Update()
        {
            base.Update();

            if (_stateMachine.ReusableData.MovementInput == Vector2.zero) return;

            OnMove();
        }
        #endregion
    }
}
