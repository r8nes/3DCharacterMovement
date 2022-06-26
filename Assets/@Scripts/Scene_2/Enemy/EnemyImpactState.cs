using System.Collections;
using System.Collections.Generic;
using ActionCatGame.Prototype.Enemy;
using ActionCatGame.Prototype.State;
using UnityEngine;

namespace ActionCatGame.Prototype.Enemy
{
    public class EnemyImpactState : EnemyBaseState
    {
        private readonly int IMPACT_HASH = Animator.StringToHash("Impact");
        private const float CROSS_FADE_DURATION = 0.1f;
        private float _duration = 1f;

        public EnemyImpactState(EnemyStateMachine enemyState) : base(enemyState)
        {
        }

        public override void Enter()
        {
            _enemyState.Animator.CrossFadeInFixedTime(IMPACT_HASH, CROSS_FADE_DURATION);
        }

        public override void Tick(float delta)
        {
            Move(delta);
         
            _duration -= delta;

            if (_duration <= 0f)
            {
                _enemyState.SwitchState(new EnemyIdleState(_enemyState)); 
            }
        }

        public override void Exit()
        {
        }
    }
}
