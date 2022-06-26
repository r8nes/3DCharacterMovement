using System.Collections;
using System.Collections.Generic;
using ActionCatGame.Prototype.Player;
using ActionCatGame.Prototype.State;
using UnityEngine;

namespace ActionCatGame.Prototype.Player
{
    public class PlayerImpactState : PlayerBaseState
    {
        private readonly int IMPACT_HASH= Animator.StringToHash("Impact");

        private const float CROSS_FADE_DURATION = 0.1f;
        
        private float _duration = 1f;

        public PlayerImpactState(PlayerStateMachine playerState) : base(playerState)
        {
        }

        public override void Enter()
        {
            _playerState.Animator.CrossFadeInFixedTime(IMPACT_HASH, CROSS_FADE_DURATION);

        }

        public override void Tick(float delta)
        {
            Move(delta);

            _duration -= delta;

            if (_duration <= 0f)
            {
                ReturnToLocomotion();
            }
        }

        public override void Exit()
        {
        }
    }
}
