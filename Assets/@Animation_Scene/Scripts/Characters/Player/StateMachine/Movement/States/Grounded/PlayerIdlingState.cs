using System.Collections;
using System.Collections.Generic;
using ActionCatGame.Core.State;
using UnityEngine;

namespace ActionCatGame.Core.PlayerState
{
    public class PlayerIdlingState : PlayerMovementState
    {
        public PlayerIdlingState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
        {
        }
    }
}
