using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ActionCatGame.Prototype.State
{
    public class PlayerTestState : PlayerBaseState
    {
        private float _timer = 0;
        public PlayerTestState(PlayerStateMachine playerState) : base(playerState)
        {

        }

        public override void Enter()
        {
            _playerState.Input.JumpEvent += OnJump;
        }

        public override void Tick(float delta)
        {
            _timer += delta;

            Debug.Log(_timer) ;
        }

        public override void Exit()
        {
            _playerState.Input.JumpEvent -= OnJump;
        }

        private void OnJump() 
        {
            _playerState.SwitchState(new PlayerTestState(_playerState));
        }
    }
}
