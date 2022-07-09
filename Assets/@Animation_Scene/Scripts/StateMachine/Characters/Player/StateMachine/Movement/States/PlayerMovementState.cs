using System;
using System.Collections;
using System.Collections.Generic;
using ActionCatGame.Core.State;
using UnityEngine;

namespace ActionCatGame.Core.State
{
    public class PlayerMovementState : IState
    {
        protected float _baseSpeed = 5f;
        protected float _speedModif = 1f;

        protected Vector2 _movementInput;
        protected PlayerMovementStateMachine _stateMachine;
        public PlayerMovementState(PlayerMovementStateMachine playerMovementStateMachine)
        {
            _stateMachine = playerMovementStateMachine;
        }

        #region IState Methods
        public void Enter()
        {
            Debug.Log("State:" + GetType().Name);
        }

        public void Exit()
        {
        }

        public void HandleInput()
        {
            ReadMovementInput();
        }

        public void Update()
        {
        }

        public void PhysicsUpdate()
        {
            Move();
        }

        #endregion

        #region Main Method

        private void Move()
        {
            if (_movementInput == Vector2.zero || _speedModif == 0f) return;

            Vector3 movementDir = GetMovementDirection();

            float movementSpeed = GetMovementSpeed();

            Vector3 currentPlayerHorizontalVelocity = GetPlayerHorizontalVelocity();

            _stateMachine.Player.Rigidbody.AddForce(movementDir * movementSpeed - currentPlayerHorizontalVelocity, ForceMode.VelocityChange);
        }

        private void ReadMovementInput()
        {
            _movementInput = _stateMachine.Player.Input.PlayerActions.Move.ReadValue<Vector2>();
        }

        #endregion

        #region Reusable Methods

        protected float GetMovementSpeed()
        {
            return _baseSpeed * _speedModif;
        }

        protected Vector3 GetMovementDirection()
        {
            return new Vector3(_movementInput.x, 0f, _movementInput.y);
        }

        protected Vector3 GetPlayerHorizontalVelocity()
        {
            Vector3 playerHorizontalVelocity = _stateMachine.Player.Rigidbody.velocity;

            playerHorizontalVelocity.y = 0f;

            return playerHorizontalVelocity;
        }

        #endregion
    }
}