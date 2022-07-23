using ActionCatGame.Core.Data;
using ActionCatGame.Core.State;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ActionCatGame.Core.PlayerState
{
    public class PlayerDashingState : PlayerGroundedState
    {
        private PlayerDashData _data;

        private float _startTime;

        private int _dashedUsed;
        public PlayerDashingState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
        {
            _data = _movementData.DashData;
        }
        
        #region IState Methods
        
        public override void Enter()
        {
            base.Enter();

            _stateMachine.ReusableData.MovementSpeedMod = _data.SpeedModif;

            AddForceOnStationaryState();

            UpdateRowDashed();

            _startTime = Time.time;
        }

        public override void OnAnimationTransitionEvent()
        {
            if (_stateMachine.ReusableData.MovementInput == Vector2.zero)
            {
                _stateMachine.ChangeState(_stateMachine.HardStoppingState);
                
                return;
            }

            _stateMachine.ChangeState(_stateMachine.SprintingState);
        }

        #endregion

        #region Main Methods

        private void AddForceOnStationaryState()
        {
            if (_stateMachine.ReusableData.MovementInput != Vector2.zero) return;

            Vector3 characterRotation = _stateMachine.Player.transform.forward;

            characterRotation.y = 0f;

            _stateMachine.Player.Rigidbody.velocity = characterRotation * GetMovementSpeed();
        }

        private void UpdateRowDashed()
        {
            if (!IsInRow()) _dashedUsed = 0;
            
            ++_dashedUsed;

            if (_dashedUsed == _data.DashesLimit)
            {
                _dashedUsed = 0;

                _stateMachine.Player.Input.DisableActionFor(_stateMachine.Player.Input.PlayerActions.Dash, _data.DashLimitCooldown);      
            }
        }

        private bool IsInRow()
        {
            return Time.time < _startTime + _data.TimeToBeConsiderRow;
        }

        #endregion

        #region Input Methods

        protected override void OnMovementCanceled(InputAction.CallbackContext obj)
        {
        }

        protected override void OnDashStarted(InputAction.CallbackContext obj)
        {
        }

        #endregion
    }
}
