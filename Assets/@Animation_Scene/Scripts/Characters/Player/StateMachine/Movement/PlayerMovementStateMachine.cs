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
        public PlayerLightStoppingState LightStopState { get; }
        public PlayerMediumStoppingState MediumStopState { get; }
        public PlayerHardStoppingState HardStopState { get; }

        public PlayerMovementStateMachine(Player player) 
        {
            Player = player;

            ReusableData = new PlayerStateReusableData();
            
            IdlingState = new PlayerIdlingState(this);
            WalkingState = new PlayerWalkingState(this);

            RunningState = new PlayerRunningState(this);
            SprintingState = new PlayerSprintingState(this);
            DashingState = new PlayerDashingState(this);

            LightStopState = new PlayerLightStoppingState(this);
            MediumStopState = new PlayerMediumStoppingState(this);
            HardStopState = new PlayerHardStoppingState(this);
        }
    }
}
