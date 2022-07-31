using ActionCatGame.Core.State;
using UnityEngine.InputSystem;

namespace ActionCatGame.Core.PlayerState
{
    public class PlayerStoppingState : PlayerGroundedState
    {
        public PlayerStoppingState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
        {
        }

        #region IState Methods

        public override void Enter()
        {
            _stateMachine.ReusableData.MovementSpeedMod = 0f;

            SetBaseCameraRecenteringData();
            
            base.Enter();

            StartAnimation(_stateMachine.Player.AnimationData.StoppingParameterHash);
        }

        public override void Exit()
        {
            base.Exit();

            StopAnimation(_stateMachine.Player.AnimationData.StoppingParameterHash);
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();

            RotateTowardsTargetRotation();

            if (!IsMovingHorizontally()) return;

            DecelerateHorizontally();
        }

        public override void OnAnimationTransitionEvent()
        {
            _stateMachine.ChangeState(_stateMachine.IdlingState);
        }

        #endregion


        #region Reusable Methods

        protected override void AddInputActionsCallBacks()
        {
            base.AddInputActionsCallBacks();

            _stateMachine.Player.Input.PlayerActions.Move.started += OnMovementStatred;
        }

        protected override void RemoveInputActionsCallbacks()
        {
            base.RemoveInputActionsCallbacks();

            _stateMachine.Player.Input.PlayerActions.Move.started -= OnMovementStatred;

        }

        #endregion

        #region Input Methods

        private void OnMovementStatred(InputAction.CallbackContext obj)
        {
            OnMove();
        }

        #endregion
    }
}
