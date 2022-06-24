namespace ActionCatGame.Prototype.State
{
    public abstract class PlayerBaseState : State
    {
        protected private PlayerStateMachine _playerState;

        public PlayerBaseState(PlayerStateMachine playerState) 
        {
            _playerState = playerState;
        }
    }
}
