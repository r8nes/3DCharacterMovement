using System;
using System.Collections;
using System.Collections.Generic;
using ActionCatGame.Prototype.State;
using UnityEngine;

namespace ActionCatGame.Prototype.Enemy
{
    public class EnemyChasingState : EnemyBaseState
    {
        private readonly int LOCOMOTION_HASH = Animator.StringToHash("Locomotion");
        private readonly int SPEED_HASH = Animator.StringToHash("Speed");

        private const float CROSS_FADE_TIME = 0.1f;
        private const float DAMP_TIME = 0.1f;

        public EnemyChasingState(EnemyStateMachine enemyState) : base(enemyState)
        {
        }

        public override void Enter()
        {
            _enemyState.Animator.CrossFadeInFixedTime(LOCOMOTION_HASH, CROSS_FADE_TIME);
        }

        public override void Tick(float delta)
        {
            if (!IsInChaseRange())
            {
                _enemyState.SwitchState(new EnemyIdleState(_enemyState));
                return;
            }
            else if (IsInAttackRange())
            {
                _enemyState.SwitchState(new EnemyAttackState(_enemyState));
                return;
            }

            MoveToPlayer(delta);

            FacePlayer();

            _enemyState.Animator.SetFloat(SPEED_HASH, 1f, DAMP_TIME, delta);
        }

        public override void Exit()
        {
            if (_enemyState.Agent.enabled)
            {
                _enemyState.Agent.ResetPath();
            }
            _enemyState.Agent.velocity = Vector3.zero;
        }

        private void MoveToPlayer(float delta)
        {
            if (_enemyState.Agent.isOnNavMesh)
            {
                _enemyState.Agent.destination = _enemyState.Player.transform.position;
                Move(_enemyState.Agent.desiredVelocity.normalized * _enemyState.MovementSpeed, delta);
            }

            _enemyState.Agent.velocity = _enemyState.CharacterController.velocity;
        }

        private bool IsInAttackRange()
        {
            if (_enemyState.Player.IsDead) return false;
            
            float playerDistanceSqr = (_enemyState.Player.transform.position - _enemyState.transform.position).sqrMagnitude;

            // ьс Mathf.Pow
            return playerDistanceSqr <= _enemyState.AttackRange * _enemyState.AttackRange;
        }
    }
}
