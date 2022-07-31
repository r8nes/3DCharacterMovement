using System;
using System.Collections;
using System.Collections.Generic;
using ActionCatGame.Core.Data;
using ActionCatGame.Core.State;
using UnityEngine;

namespace ActionCatGame.Core.PlayerState
{
    public class PlayerFallingState : PlayerAirborneState
    {
        private PlayerFallData _data;

        private Vector3 _playerPositionOnEnter;

        public PlayerFallingState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
        {
            _data = _airborneData.FallData;
        }

        #region IState Methods

        public override void Enter()
        {
            base.Enter();

            StartAnimation(_stateMachine.Player.AnimationData.FallParameterHash);

            _playerPositionOnEnter = _stateMachine.Player.transform.position; 

            _stateMachine.ReusableData.MovementSpeedMod = 0f;

            ResetVerticalVelocity();
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();

            LimitVerticalVelocity();
        }

        public override void Exit()
        {
            base.Exit();

            StopAnimation(_stateMachine.Player.AnimationData.FallParameterHash);
        }

        #endregion

        #region Reusable Methods

        protected override void ResetSprintState()
        {
        }

        protected override void OnContactWithGround(Collider collider)
        {
            float fallDistance = _playerPositionOnEnter.y - _stateMachine.Player.transform.position.y;

            if (fallDistance < _data.MinDistanceFall)
            {
                _stateMachine.ChangeState(_stateMachine.LightLandingState);

                return;
            }

            if (_stateMachine.ReusableData.ShouldWalk 
                && !_stateMachine.ReusableData.ShouldSprint 
                || _stateMachine.ReusableData.MovementInput == Vector2.zero) 
            {
                _stateMachine.ChangeState(_stateMachine.HardLandingState);
                
                return;
            }

            _stateMachine.ChangeState(_stateMachine.RollingState);
        }

        #endregion

        #region Main Methods

        private void LimitVerticalVelocity()
        {
            Vector3 playerVerticalVelocity = GetPlayerVerticalVelocity();

            if (_stateMachine.Player.Rigidbody.velocity.y >= -_data.FallSpeedLimit) return;

            Vector3 limitedVelocity = new Vector3(0f, -_data.FallSpeedLimit - playerVerticalVelocity.y, 0f);

            _stateMachine.Player.Rigidbody.AddForce(limitedVelocity, ForceMode.VelocityChange);
        }

        #endregion
    }
}
