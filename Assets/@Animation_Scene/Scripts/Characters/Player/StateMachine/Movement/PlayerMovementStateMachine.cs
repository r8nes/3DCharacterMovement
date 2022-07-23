using ActionCatGame.Core.Character;
using ActionCatGame.Core.Data;
using ActionCatGame.Core.PlayerState;

namespace ActionCatGame.Core.State
{
    public class PlayerMovementStateMachine : StateMachine
    {
        public Player Player { get; }
        public PlayerStateReusableData ReusableData { get; }
        public PlayerIdlingState IdlingState { get; }
        public PlayerWalkingState WalkingState { get; }
        public PlayerRunningState RunningState { get; }
        public PlayerSprintingState SprintingState { get; }
        public PlayerDashingState DashingState { get; }
        public PlayerLightStoppingState LightStoppingState { get; }
        public PlayerMediumStoppingState MediumStoppingState { get; }
        public PlayerHardStoppingState HardStoppingState { get; }

        public PlayerMovementStateMachine(Player player) 
        {
            Player = player;

            ReusableData = new PlayerStateReusableData();
            
            IdlingState = new PlayerIdlingState(this);
            WalkingState = new PlayerWalkingState(this);

            RunningState = new PlayerRunningState(this);
            SprintingState = new PlayerSprintingState(this);
            DashingState = new PlayerDashingState(this);

            LightStoppingState = new PlayerLightStoppingState(this);
            MediumStoppingState = new PlayerMediumStoppingState(this);
            HardStoppingState = new PlayerHardStoppingState(this);
        }
    }
}
