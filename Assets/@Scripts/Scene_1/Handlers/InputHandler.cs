using UnityEngine;

namespace ActionCatGame.Handler
{
    public class InputHandler : MonoBehaviour
    {
        [SerializeField] private float _horizontal;
        [SerializeField] private float _vertical;
        [SerializeField] private float _moveAmoint;
        [SerializeField] private float _mouseX;
        [SerializeField] private float _mouseY;

        [SerializeField] private bool _b_input;
        [SerializeField] private bool _rollFlag;
        [SerializeField] private bool _isInteracting;

        private PlayerControls _inputActions;
        private CameraHandler _cameraHandler;

        private Vector2 _movementInput;
        private Vector2 _cameraInput;


        public float MoveAmoint { get => _moveAmoint; set => _moveAmoint = value; }
        public float Vertical { get => _vertical; set => _vertical = value; }
        public float Horizontal { get => _horizontal; set => _horizontal = value; }
        public bool RollFlag { get => _rollFlag; set => _rollFlag = value; }
        public bool IsInteracting { get => _isInteracting; set => _isInteracting = value; }

        private void Start()
        {
            _cameraHandler = CameraHandler.Instance;
        }

        //not smoothed!

        private void Update()
        {
            float delta = Time.deltaTime;

            if (_cameraHandler != null)
            {
                _cameraHandler.HandleCameraRotation(delta, _mouseX, _mouseY);
            }
        }

        private void FixedUpdate()
        {
            float delta = Time.deltaTime;

            if (_cameraHandler!= null)
            {
                _cameraHandler.FollowTarget(delta);
            }
        }

        public void TickInput(float delta)
        {
            MoveInput(delta);
            HandleRollInput(delta);
        }

        private void MoveInput(float delta)
        {
            Horizontal = _movementInput.x;
            Vertical = _movementInput.y;

            MoveAmoint = Mathf.Clamp01(Mathf.Abs(Horizontal) + Mathf.Abs(Vertical));

            _mouseX = _cameraInput.x;
            _mouseY = _cameraInput.y;
        }

        private void HandleRollInput(float delta) 
        {
            _b_input = _inputActions.PlayerActions.Roll.phase == UnityEngine.InputSystem.InputActionPhase.Performed;
         
            if (_b_input)
            {
                 RollFlag = true;
            }
        }

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
    }
}
