using ActionCatGame.Core.State;

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
            base.Enter();

            _stateMachine.ReusableData.MovementSpeedMod = 0f;
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();

            if (!IsMovingHorizontally()) return;

            DecelerateHorizontally();
        }

        #endregion
    }
}
