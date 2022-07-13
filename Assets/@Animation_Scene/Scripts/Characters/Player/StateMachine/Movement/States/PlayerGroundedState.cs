using System.Collections;
using System.Collections.Generic;
using ActionCatGame.Core.State;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ActionCatGame.Core.PlayerState
{
    public class PlayerGroundedState : PlayerMovementState
    {
        public PlayerGroundedState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
        {
        }

        #region Reusable Methods

        protected override void AddInputActionsCallBacks()
        {
            base.AddInputActionsCallBacks();

            _stateMachine.Player.Input.PlayerActions.Move.canceled += OnMovementCanceled;
        }

        protected override void RemoveInoutActionsCallbacks()
        {
            base.RemoveInoutActionsCallbacks();
        }

        protected virtual void OnMove()
        {
            if (_stateMachine.ReusableData.ShouldWalk)
            {
                _stateMachine.ChangeState(_stateMachine.WalkingState);
                return;
            }

            _stateMachine.ChangeState(_stateMachine.RunningState);
        }
        #endregion

        #region Input Methods

        protected virtual void OnMovementCanceled(InputAction.CallbackContext obj)
        {
            _stateMachine.ChangeState(_stateMachine.IdlingState);
        }

        #endregion
    }
}
