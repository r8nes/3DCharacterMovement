using System.Collections;
using System.Collections.Generic;
using ActionCatGame.Prototype.State;
using UnityEngine;

namespace ActionCatGame.Prototype.Enemy
{
    public class EnemyIdleState : EnemyBaseState
    {
        private readonly int LOCOMOTION_HASH = Animator.StringToHash("Locomotion");
        private readonly int SPEED_HASH = Animator.StringToHash("Speed");

        private const float CROSS_FADE_TIME = 0.1f;
        private const float DAMP_TIME = 0.1f;

        public EnemyIdleState(EnemyStateMachine enemyState) : base(enemyState)
        {
        }

        public override void Enter()
        {
            _enemyState.Animator.CrossFadeInFixedTime(LOCOMOTION_HASH, CROSS_FADE_TIME);
        }

        public override void Tick(float delta)
        {
            Move(delta);

            if (IsInChaseRange())
            {
                
                return;
            }

            _enemyState.Animator.SetFloat(SPEED_HASH, 0, DAMP_TIME, delta);
        }

        public override void Exit()
        {
        }
    }
}
