using System.Collections;
using System.Collections.Generic;
using ActionCatGame.Core.State;
using UnityEngine;

namespace ActionCatGame.Core.Character
{
    [RequireComponent(typeof(PlayerInput))]

    public class Player : MonoBehaviour
    {
        private PlayerMovementStateMachine _movementStateMachine;

        public Rigidbody Rigidbody { get; private set; }
        public PlayerInput Input { get; private set; }

        private void Awake()
        {
            Rigidbody = GetComponent<Rigidbody>();
            Input = GetComponent<PlayerInput>();

            _movementStateMachine = new PlayerMovementStateMachine(this);
        }

        private void Start()
        {
            _movementStateMachine.ChangeState(_movementStateMachine.IdlingState);
        }

        private void Update()
        {
            _movementStateMachine.HandleInput();
            _movementStateMachine.Update();
        }

        private void FixedUpdate()
        {
            _movementStateMachine.PhysicsUpdate();
        }
    }
}
