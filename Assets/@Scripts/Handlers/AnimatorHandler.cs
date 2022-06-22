using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ActionCatGame.Handler
{
    public class AnimatorHandler : MonoBehaviour
    {
        private int _vertical;
        private int _horizontal;

        [SerializeField] private bool _canRotate;

        [SerializeField] Animator _animator;

        public bool CanRotation { get => _canRotate; set => _canRotate = value; }

        public void Init()
        {
            _animator = GetComponent<Animator>();

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

            _animator.SetFloat(_vertical, v, 0.1f, Time.deltaTime);
            _animator.SetFloat(_horizontal, h, 0.1f, Time.deltaTime);
        }

        public void CanRotate()
        {
            CanRotation = true;
        }

        public void StopRotation()
        {
            CanRotation = false;
        }
    }
}
