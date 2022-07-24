using System.Collections;
using System.Collections.Generic;
using ActionCatGame.Core.State;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ActionCatGame.Core.PlayerState
{
    public class PlayerLightLandingState : PlayerLandingState
    {
        public PlayerLightLandingState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
        {
        }

        #region IState Method

        public override void Enter()
        {
            base.Enter();

            _stateMachine.ReusableData.MovementSpeedMod = 0f;

            _stateMachine.ReusableData.CurrentJumpForce = _airborneData.JumpData.StationaryForce;

            ResetVelocity();
        }

        public override void Update()
        {
            base.Update();

            if (_stateMachine.ReusableData.MovementInput == Vector2.zero) return;

            OnMove();
        }

        public override void OnAnimationTransitionEvent()
        { 
            _stateMachine.ChangeState(_stateMachine.IdlingState);
        }


        #endregion
    }
}
