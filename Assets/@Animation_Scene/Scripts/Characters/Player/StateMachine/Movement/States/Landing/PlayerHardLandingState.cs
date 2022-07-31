using System;
using System.Collections;
using System.Collections.Generic;
using ActionCatGame.Core.State;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ActionCatGame.Core.PlayerState
{
    public class PlayerHardLandingState : PlayerLandingState
    {
        public PlayerHardLandingState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
        {
        }

        #region IState Methods

        public override void Enter()
        {
            _stateMachine.ReusableData.MovementSpeedMod = 0f;

            base.Enter();

            StartAnimation(_stateMachine.Player.AnimationData.HardLandingParameterHash);

            _stateMachine.Player.Input.PlayerActions.Move.Disable();

            ResetVelocity();
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();

            if (!IsMovingHorizontally()) return;

            ResetVelocity();
        }

        public override void Exit()
        {
            base.Exit();

            StopAnimation(_stateMachine.Player.AnimationData.HardLandingParameterHash);

            _stateMachine.Player.Input.PlayerActions.Move.Enable() ;
        }

        public override void OnAnimationExitEvent()
        {
            _stateMachine.Player.Input.PlayerActions.Move.Enable();
        }

        public override void OnAnimationTransitionEvent()
        {
            _stateMachine.ChangeState(_stateMachine.IdlingState );
        }

        #endregion

        #region Reusable Methods

        protected override void AddInputActionsCallBacks()
        {
            base.AddInputActionsCallBacks();

            _stateMachine.Player.Input.PlayerActions.Move.started += OnMovementStarted;
        }

        protected override void RemoveInputActionsCallbacks()
        {
            _stateMachine.Player.Input.PlayerActions.Move.started -= OnMovementStarted;
        }

        protected override void OnMove()
        {
            if (_stateMachine.ReusableData.ShouldWalk) return;

            _stateMachine.ChangeState(_stateMachine.RunningState);
        }

        #endregion

        #region Input Mrthods
        protected override void OnJumpStarted(InputAction.CallbackContext obj)
        {
        }

        private void OnMovementStarted(InputAction.CallbackContext obj)
        {
            OnMove();
        }

        #endregion
    }
}
