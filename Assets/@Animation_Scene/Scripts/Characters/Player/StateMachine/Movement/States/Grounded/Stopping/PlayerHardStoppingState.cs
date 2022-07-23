using ActionCatGame.Core.State;

namespace ActionCatGame.Core.PlayerState
{
    public class PlayerHardStoppingState : PlayerStoppingState
    {
        public PlayerHardStoppingState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
        {
        }

        #region IState Methods

        public override void Enter()
        {
            base.Enter();

            _stateMachine.ReusableData.MovementDecelerationForce = _movementData.StopData.HardDecelerationForce;
        }

        #endregion

        #region Reusable Methods

        protected override void OnMove()
        {
            if (_stateMachine.ReusableData.ShouldWalk) return;

            _stateMachine.ChangeState(_stateMachine.RunningState);
        }
        #endregion
    }
}
