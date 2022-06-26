using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ActionCatGame.Prototype.State
{
    public abstract class EnemyBaseState : State
    {
        protected private EnemyStateMachine _enemyState;

        public EnemyBaseState(EnemyStateMachine enemyState)
        {
            _enemyState = enemyState;
        }

        protected bool IsInChaseRange() 
        {
            float playerDistance =(_enemyState.Player.transform.position - _enemyState.transform.position).sqrMagnitude;

            // �������� ���������� ����� Mathf.Pow
            return playerDistance <= _enemyState.PlayerChasingRange * _enemyState.PlayerChasingRange ;
        }

        protected void Move(float delta)
        {
            Move(Vector3.zero, delta);
        }

        protected void Move(Vector3 motion, float delta)
        {
            _enemyState.CharacterController.Move((motion + _enemyState.ForceReceiver.Movement) * delta);
        }
    }
}
