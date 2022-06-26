using System;
using System.Collections;
using System.Collections.Generic;
using ActionCatGame.Prototype.Combat;
using ActionCatGame.Prototype.State;
using UnityEngine;

namespace ActionCatGame.Prototype.Player
{
    public class PlayerAttackingState : PlayerBaseState
    {
        private bool _alreadyAppliedForce;
        
        private float _prevFrameTime;

        private Attack _attack;

        public PlayerAttackingState(PlayerStateMachine playerState, int attackIndex) : base(playerState)
        {
            _attack = _playerState.Attacks[attackIndex];
        }

        public override void Enter()
        {
            _playerState.Damage.SetAttack(_attack.Damage, _attack.KnockBack);
            _playerState.Animator.CrossFadeInFixedTime(_attack.AnimationName, _attack.TransitionDuration);
        }

        public override void Tick(float delta)
        {
            Move(delta);

            FaceTarget();

            float normalizeTime = GetNormalizedTime(_playerState.Animator);

            if (normalizeTime >= _prevFrameTime && normalizeTime < 1f)
            {
                if (normalizeTime >= _attack.ForceTime)
                {
                    TryAttackForce();
                }

                if (_playerState.Input.IsAttacking)
                {
                    TryComboAttack(normalizeTime);
                }
            }
            else
            {
                if (_playerState.Targeter.CurrentTarget !=null)
                {
                    _playerState.SwitchState(new PlayerTargetingState(_playerState));
                }
                else
                {
                    _playerState.SwitchState(new PlayerFreeLookState(_playerState));
                }
            }

            _prevFrameTime = normalizeTime;
        }

        public override void Exit()
        {

        }

        private void TryComboAttack(float normalizeTime)
        {
            if (_attack.CombatStateIndex == -1) return;

            if (normalizeTime < _attack.ComboAttackTime) return;

            _playerState.SwitchState
                (
                    new PlayerAttackingState
                    (
                            _playerState,
                            _attack.CombatStateIndex
                     )
                );
        }

        private void TryAttackForce() 
        {
            if (_alreadyAppliedForce) return;

            _playerState.ForceReceiver.AddForce(_playerState.transform.forward * _attack.Force);

            _alreadyAppliedForce = true;
        }
    }
}
