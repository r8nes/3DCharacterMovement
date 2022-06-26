using System.Collections;
using System.Collections.Generic;
using ActionCatGame.Prototype.State;
using UnityEngine;

namespace ActionCatGame.Prototype.Enemy
{
    public class EnemyDeadState : EnemyBaseState
    {
        public EnemyDeadState(EnemyStateMachine enemyState) : base(enemyState)
        {
        }

        public override void Enter()
        {
            _enemyState.Ragdoll.ToggleRagdoll(true);
            _enemyState.Damage.gameObject.SetActive(false);
            GameObject.Destroy(_enemyState.Target);
        }

        public override void Tick(float delta)
        {
        }

        public override void Exit()
        {
        }
    }
}
