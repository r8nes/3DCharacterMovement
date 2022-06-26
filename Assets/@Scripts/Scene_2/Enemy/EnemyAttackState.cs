using System.Collections;
using System.Collections.Generic;
using ActionCatGame.Prototype.Enemy;
using ActionCatGame.Prototype.State;
using UnityEngine;

namespace ActionCatGame
{
    public class EnemyAttackState : EnemyBaseState
    {
        private readonly int ATTACK_HASH = Animator.StringToHash("Attack");

        private const float TRANSITION_DURATION = 0.1f;

        public EnemyAttackState(EnemyStateMachine enemyState) : base(enemyState)
        {
        }

        public override void Enter()
        {
            _enemyState.Damage.SetAttack(_enemyState.AttackDamage, _enemyState.Knockback);
            _enemyState.Animator.CrossFadeInFixedTime(ATTACK_HASH, TRANSITION_DURATION);
        }

        public override void Tick(float delta)
        {
            if (GetNormalizedTime(_enemyState.Animator) >= 1)
            {
                _enemyState.SwitchState(new EnemyChasingState(_enemyState));
            }
        }

        public override void Exit()
        {

        }
    }
}
