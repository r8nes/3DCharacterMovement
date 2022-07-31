using ActionCatGame.Core.Data;
using ActionCatGame.Core.State;
using ActionCatGame.Core.Utilities;
using UnityEngine;

namespace ActionCatGame.Core.Character
{
    [RequireComponent(typeof(PlayerInput))]

    public class Player : MonoBehaviour
    {
        private PlayerMovementStateMachine _movementStateMachine;

        [field: Header("References")]
        [field:SerializeField] public PlayerSO Data { get; private set; }

        [field:Header(("Collision"))]
        [field:SerializeField] public PlayerCapsuleColliderUtility ColliderUtility { get; private set; }
        [field:SerializeField] public PlayerLayerData LayerData { get; private set; }
        
        [field: Header(("Cameras"))]
        [field:SerializeField] public PlayerCameraUtility CameraUtility { get; private set; }
        public Transform MainCameraTransform { get; private set; }

        [field: Header(("Animations"))]
        [field:SerializeField] public PlayerAnimationData AnimationData { get; private set; }
        public Rigidbody Rigidbody { get; private set; }
        public Animator Animator { get; private set; }
        public PlayerInput Input { get; private set; }

        private void Awake()
        {
            Rigidbody = GetComponent<Rigidbody>();
            Input = GetComponent<PlayerInput>();
            Animator = GetComponentInChildren<Animator>();

            ColliderUtility.Init(gameObject);
            ColliderUtility.CalculateCapsuleColliderDimensions();
            CameraUtility.Init();
            AnimationData.Init();

            MainCameraTransform = Camera.main.transform;

            _movementStateMachine = new PlayerMovementStateMachine(this);
        }

        private void OnValidate()
        {
            ColliderUtility.Init(gameObject);
            ColliderUtility.CalculateCapsuleColliderDimensions();
        }

        private void Start()
        {
            _movementStateMachine.ChangeState(_movementStateMachine.IdlingState);
        }

        private void OnTriggerEnter(Collider collider)
        {
            _movementStateMachine.OnTriggerEnter(collider);
        }

        private void OnTriggerExit(Collider collider)
        {
            _movementStateMachine.OnTriggerExit(collider);
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

        public void OnMovementStateAnimationEnterEvent() 
        {
            _movementStateMachine.OnAnimationEnterEvent();
        }

        public void OnMovementStateAnimationExitEvent()
        {
            _movementStateMachine.OnAnimationEnterEvent();
        }
        
        public void OnMovementStateAnimationTransitionEvent()
        {
            _movementStateMachine.OnAnimationEnterEvent();
        }
    }
}
