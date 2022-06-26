using System.Collections;
using System.Collections.Generic;
using ActionCatGame.Prototype.State;
using UnityEngine;

namespace ActionCatGame.Prototype.Player
{
    public class PlayerDeadState : PlayerBaseState
    {
        public PlayerDeadState(PlayerStateMachine playerState) : base(playerState)
        {
        }

        public override void Enter()
        {
            _playerState.Ragdoll.ToggleRagdoll(true);
            _playerState.Damage.gameObject.SetActive(false);
        }

        public override void Tick(float delta)
        {
        }

        public override void Exit()
        {
        }

    }
}
