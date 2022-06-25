using System.Collections;
using System.Collections.Generic;
using ActionCatGame.Prototype.Combat;
using UnityEngine;

namespace ActionCatGame.Prototype.State
{
    public class PlayerAttackingState : PlayerBaseState
    {
        private Attack _attack;

        public PlayerAttackingState(PlayerStateMachine playerState, int attackIndex) : base(playerState)
        {
            _attack = _playerState.Attacks[attackIndex];
        }

        public override void Enter()
        {
            _playerState.Animator.CrossFadeInFixedTime(_attack.AnimationName, 0.1f);
        }

        public override void Tick(float delta)
        {
            
        }

        public override void Exit()
        {

        }

        private float GetNormalizedTime() 
        {
            return 1f;
        }
    }
}
