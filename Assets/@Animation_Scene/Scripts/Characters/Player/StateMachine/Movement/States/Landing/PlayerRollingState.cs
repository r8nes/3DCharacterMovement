using ActionCatGame.Core.Data;
using ActionCatGame.Core.State;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ActionCatGame.Core.PlayerState
{
    public class PlayerRollingState : PlayerLandingState
    {
        private PlayerRollData _data;

        public PlayerRollingState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
        {
            _data = _movementData.RollData;
        }

        #region IState Methods

        public override void Enter()
        {
            base.Enter();

            _stateMachine.ReusableData.MovementSpeedMod = _data.SpeedModid;

            _stateMachine.ReusableData.ShouldSprint = false;
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();

            if (_stateMachine.ReusableData.MovementInput != Vector2.zero) return;

            RotateTowardsTargetRotation();
        }

        public override void OnAnimationTransitionEvent()
        {
            if (_stateMachine.ReusableData.MovementInput == Vector2.zero)
            {
                _stateMachine.ChangeState(_stateMachine.MediumStoppingState);
                return;
            }

            OnMove();
        }

        #endregion

        #region Input Methods
        
        protected override void OnJumpStarted(InputAction.CallbackContext obj)
        {

        }
        
        #endregion

    }
}
