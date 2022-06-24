using ActionCatGame.Movement;
using UnityEngine;

namespace ActionCatGame.Handler
{
    public class AnimatorHandler : MonoBehaviour
    {
        private int _vertical;
        private int _horizontal;

        [SerializeField] private bool _canRotate;
        [SerializeField] private Animator _animator;
        [SerializeField] private InputHandler _inputHandler;
        [SerializeField] private PlayerLocomotion _playerLocomotion;

        public bool CanRotation { get => _canRotate; set => _canRotate = value; }
        public Animator Animator { get => _animator; set => _animator = value; }
       
        public void Init()
        {
            Animator = GetComponent<Animator>();
            _inputHandler = GetComponentInParent<InputHandler>();
            _playerLocomotion = GetComponentInParent<PlayerLocomotion>();

            _vertical = Animator.StringToHash("Vertical");
            _horizontal = Animator.StringToHash("Horizontal");
        }

        public void UpdateAnimatorValues(float verticalMovement, float horizontalMovement)
        {
            #region Vertical
            float v = 0;

            if (verticalMovement > 0 && verticalMovement < 0.55f)
                v = 0.5f;
            else if (verticalMovement > 0.55f)
                v = 1;
            else if (verticalMovement < 0 && verticalMovement > -0.55f)
                v = -0.5f;
            else if (verticalMovement < 0.55f)
                v = -1;
            else
                v = 0;
            #endregion

            #region Horizontal
            float h = 0;

            if (horizontalMovement > 0 && horizontalMovement < 0.55f)
                h = 0.5f;
            else if (horizontalMovement > 0.55f)
                h = 1;
            else if (horizontalMovement < 0 && horizontalMovement > -0.55f)
                h = -0.5f;
            else if (horizontalMovement < 0.55f)
                h = -1;
            else
                h = 0;
            #endregion

            Animator.SetFloat(_vertical, v, 0.1f, Time.deltaTime);
            Animator.SetFloat(_horizontal, h, 0.1f, Time.deltaTime);
        }

        public void PlayTargetAnimation(string targetAnim, bool isInteracting) 
        {
            Animator.applyRootMotion = isInteracting;
            Animator.SetBool("isInteracting", isInteracting);
            Animator.CrossFade(targetAnim, 0.2f);
        }

        public void CanRotate()
        {
            CanRotation = true;
        }

        public void StopRotation()
        {
            CanRotation = false;
        }

        private void OnAnimatorMove()
        {
            if (_inputHandler.IsInteracting == false)
                return;

            float delta = Time.deltaTime;
            _playerLocomotion.RigidBody.drag = 0;
            Vector3 deltaPosition = _animator.deltaPosition;
            deltaPosition.y = 0;
            Vector3 velocity = deltaPosition / delta;
            _playerLocomotion.RigidBody.velocity = velocity;
        }
    }
}
