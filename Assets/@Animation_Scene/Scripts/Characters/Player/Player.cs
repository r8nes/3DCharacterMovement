using System.Collections;
using System.Collections.Generic;
using ActionCatGame.Core.Data;
using ActionCatGame.Core.State;
using UnityEngine;

namespace ActionCatGame.Core.Character
{
    [RequireComponent(typeof(PlayerInput))]

    public class Player : MonoBehaviour
    {
        private PlayerMovementStateMachine _movementStateMachine;

        [field: Header("References")]
        [field:SerializeField] public PlayerSO Data { get; private set; }
        public Transform MainCameraTransform { get; private set; }
        public Rigidbody Rigidbody { get; private set; }
        public PlayerInput Input { get; private set; }

        private void Awake()
        {
            Rigidbody = GetComponent<Rigidbody>();
            Input = GetComponent<PlayerInput>();

            _movementStateMachine = new PlayerMovementStateMachine(this);

            MainCameraTransform = Camera.main.transform;
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
