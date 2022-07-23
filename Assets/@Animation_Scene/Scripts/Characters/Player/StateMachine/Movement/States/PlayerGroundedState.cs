using System;
using System.Collections;
using System.Collections.Generic;
using ActionCatGame.Core.State;
using ActionCatGame.Core.Utilities;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ActionCatGame.Core.PlayerState
{
    public class PlayerGroundedState : PlayerMovementState
    {
        private SlopeData _slopeData;
        public PlayerGroundedState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
        {
            _slopeData = _stateMachine.Player.ColliderUtility.SlopeData;
        }

        #region IState Methods

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();

            Float();
        }
        #endregion

        #region Main Methods

        private void Float()
        {
            Vector3 capsuleColliderCenterInWorldSpace = _stateMachine.Player.ColliderUtility.CapsuleColliderData.Collider.bounds.center;

            Ray downwardsRayFromCapsuleCenter = new Ray(capsuleColliderCenterInWorldSpace, Vector3.down);

            if (Physics.Raycast(downwardsRayFromCapsuleCenter, out RaycastHit hit, _slopeData.FloatRayDistance, _stateMachine.Player.LayerData.GroundLayer, QueryTriggerInteraction.Ignore))
            {
                float groundAngle = Vector3.Angle(hit.normal, -downwardsRayFromCapsuleCenter.direction);

                float slopeSpeedModif = SetSlopeSpeedOnAngle(groundAngle);

                if (slopeSpeedModif == 0f) return;

                float distance = _stateMachine.Player.ColliderUtility.CapsuleColliderData.ColliderCenterInLocalSpace.y * _stateMachine.Player.transform.localScale.y - hit.distance;

                if (distance == 0f) return;

                float amountToLift = distance * _slopeData.StepReachForce - GetPlayerVerticalVelocity().y;

                Vector3 liftForce = new Vector3(0f, amountToLift, 0f);

                _stateMachine.Player.Rigidbody.AddForce(liftForce, ForceMode.VelocityChange);
            }
        }

        private float SetSlopeSpeedOnAngle(float angle)
        {
            float slopeSpeedMofid = _movementData.SlopeSpeedAngle.Evaluate(angle);

            _stateMachine.ReusableData.MovementOnSlopesSpeedMod = slopeSpeedMofid;

            return slopeSpeedMofid;
        }

        #endregion

        #region Reusable Methods

        protected override void AddInputActionsCallBacks()
        {
            base.AddInputActionsCallBacks();

            _stateMachine.Player.Input.PlayerActions.Move.canceled += OnMovementCanceled;

            _stateMachine.Player.Input.PlayerActions.Dash.started += OnDashStarted;

            _stateMachine.Player.Input.PlayerActions.Jump.started += OnJumpStarted;
        }

        protected override void RemoveInputActionsCallbacks()
        {
            base.RemoveInputActionsCallbacks();

            _stateMachine.Player.Input.PlayerActions.Move.canceled -= OnMovementCanceled;

            _stateMachine.Player.Input.PlayerActions.Dash.started -= OnDashStarted;

            _stateMachine.Player.Input.PlayerActions.Jump.started -= OnJumpStarted;
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

        protected virtual void OnJumpStarted(InputAction.CallbackContext obj)
        {
            _stateMachine.ChangeState(_stateMachine.JumpingState);
        } 

        protected virtual void OnMovementCanceled(InputAction.CallbackContext obj)
        {
            _stateMachine.ChangeState(_stateMachine.IdlingState);
        }

        protected virtual void OnDashStarted(InputAction.CallbackContext obj)
        {
            _stateMachine.ChangeState(_stateMachine.DashingState);
        }

        #endregion
    }
}
