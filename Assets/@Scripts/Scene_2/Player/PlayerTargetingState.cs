using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ActionCatGame.Prototype.State
{
    public class PlayerTargetingState : PlayerBaseState
    {
        private readonly int TARGETING_BLEND_TREE_HASH = Animator.StringToHash("TargetingBlendTree");

        public PlayerTargetingState(PlayerStateMachine playerState) : base(playerState) {}

        public override void Enter()
        {
            _playerState.Input.CancelEvent += OnCancel;
            _playerState.Animator.Play(TARGETING_BLEND_TREE_HASH);
        }
        public override void Tick(float delta)
        {
            Debug.Log(_playerState.Targeter.CurrentTarget.name);
        }

        public override void Exit()
        {
            _playerState.Input.CancelEvent -= OnCancel;
        }

        private void OnCancel() 
        {
            _playerState.Targeter.Cancel();
            _playerState.SwitchState(new PlayerFreeLookState(_playerState));    
        }
    }
}
