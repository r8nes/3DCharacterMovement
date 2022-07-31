using ActionCatGame.Core.Data;
using ActionCatGame.Core.State;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ActionCatGame.Core.PlayerState
{
    public class PlayerJumpingState : PlayerAirborneState
    {
        private bool _canStartFalling;
        private bool _shouldKeepRotating;

        private PlayerJumpData _data;

        public PlayerJumpingState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
        {
            _data = _airborneData.JumpData;
        }
        
        #region IState Method
        
        public override void Enter()
        {
            base.Enter();

            _stateMachine.ReusableData.MovementSpeedMod = 0f;

            _stateMachine.ReusableData.MovementDecelerationForce = _data.DecelerationForce;

            _shouldKeepRotating = _stateMachine.ReusableData.MovementInput != Vector2.zero;

            Jump();
        }

        public override void Exit()
        {
            base.Exit();

            SetBaseRotationData();

            _canStartFalling = false;
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();

            if (_shouldKeepRotating)
            {
                RotateTowardsTargetRotation();
            }

            if (IsMovingUp())
            {
                DecelerateVertically();
            }
        }

        public override void Update()
        {
            base.Update();

            if (!_canStartFalling && IsMovingUp(0f))
            {
                _canStartFalling = true;
            } 

            if (!_canStartFalling || GetPlayerVerticalVelocity().y > 0) return;

            _stateMachine.ChangeState(_stateMachine.FallingState);
        }

        #endregion

        #region Main Method

        private void Jump()
        {
            Vector3 jumpForce = _stateMachine.ReusableData.CurrentJumpForce;

            Vector3 jumpDirection = _stateMachine.Player.transform.forward;

            if (_shouldKeepRotating)
            {
                UpdateTargetRotation(GetMovementDirection());

                jumpDirection = GetTargetRotationDirection(_stateMachine.ReusableData.ÑurrentTargetRotation.y);
            }

            jumpForce.x *= jumpDirection.x;
            jumpForce.z *= jumpDirection.z;

            Vector3 capsuleColliderCenterInputInWorldSpace = _stateMachine.Player.ColliderUtility.CapsuleColliderData.Collider.bounds.center;

            Ray rayFromCenterToDown = new Ray(capsuleColliderCenterInputInWorldSpace, Vector3.down);

            if (Physics.Raycast(rayFromCenterToDown, 
                out RaycastHit hit, 
                _data.JumpToGroundRayDistance, 
                _stateMachine.Player.LayerData.GroundLayer, 
                QueryTriggerInteraction.Ignore))
            {
                float groundAngle = Vector3.Angle(hit.normal, -rayFromCenterToDown.direction);

                if (IsMovingUp())
                {
                    float forceModif = _data.JumpForceModifOnSlopeUp.Evaluate(groundAngle);

                    jumpForce.x *= forceModif; 
                    jumpForce.z *= forceModif; 
                }

                if (IsMovingDown())
                {
                    float forceModif = _data.JumpForceModifOnSlopeDown.Evaluate(groundAngle);

                    jumpForce.y *= forceModif;
                }
            }

            ResetVelocity(); 

            _stateMachine.Player.Rigidbody.AddForce(jumpForce, ForceMode.VelocityChange);
        }

        #endregion

        #region Reusable Methods

        protected override void ResetSprintState()
        {
        }

        #endregion

        #region Input Methods

        protected override void OnMovementCanceled(InputAction.CallbackContext obj)
        {
        }

        #endregion

    }
}
