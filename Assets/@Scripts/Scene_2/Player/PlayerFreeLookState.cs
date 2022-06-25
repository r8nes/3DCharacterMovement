using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ActionCatGame.Prototype.State
{
    public class PlayerFreeLookState : PlayerBaseState
    {
        private readonly int FREE_LOOK_SPEED_HASH = Animator.StringToHash("FreeLookSpeed");

        private readonly int FREE_LOOK_BLEND_HASH = Animator.StringToHash("FreeLookBlendTree");

        private const float ANIMATOR_DAMP_TIME = 0.15f;

        public PlayerFreeLookState(PlayerStateMachine playerState) : base(playerState) {}

        public override void Enter()
        {
            _playerState.Input.TargetEvent += OnTarget;
            _playerState.Animator.Play(FREE_LOOK_BLEND_HASH);
        }

        public override void Tick(float delta)
        {
            Vector3 movement = CalculateMovement();

            _playerState.CharacterController.Move(_playerState.FreeLookMovementSpeed * delta * movement);

            if (_playerState.Input.MovementValue == Vector2.zero)
            {
                _playerState.Animator.SetFloat(FREE_LOOK_SPEED_HASH, 0, ANIMATOR_DAMP_TIME, delta);
                return;
            }

            _playerState.Animator.SetFloat(FREE_LOOK_SPEED_HASH, 1, ANIMATOR_DAMP_TIME / 2 , delta);
            FaceMovementDirection(movement, delta);
        }
        public override void Exit()
        {
            _playerState.Input.TargetEvent -= OnTarget;
        }

        private void OnTarget()
        {
            if (!_playerState.Targeter.SelectTarget()) return;
         
            _playerState.SwitchState(new PlayerTargetingState(_playerState)); 
        }

        private void FaceMovementDirection(Vector3 movement, float delta)
        {
            _playerState.transform.rotation = Quaternion.Lerp(
                _playerState.transform.rotation, 
                Quaternion.LookRotation(movement), 
                delta * _playerState.RotationDamping);
        }


        private Vector3 CalculateMovement()
        {

            Vector3 forward =_playerState.MainCameraTranform.forward;
            Vector3 right = _playerState.MainCameraTranform.right;

            forward.y = 0f;
            right.y = 0f;

            forward.Normalize();
            right.Normalize();

            return forward * _playerState.Input.MovementValue.y + right * _playerState.Input.MovementValue.x;
        }
    }
}
