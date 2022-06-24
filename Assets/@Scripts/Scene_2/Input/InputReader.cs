using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ActionCatGame
{
    public class InputReader : MonoBehaviour, PlayerControls.IPlayerActions
    {
        private PlayerControls _controls;

        public event Action JumpEvent;
        public event Action DodgeEvent;

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
    }
}
