using System.Collections;
using System.Collections.Generic;
using ActionCatGame.Prototype.State;
using UnityEngine;

namespace ActionCatGame.Prototype.Player
{
    public class PlayerTargetingState : PlayerBaseState
    {
        private readonly int TARGETING_BLEND_TREE_HASH = Animator.StringToHash("TargetingBlendTree");
        private readonly int TARGETING_FORWARD_HASH = Animator.StringToHash("ForwardSpeed");
        private readonly int TARGETING_RIGHT_HASH = Animator.StringToHash("RightSpeed");

        private const float CROSS_FADE_DURATION = 0.1f;
        public PlayerTargetingState(PlayerStateMachine playerState) : base(playerState) {}

        public override void Enter()
        {
            _playerState.Input.CancelEvent += OnCancel;
            _playerState.Animator.CrossFadeInFixedTime(TARGETING_BLEND_TREE_HASH, CROSS_FADE_DURATION);
        }

        public override void Tick(float delta)
        {
            if (_playerState.Input.IsAttacking)
            {
                _playerState.SwitchState(new PlayerAttackingState(_playerState, 0));
                return;
            }

            if (_playerState.Input.IsBlocking)
            {
                _playerState.SwitchState(new PlayerBlockingState(_playerState));
                return;
            }

            if (_playerState.Targeter.CurrentTarget == null)
            {
                _playerState.SwitchState(new PlayerFreeLookState(_playerState));
                return;
            }

            Vector3 movement = CalculateMovement();

            Move(movement * _playerState.TargetingMovementSpeed, delta);

            UpdateAnimator(delta);
            FaceTarget();
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

        private Vector3 CalculateMovement() 
        {
            Vector3 movement = new Vector3();

            movement += _playerState.transform.right * _playerState.Input.MovementValue.x;
            movement += _playerState.transform.forward * _playerState.Input.MovementValue.y;

            return movement;
        }

        private void UpdateAnimator(float delta) 
        {
            if (_playerState.Input.MovementValue.y == 0)
            {
                _playerState.Animator.SetFloat(TARGETING_FORWARD_HASH, 0, 0.01f, delta);
            }
            else
            {
                float value = _playerState.Input.MovementValue.y > 0 ? 1f : -1f;
                _playerState.Animator.SetFloat(TARGETING_FORWARD_HASH, value, 0.01f, delta);
            }

            if (_playerState.Input.MovementValue.x == 0)
            {
                _playerState.Animator.SetFloat(TARGETING_RIGHT_HASH, 0, 0.01f, delta);
            }
            else
            {
                float value = _playerState.Input.MovementValue.x > 0 ? 1f : -1f;
                _playerState.Animator.SetFloat(TARGETING_RIGHT_HASH, value, 0.01f, delta);
            }
        }
    }
}
