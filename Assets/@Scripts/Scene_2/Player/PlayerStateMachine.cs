using System.Collections;
using System.Collections.Generic;
using ActionCatGame.Prototype.Combat;
using ActionCatGame.Prototype.Props;
using UnityEngine;

namespace ActionCatGame.Prototype.State
{
    public class PlayerStateMachine : StateMachine
    {
        [field: SerializeField] public InputReader Input { get; private set; }
        [field: SerializeField] public CharacterController CharacterController { get; private set; }
        [field: SerializeField] public Animator Animator { get; private set; }
        [field: SerializeField] public Targeter Targeter { get; private set; }
        [field: SerializeField] public ForceReceiver ForceReceiver { get; private set; }
        [field: SerializeField] public float FreeLookMovementSpeed { get; private set; }
        [field: SerializeField] public float TargetingMovementSpeed { get; private set; }
        [field: SerializeField] public float RotationDamping { get; private set; }
        [field: SerializeField] public Attack[] Attacks { get; private set; }

        public Transform MainCameraTranform { get; private set; }

        private void Start()
        {
            MainCameraTranform = Camera.main.transform;

            SwitchState(new PlayerFreeLookState(this));
        }
    }
}
