using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ActionCatGame.Prototype.Props
{
    public class Ragdoll : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private CharacterController _controller;

        private Collider[] _colliders;
        private Rigidbody[] _rigidbodies;

        private void Start()
        {
            _colliders = GetComponentsInChildren<Collider>(true);
            _rigidbodies = GetComponentsInChildren<Rigidbody>(true);
            ToggleRagdoll(false);
        }

        public void ToggleRagdoll(bool isRagdoll)
        {
            foreach (var collider in _colliders)
            {
                if (collider.gameObject.CompareTag("Ragdoll"))
                {
                    collider.enabled = isRagdoll;
                }
            }

            foreach (var rig in _rigidbodies)
            {
                if (rig.gameObject.CompareTag("Ragdoll"))
                {
                    rig.isKinematic = !isRagdoll;
                    rig.useGravity = isRagdoll;
                }
            }

            _controller.enabled = !isRagdoll;
            _animator.enabled = !isRagdoll;
        }
    }
}
