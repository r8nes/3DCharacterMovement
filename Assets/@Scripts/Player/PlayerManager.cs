using ActionCatGame.Handler;
using UnityEngine;

namespace ActionCatGame
{
    public class PlayerManager : MonoBehaviour
    {
        private InputHandler _inputHandler;
        private Animator _animator;

        private void Start()
        {
            _inputHandler = GetComponent<InputHandler>();
            _animator = GetComponentInChildren<Animator>();
        }

        private void Update()
        {
            _inputHandler.IsInteracting = _animator.GetBool("isInteracting");
            _inputHandler.RollFlag = false;
        }
    }
}
