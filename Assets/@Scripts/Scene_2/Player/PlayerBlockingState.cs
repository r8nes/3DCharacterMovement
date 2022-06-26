using System.Collections;
using System.Collections.Generic;
using ActionCatGame.Prototype.State;
using UnityEngine;

namespace ActionCatGame.Prototype.Player
{
    public class PlayerBlockingState : PlayerBaseState
    {
        private readonly int BLOCK_HASH = Animator.StringToHash("Block");

        private const float CROSS_FADE_DURATION = 0.01f;

        public PlayerBlockingState(PlayerStateMachine playerState) : base(playerState)
        {
        }

        public override void Enter()
        {
            _playerState.Health.SetInvulnerable(true);
            _playerState.Animator.CrossFadeInFixedTime(BLOCK_HASH, CROSS_FADE_DURATION);
        }

        public override void Tick(float delta)
        {
            Move(delta);

            if (_playerState.Input.IsBlocking)
            {
                _playerState.SwitchState(new PlayerTargetingState(_playerState));
                return;
            }

            if (_playerState.Targeter.CurrentTarget == null)
            {
                _playerState.SwitchState(new PlayerFreeLookState(_playerState));
                return;
            }
        }

        public override void Exit()
        {
            _playerState.Health.SetInvulnerable(false);
        }
    }
}
