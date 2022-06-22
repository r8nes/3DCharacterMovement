using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ActionCatGame.Handler.Input
{
    public class InputHandler : MonoBehaviour
    {
        [SerializeField] private float _horizontal;
        [SerializeField] private float _vertical;
        [SerializeField] private float _moveAmoint;
        [SerializeField] private float _mouseX;
        [SerializeField] private float _mouseY;

        private PlayerControls _inputActions;
        private Vector2 _movementInput;
        private Vector2 _cameraInput;

        public float MoveAmoint { get => _moveAmoint; set => _moveAmoint = value; }
        public float Vertical { get => _vertical; set => _vertical = value; }
        public float Horizontal { get => _horizontal; set => _horizontal = value; }

        private void OnEnable()
        {
            if (_inputActions == null)
            {
                _inputActions = new PlayerControls();
                _inputActions.PlayerMovement.Movement.performed += _inputActions => _movementInput = _inputActions.ReadValue<Vector2>();
                _inputActions.PlayerMovement.Camera.performed += i => _cameraInput = i.ReadValue<Vector2>();
            }

            _inputActions.Enable();
        }

        private void OnDisable()
        {
            _inputActions.Disable();
        }

        public void TickInput(float delta) 
        {
            MoveInput(delta);
        }

        private void MoveInput(float delta) 
        {
            Horizontal = _movementInput.x;
            Vertical = _movementInput.y;

            MoveAmoint = Mathf.Clamp01(Mathf.Abs(Horizontal) + Mathf.Abs(Vertical));
            
            _mouseX = _cameraInput.x;
            _mouseY = _cameraInput.y;
        }
    }
}
