using UnityEngine;
using ActionCatGame.Prototype.State;
using ActionCatGame.Prototype.Player;

namespace ActionCatGame.Prototype.State { 
    public abstract class PlayerBaseState : State
    {
        protected private PlayerStateMachine _playerState;

        public PlayerBaseState(PlayerStateMachine playerState) 
        {
            _playerState = playerState;
        }

        protected void Move(float delta) 
        {
            Move(Vector3.zero, delta);
        }

        protected void Move(Vector3 motion, float delta) 
        {
            _playerState.CharacterController.Move((motion + _playerState.ForceReceiver.Movement) * delta);
        }

        protected void FaceTarget() 
        {
            if (_playerState.Targeter.CurrentTarget == null) return;

            Vector3 lookPos = _playerState.Targeter.CurrentTarget.transform.position - _playerState.transform.position;
            lookPos.y = 0f;

            _playerState.transform.rotation = Quaternion.LookRotation(lookPos);
        }

        protected void ReturnToLocomotion() 
        {
            if (_playerState.Targeter.CurrentTarget != null)
            {
                _playerState.SwitchState(new PlayerTargetingState(_playerState));
            }
            else
            {
                _playerState.SwitchState(new PlayerFreeLookState(_playerState));
            }
        }
    }
}
