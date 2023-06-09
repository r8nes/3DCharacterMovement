using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ActionCatGame
{
    public class InputReader : MonoBehaviour, PlayerControls.IPlayerActions
    {
        public bool IsAttacking { get; private set; }
        public bool IsBlocking { get; private set; }

        private PlayerControls _controls;

        public event Action JumpEvent;
        public event Action DodgeEvent;
        public event Action TargetEvent;
        public event Action CancelEvent;
        public event Action BlockEvent;
        public Vector2 MovementValue { get; private set; }

        private void Start()
        {
            _controls = new PlayerControls();
            _controls.Player.SetCallbacks(this);

            _controls.Player.Enable();
        }

        private void OnDestroy()
        {
            _controls.Player.Disable();
        }

        public void OnJump(InputAction.CallbackContext context)
        {
            if (!context.performed) return;
                JumpEvent?.Invoke();
        }

        public void OnDodge(InputAction.CallbackContext context)
        {
            if (!context.performed) return;
            DodgeEvent?.Invoke();
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            MovementValue = context.ReadValue<Vector2>();
        }

        public void OnLook(InputAction.CallbackContext context)
        {
        }

        public void OnTarget(InputAction.CallbackContext context)
        {
            if (!context.performed) return;

            TargetEvent?.Invoke();
        }

        public void OnCancel(InputAction.CallbackContext context)
        {
            if (!context.performed) return;

            CancelEvent?.Invoke();
        }

        public void OnAttack(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                IsAttacking = true;
            }
            else if (context.canceled)
            {
                IsAttacking = false;
            }
        }

        public void OnBlock(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                IsBlocking = true;
            }
            else if (context.canceled)
            {
                IsBlocking = false;
            }
        }

        public void OnWalkToggle(InputAction.CallbackContext context)
        {
            throw new NotImplementedException();
        }

        public void OnZoom(InputAction.CallbackContext context)
        {
            throw new NotImplementedException();
        }

        public void OnDash(InputAction.CallbackContext context)
        {
            throw new NotImplementedException();
        }

        public void OnSprint(InputAction.CallbackContext context)
        {
            throw new NotImplementedException();
        }
    }
}
